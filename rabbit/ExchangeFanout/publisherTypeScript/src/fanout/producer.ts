
const fs = require("fs");
const path = require("path");
var mv = require('mv');
const readline = require('readline');
var amqp = require('amqplib/callback_api');

import * as faker from 'faker';
import {v4 as uuidv4} from 'uuid'
import { Helper } from "../helper";
import { Person } from "../model";
const MESSAGE_AMOUNT=6;
const colors = require("colors");
// var cron = require("node-cron");
// En el fanout la cola no importa por que el publisher le tira a todos

const exchangeName = process.env.EXCHANGE || 'peopleArrivesExchange'; 
const routingKey = process.env.ROUTING_KEY || ''; // no existe dado que es fanout
const exchangeType = 'fanout'
const wait = 400;


export class Publisher {


  constructor() {}

  public async Start() {
    console.log(
      colors.blue("------------------publisher started--------------------")
    );
   
    // await this.DoWork() ;
      //runs every minute
    // cron.schedule("*/1 * * * *", async () => {
    //   await this.DoWork();
    // });

     setInterval(async () => { 
      console.log(
        colors.blue("------------------sending--------------------")
      );
          let messagesAmount:number = MESSAGE_AMOUNT;

          await this.DoWork(messagesAmount) ;

        },13000);
  }



  
   public async DoWork(messagesAmount:number): Promise<void> {
    
    amqp.connect('amqp://localhost', function(error0, connection) {
        if (error0) {
            throw error0;
        }
        connection.createChannel(function(error1, channel) {
            if (error1) {
                throw error1;
            } 
            channel.assertExchange(exchangeName, exchangeType, {
                durable: false // by default is true
            });

            //this.sleepLoop(messagesAmount, async () => {
              while (messagesAmount--){
                const person = Publisher.generatePerson();
                const sent = channel.publish(
                          exchangeName, 
                          routingKey,// no existe dado que es fanout
                          Buffer.from(JSON.stringify(person)), {
                             persistent: true
                      });

              console.log(sent);
              sent
              ? console.log(`Sent person to "${exchangeName}" exchange`, person.GetFullName())
              : console.log(`Fails sending message to "${exchangeName}" exchange` )

              

            }
          //});
            
        });

       setTimeout(function() {

              connection.close();
              //process.exit(0);
          }, 500);
     
    });
   
      
  }
    

   public static generatePerson():Person{
    let  p:Person = new  Person();
    p.Id = uuidv4();
    p.FirstName = faker.name.firstName();
    p.Lastname = faker.name.lastName();

    return p;
   }


   public  sleep(ms) {
    return new Promise((resolve) => {
      setTimeout(resolve,ms);
    })
  }

  public async  sleepLoop(number, cb) {

    while (number--){

      await this.sleep(wait);
      cb();
    }
   
 }

   public async DoWork_old(): Promise<void> {

    return new Promise<void>((resolve, reject) => {
        try{
            let  p:Person = Publisher.generatePerson()
            
    
            this.send(p);
            Helper.Log('enviando a la cola ' + p.GetFullName());
          
            resolve();
        }catch(err){
            Helper.LogErrorFull("Error al leer carpeta de cobranzas", err);
            reject(err);
        }
       

   
      });
      
  }

 
   public async send(person:Person){

   await amqp.connect('amqp://localhost',async function(error0, connection)  {
        if (error0) {
            throw error0;
        }
        connection.createChannel(function(error1, channel) {
            if (error1) {
                throw error1;
            }

            
            
             channel.assertExchange(exchangeName, exchangeType, {
                durable: false // by default is true
            });

            const sent = channel.publish(exchangeName, routingKey, Buffer.from(JSON.stringify(person)));

            sent
            ? console.log(`Sent person to "${exchangeName}" exchange`, person.GetFullName())
            : console.log(`Fails sending message to "${exchangeName}" exchange` )

            
        });
    
        //  setTimeout(function() {
        //      connection.close();
        //      process.exit(0);
        //  }, 500);
    });
   }

}
