const amqp = require('amqplib/callback_api');
import * as faker from 'faker';
const colors = require("colors");
import {v4 as uuidv4} from 'uuid'
import { Person } from './model';
var messagesAmount = 2;
const cron = require("node-cron");
// En el direct la cola no importa, al igual que en el fanout
const exchangeName =  'alertsDirectExchange'; 
const exchangeType = 'direct'



export class Publisher {
  constructor() {}

  public async Start() {
    // leemos los parametros o args
    const args = process.argv.slice(2);
    // routingKey
    const alertSeverity = args.length > 0 ? args[2] : "apple";

    console.log(
      colors.blue(
        `------------------Direct exchange publisher rutingKey = "${alertSeverity}" --------------------`
      )
    );
    

      //await this.DoWork(alertSeverity) ;
      
        setInterval(async () => {
          console.log(colors.blue(`---->Sending--------------- "${alertSeverity}"-----`));
            await this.DoWork(alertSeverity) ;
        }, 3000);
      }
      public async DoWork(alertSeverity): Promise<void> {
    
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
                              '',// no existe dado que es fanout
                              Buffer.from(JSON.stringify(person)), {
                                 persistent: true
                          });
    
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

  public async DoWorkdss(alertSeverity): Promise<void> {
   
    amqp.connect("amqp://localhost" , function (error0, connection) {
     
      if (error0) {
        throw error0;
    }
   
  
      // connection.on("error", function(err) {
      //   if (err.message !== "Connection closing") {
      //     console.error("[AMQP] conn error", err.message);
      //   }
      // });
      // connection.on("close", function() {
      //   console.error("[AMQP] connection close");
      //   //return; // setTimeout(this.DoWork(alertSeverity), 7000);
      // });
    
     // WHEN CONNECTED
       connection.createChannel(function (error1, channel) {
            if (error1) {
              throw error1;
          }
        
    
        // channel.on("error", function(err) {
        //   //console.error("[AMQP] channel error", err.message);
        //   console.error("[channel] error", err);
        //   connection.close();
        // });
        // channel.on("close", function() {
        //   console.log("[AMQP] channel closed");
        // });
        //channel.prefetch(10);
    
        channel.assertExchange(exchangeName, exchangeType, {
          durable: false, // by default is true
        });
    
        while (messagesAmount--) {
    
            const person = Publisher.generatePerson();
            console.log(colors.blue(`---->Sending--------------- "${alertSeverity}"-----`));  
    
            const sent = channel.publish(
              exchangeName, 
              '',// no existe dado que es fanout
              Buffer.from(JSON.stringify(person)), {
                 persistent: true
          });
    
            sent
              ? console.log(`Sent to "${exchangeName}" exchange "${exchangeName}" `,  person.GetFullName())
              : console.log(`Fails sending message to "${exchangeName}" exchange `, person.GetFullName());
      
        }
        //Publisher.exitAfterSend();
      
      });

      setTimeout(function () {
        connection.close();
        //process.exit(0);
      }, 500);
  
  
    });
  }






 







static async   exitAfterSend() {

  console.log(`Exit After Send`);
  await this.sleep(messagesAmount * 500 * 1.2)

    process.exit(0)
}

static sleep(ms) {
  return new Promise((resolve) => {
      setTimeout(resolve, ms)
  })
}

static generatePerson(): Person {
  let p: Person = new Person();
  p.Id = uuidv4();
  p.FirstName = faker.name.firstName();
  p.Lastname = faker.name.lastName();

  return p;
}

}
