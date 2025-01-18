import { Color } from "colors";
import { Helper } from "./common/helper";
import * as cron from 'node-cron';
import { SocioBE, SocioWrap } from "./dto/SociosBE";
import SociosRepo from "./infra/SociosRepo";
import SociosService from "./infra/SociosService";
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
import { AppConstants } from "./common/CommonConstants";
import { DateFunctions } from "./common/DateFunctions";
import { ReportBE } from "./dto/ReportBE";
import LogsService from "./infra/LogsService";
dayjs.extend(utc);

export class Engine {
  private _sociosRepo: SociosRepo;
  private _sociosService: SociosService;
  private _logsService: LogsService;

  /**
   * prevents jonns being interrupted
   */
  private isRunning: boolean;
  

  constructor() {
    this.isRunning = false; // Variable para controlar el estado de ejecución
    this._sociosRepo = new SociosRepo();
    this._logsService = new LogsService();
    this._sociosService = new SociosService();

  }

  /**
   * Call an oimplement the sheduling and execute DoWork
   */
  public async Start() {

    //"*/1 10-17 * * *" | Every minute de 10 AM a 5 PM  (10 a 17)
    //"* 10-17 * * *"   | Every minute de 10 AM a 5 PM   (10 a 17)
    //"*/5 * * * *"     | Every 5 minutes  
    //cron.schedule("*/1 * * * *", async () => {
    cron.schedule(AppConstants.APP_SCHEDULING, async () => {

      if (this.isRunning) {
        return;
      }
      this.isRunning = true; // set as excecution
      try {

        await this.DoWork(); // exec the work
        //await this.DoWork_Test_Socio(); // exec the work

      } finally {
        this.isRunning = false; //  Release lock upon completion
      }

    });
  }

  /**
   * This methods does all the work. It has the logic of calling dababase repository, APIs an perform
   *  all neccesary logs an error controls
   */
  async DoWork(): Promise<void> {
    try {

      await Helper.Log("---------------------Start working---------------------- ", true);
      const limit = parseInt(AppConstants.APP_LIMITS.toString());
      let startDateString = await Helper.getLastUpdate();
      //let startDate = dayjs.utc(startDateString).toDate();

      while (true) {


        await Helper.Log(`Fetching next ${limit} SOCIOS from  ${startDateString} `, true);

        const socios = await this._sociosService.Search(startDateString, limit);
        // I there are not [socios], breack the loop

        if (socios.length === 0) {

          break;
        }
        await Helper.Log(`Proccecing  ${socios.length} Socios `, true);

        //forEach no pueden manejar promesas de manera que respeten el orden de ejecución asincrónica. 
        for (const s of socios) {

          const socio = await this._sociosService.get(s.numSocio);
          const exist = await this._sociosRepo.Exist(s.numSocio, null);
          try {
            if (exist)
              await this._sociosRepo.update(socio);
            else
              await this._sociosRepo.Insert(socio);

            const report: ReportBE = new ReportBE();
            report.fechaModif = s.modif;
            report.numSocio = s.numSocio;
            await this._logsService.Create(report, exist);

            // /**agrego csv para reportes */
            // await Helper.AppendCSV({
            //   NumSocio: s.numSocio,
            //   FechaModif: s.modif,
            //   Action: report.action

            // });

            // await this._reportsRepo.Insert(report);

            //Almaceno par aproxima iteracion
            await Helper.SaveLastUpdate(s.modif);
            //startDate = dayjs.utc(s.modif).toDate();
            startDateString = s.modif.trim();
          }
          catch (error) {
            const msg = `Error Insertar/actualizar numSocio ${s.numSocio} fechaModif : ${s.modif} `;
            Helper.LogErrorFull(msg, error, true);

          }
        }
      }
      await Helper.Log("---------------------End working---------------------- ", true);
    }
    catch (error) {
      Helper.LogErrorFull("Error working", error, true);

    }

  }








  /**
   * Función para realizar la pausa (sleep) 
   * @param s 
   * @returns 
   */
  sleep = (s: number): Promise<void> => {
    const ms = s * 1000;
    return new Promise(resolve => setTimeout(resolve, ms));
  };


  socioslistConsole = (socios: SocioBE[]) => {
    socios.forEach(async s => {

      const msg = `${s.documento} ${s.ultimaModificacion} `
      await Helper.Log(msg, false);
    })

  }


}
