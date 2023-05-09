import { Helper } from './helper';
import * as faker from "faker";
var colors = require("colors");
import { v4 as uuidv4 } from "uuid";
import { Person } from "./model";
//import redis from "redis";
const redis = require('redis')
import { AppSettings, Setting } from './settings';

import * as util from "util"
require('dotenv').config();




export class Publisher {

   
 messagesCount:number;
 messagesTimeout:number = 22; //seconds
  constructor() {
 
    

    let x  = AppSettings.Instance.setting.messagesCount; 
    this.messagesCount= AppSettings.Instance.setting.messagesCount ? AppSettings.Instance.setting.messagesCount : 10;

  }
 
  public async Start() {
    
    console.log( 
      colors.blue(
        `------------------Enviando  "${this.messagesCount}" --------------------`
      )
    );
    console.log(
      colors.green(
        `------------------Redis volume in   "${process.env.RUTA_REDIS}" --------------------`
        )
      ); 

    
  //create a redisClient
  const redisClient= redis.createClient({
    host:"localhost",
    port:6379,
    auth_pass: 'pletorico28',
  });
  
  // const person = Publisher.generatePerson();

  // redisClient.set('persons3',JSON.stringify(person),(error,reply)=>{

  //   if(error)
  //       Helper.LogError('From redis persons3  --> '  + error);

  //   if(reply)
  //       Helper.Log('From redis persons3  --> '  + reply);
    
  // });

    // add promises for get and set requests of redisclient
    // const getAsync = util.promisify(redisClient.get).bind(redisClient);
   await this.redis_flushall(redisClient);
  
    setInterval(async () => {
      Helper.Log(Helper.GetTime () + ' DoWork ------------------------',false);
      
      await this.DoWork(redisClient);
    }, 6000);
  } 

   async DoWork(redisClient): Promise<void> {
    let count = this.messagesCount;
    while (count != 0) {
      
      const person = Publisher.generatePerson();
      //console.log(JSON.stringify(person));
      // We set key name person.Id ,expiry of 10000 and value i.e object, as redis doesn’t support to store object, we stringify before storing
      
     // redisClient.set(person.Id,JSON.stringify(person));
       redisClient.setex('for_kill' + person.Id,this.messagesTimeout,JSON.stringify(person));
       // const value =  redisClient.get(person.Id);
      // const value = ' okkkkkk ';
      // Helper.Log(this.messagesCount + ' From redis dowork :' , true);
      Helper.Log(person.Id + ' sened to Redis ' , true);
      
      await this.delay(5000);
      count--;

    }


  }


  async delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
}


  static generatePerson(): Person {
    let p: Person = new Person();
    p.Id = uuidv4();
    p.FirstName = faker.name.firstName();
    p.Lastname = faker.name.lastName();
    p.CreatedDate = new Date();
    return p;
  }

  async redis_flushall(redisClient): Promise<void> {
    //const  redis_flushall = (redisClient) : Promise<void>=>{
    //const  redis_flushall = (redisClient): Promise<void> => {
    return new Promise ((resolve,reject) => {
      redisClient.del((err, success) => {
        if (err) {
          reject (new Error(err));      
        }
        console.log(success); // will be true if successfull
        resolve (success);
      });
    });
   
  }
  

  // async function upsert(table, data) {
  //   let key = table;
  //   if (data && data.id) {
  //     key = key + '_' + data.id;
  //   }
  
  //   client.setex(key, 10, JSON.stringify(data));
  //   return true;
  // }
}
