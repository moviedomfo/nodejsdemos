import { Helper } from "./helper";


// Esta clase de moment solo esta por si se desea realizar alguna configuracion de entorno
export class AppSettings {

  public static Instance:AppSettings;

  
    public  static async Create(): Promise<AppSettings>{

      //let setting:   AppSettings;
     if(AppSettings.Instance)
      return AppSettings.Instance;
      else
      {
        var res = await Helper.OpenFile('appsettins.json');

        AppSettings.Instance = JSON.parse(res) as AppSettings;
      } 
      
      return  AppSettings.Instance;
    } 


    public producerSettings: ProducerSettings;
}


export class ProducerSettings {
  public apiUrlBase: string;
  public sourceFolder: string;
  public destFolder: string;
}
