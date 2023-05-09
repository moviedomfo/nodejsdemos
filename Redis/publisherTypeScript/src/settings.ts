import { Helper } from "./helper";


// Esta clase de moment solo esta por si se desea realizar alguna configuracion de entorno
export class AppSettings {
  public static Instance: AppSettings;

  public static async Create(): Promise<AppSettings> {
    if (AppSettings.Instance) return AppSettings.Instance;
    else {
      var res = await Helper.OpenFile("appsettins.json");

      AppSettings.Instance = JSON.parse(res) as AppSettings;
    }

    return AppSettings.Instance; 
  }

  public setting: Setting;
}

export class Setting {
  public apiUrlBase: string;
  public sourceFolder: string;
  public destFolder: string;
  public messagesCount: number;
  public messagesTimeout: number;
  
}
