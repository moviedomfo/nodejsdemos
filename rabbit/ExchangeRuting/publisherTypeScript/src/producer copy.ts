var amqp = require('amqplib/callback_api');
import * as faker from 'faker';
var colors = require("colors");
import {v4 as uuidv4} from 'uuid'
import { Person } from './model';


var messagesAmount = 2;
var cron = require("node-cron");
// En el direct la cola no importa, al igual que en el fanout
const exchangeName = process.env.EXCHANGE || 'alersDirectExchange'; 
const exchangeType = 'direct'



export class Publisher2 {
  public static  amqpConn = null;
  constructor() {}

  public async Start() {
    // leemos los parametros o args
    const args = process.argv.slice(2);
    // routingKey
    const alertSeverity = args.length > 0 ? args[2] : "red";

    console.log(
      colors.blue(
        `------------------Direct exchange publisher rutingKey = "${alertSeverity}" --------------------`
      )
    );
    

    await this.DoWork(alertSeverity) ;
    // setInterval(async () => {
    //   console.log(colors.blue(`---->Sending--------------- "${alertSeverity}"-----`));
    //   await this.DoWork(alertSeverity) ;
    // }, 3000);
  }


  public async DoWork(alertSeverity): Promise<void> {
   
    this.createConnection(alertSeverity);
  }


createConnection (alertSeverity){
  amqp.connect("amqp://localhost" + "?heartbeat=60", function (error0, connection) {
    if (error0) {
      console.error("[AMQP]", error0.message);
    }

    connection.on("error", function(err) {
      if (err.message !== "Connection closing") {
        console.error("[AMQP] conn error", err.message);
      }
    });
    connection.on("close", function() {
      console.error("[AMQP] reconnecting");
      return setTimeout(this.createConnection(alertSeverity), 7000);
    });

      // WHEN CONNECTED
      Publisher2.amqpConn = connection;
    connection.createChannel(function (error1, channel) {
      
      if (Publisher2.closeOnErr(error1)) return;
      channel.on("error", function(err) {
        console.error("[AMQP] channel error", err.message);
      });
      channel.on("close", function() {
        console.log("[AMQP] channel closed");
      });
      channel.prefetch(10);

      channel.assertExchange(exchangeName, exchangeType, {
        durable: false, // by default is true
      });

      while (messagesAmount--) {

          const person = Publisher2.generatePerson();
          console.log(colors.blue(`---->Sending--------------- "${alertSeverity}"-----`));  
          const sent = channel.publish( 
            exchangeName,
            alertSeverity, // ruteo
            Buffer.from(JSON.stringify(person)),
            {
              persistent: true,
            }
          );

          sent
            ? console.log(
                `Sent to "${exchangeName}" exchange "${exchangeName}" `,  person.GetFullName())
              : 
              console.log(`Fails sending message to "${exchangeName}" exchange `, person.GetFullName());
    
      }
      //Publisher.exitAfterSend();
    });

    // setTimeout(function () {
    //   connection.close();
    //   //process.exit(0);
    // }, 500);
  });
    // amqp.connect("amqp://localhost"+ "?heartbeat=60", function (err, connection) {
    //   if (err) {
    //     console.error("[AMQP]", err.message);
    //     return setTimeout(this.createConnection, 7000);
    //   }
    //   connection.on("error", function(err) {
    //     if (err.message !== "Connection closing") {
    //       console.error("[AMQP] conn error", err.message);
    //     }
    //   });
    //   connection.on("close", function() {
    //     console.error("[AMQP] reconnecting");
    //     return setTimeout(this.createConnection, 7000);
    //   });
  

    //   Publisher.amqpConn = connection;
    
    //   // WHEN CONNECTED
    //   this.startWorker();
    // });
  }




// A worker that acks messages only if processed succesfully
startWorker() {
  Publisher2.amqpConn.createChannel(function(err, ch) {
    if (Publisher2.closeOnErr(err)) return;
    ch.on("error", function(err) {
      console.error("[AMQP] channel error", err.message);
    });
    ch.on("close", function() {
      console.log("[AMQP] channel closed");
    });
    ch.prefetch(10);
    ch.assertQueue("jobs", { durable: true }, function(err, _ok) {
      if (Publisher2.closeOnErr(err)) return;
      ch.consume("jobs", this.processMsg(), { noAck: false });
      console.log("Worker is started");
    });

    
  });
}

processMsg(msg,ch) {
  var incomingDate = (new Date()).toISOString();
  console.log("Msg [deliveryTag=" + msg.fields.deliveryTag + "] arrived at " + incomingDate);
  this.work(msg, function(ok) {
    console.log("Sending Ack for msg at time " + incomingDate);
    try {
      if (ok)
        ch.ack(msg);
      else
        ch.reject(msg, true);
    } catch (e) {
      this.closeOnErr(e);
    }
  });
}

work(msg, cb) {
  console.log("Got msg", msg.content.toString());
  setTimeout(() => cb(true),  12000);
}

static closeOnErr(err) {
  if (!err) return false;
  console.error("[AMQP] error", err);
  Publisher2.amqpConn.close();
  return true;
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
