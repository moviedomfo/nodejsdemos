import { Helper } from './../helper';
var amqp = require("amqplib/callback_api");
import { Color } from "colors";
import { Person } from "../model";

const colors = require("colors");
const queue = process.env.QUEUE || "peopleArrivesQueue2";
const exchangeName = process.env.EXCHANGE || "peopleArrivesExchange";
const exchangeType = "fanout";
const routingKey = process.env.ROUTING_KEY || "";
const pattern = process.env.PATTERN || "";

export class Subscriber {
  colors: Color;

  constructor() {}

  public async Start() {
    console.log(colors.blue(` Subscriber started listening "${queue}" queue`));

    this.consume();
  }

  async consume() {
     amqp.connect( "amqp://localhost",
      function (error0, connection) {
        if (error0) {
          throw error0;
        }
        connection.createChannel(function (error1, channel) {
          if (error1) {
            throw error1;
          }
          // si el publisher envia msgs antes de que este binding se cree.- Dichos mensajes no llegaran y se
          // perderan .-
          channel.assertExchange(exchangeName, exchangeType, {
            durable: false, // by default is true
          });

          channel.assertQueue(queue, {
              exclusive: false,
            },
            function (error2, q) {
              if (error2) {
                Helper.Log(error2) ;
              }
              console.log(colors.blue(" [*] Waiting for messages in %s. To exit press CTRL+C",q.queue));
              // unimos la cola al exchange 
              // We've already created a fanout exchange and a queue. Now we need to tell the exchange to send messages to our queue. 
              // That relationship between exchange and a queue is called a binding
              channel.bindQueue(queue, exchangeName);
              channel.consume(q.queue, async (message) => {
                  if (message.content) {
                    let person: Person = JSON.parse(message.content.toString()) as Person;
                    await Subscriber.log(person);
                    
                    //channel.ack(message); // si esta noAck: true y enviamos el ack arroja error
                  }
                },{noAck: true}
              );
            }
          );
        });
      }
    );

    // setTimeout( function()  {
    //   channel.close();
    //   conn.close();}
    //   ,  500 );
  }

  

  static log(person) {
    return new Promise<void>((resolve) => {
      const date = new Date().toISOString();
      console.log(
        colors.blue(date + ` Received message from "${queue}" queue`)
      );
      console.log(colors.yellow(person.Lastname + ` ,` + person.FirstName));
      resolve();
    });
  }
}
