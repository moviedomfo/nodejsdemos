import { Helper } from './helper';
import  { Subscriber } from './fanout/subscriber';
import { AppSettings } from './settings';

const consumer = new Subscriber();

init().then(()=>{
  consumer.Start().then((res)=>{
   
     });
});

 
async function init() {
  try {
    //let setting =  await AppSettings.Create();

    Helper.Log('Initializing ....');
    //console.log(setting );
  } catch (error) {
    Helper.LogError(`Got an error trying to write to a file: ${error.message}`);
    
  }
}