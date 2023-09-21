import { Helper } from './helper';
import  { Publisher } from './producer';
import { AppSettings } from './settings';


// como mostrar una fecha con typescript ?


init().then(()=>{
    const publisher = new Publisher();
    publisher.Start().then((res)=>{ });
});

 
async function init() {
    try {
      let setting =  await AppSettings.Create();

      Helper.Log('Initializing ....');
      console.log(setting );
    } catch (error) {
      Helper.LogError(`Got an error trying to write to a file: ${error.message}`);
      
    }
  }