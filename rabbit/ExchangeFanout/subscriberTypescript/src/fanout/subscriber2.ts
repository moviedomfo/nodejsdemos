var amqp = require('amqplib/callback_api');
import { Color } from "colors";
import { Helper } from "../helper";
import { Person } from "../model";

const colors = require("colors");
const queue = process.env.QUEUE || 'peopleArrivesQueue';
const exchangeName = process.env.EXCHANGE || 'peopleArrivesExchange';
const exchangeType = 'fanout'
const routingKey = process.env.ROUTING_KEY || ''
const pattern = process.env.PATTERN || '';



export class Subscriber2{
  
  colors: Color;

  constructor() {}

  public async Start() {
    console.log(colors.blue("------------------subscriber started--------------------") );
  
    this.do_consume();
   // await this.DoWork() ;
    
    
  }

  public async DoWork() {

    return new Promise<void>(async (resolve, reject) => {
        try{
           
        
         await this.do_consume(). catch(e => {
          console.log('---------------------------------') ;
           console.log(e) ;
           console.log('---------------------------------') 
          });
          
          resolve();
        }catch(err){
            Helper.LogErrorFull("Error al leer carpeta de cobranzas", err);
            reject(err);
        }
       

   
      });
      
  }

 
  async  do_consume(){

    const  cnn = await amqp.connect('amqp://localhost');
    const  channel = await cnn.createChannel();
  
    await channel.assertQueue(queue);
    //await channel.assertExchange(exchangeName, exchangeType);
    // si el publisher envia msgs antes de que este binding se cree.- Dichos mensajes no llegaran y se 
    // perderan .-
    await channel.bindQueue(queue, exchangeName); // unimos la cola al exchange
   
      
    await channel.consume(
      queue,
      (msg) => {
        //console.log(msg.content.toString());
        let p: Person = JSON.parse(msg.content.toString()) as Person;

        this.intensiveOperation();

        console.log(p.FirstName + " " + p.Lastname);

        channel.ack(msg);

        channel.cancel("myconsumer");
      },
      { consumerTag: "myconsumer" }
    );



    
   }
    
  
  intensiveOperation() {
      let i = 1e3;
      while (i--) {}
    }

 delay(ms: number) {
      return new Promise( resolve => setTimeout(resolve, ms) );
  }
}
