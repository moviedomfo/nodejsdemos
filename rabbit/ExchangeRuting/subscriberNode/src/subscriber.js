'use strict'

const color = require('colors') 
const amqp = require('amqplib')
const queue = process.env.QUEUE || 'alerts'
const exchangeName = process.env.EXCHANGE || 'alertsDirectExchange'; 
//const pattern = process.env.PATTERN || 'orange'; 
var pattern = 'insteretsss'; 
const exchangeType = 'direct'


console.log({
    queue,
    exchangeName,
    pattern
})

async function subscriber() {
    // leemos los parametros o args
    const args = process.argv.slice(2);
    // routingKey
    pattern  = args.length > 0 ? args[2] : "insteretsss";
    console.log(pattern);
    console.log(color.blue(`------------------Listenning Direct Ex ruting  "${pattern}" --------------------` )  );

    const connection = await amqp.connect('amqp://localhost')
    const channel = await connection.createChannel()

    await channel.assertQueue(queue)
    await channel.assertExchange(exchangeName, exchangeType, {
        durable: false // by default is true
    });
    await channel.bindQueue(queue, exchangeName,pattern)

    channel.consume(queue,async (message) => {
        
        const person = JSON.parse(message.content.toString());
        await log(person) 
        
        channel.ack(message)
    })
}

subscriber().catch((error) => {
    console.error(error)
    process.exit(1)
})

function sleep(ms) {
    return new Promise((resolve) => {
      setTimeout(resolve,ms);
    })
  }
  
function log(person) {
    return new Promise((resolve) => {
        const f = new Date().toISOString()
        console.log(color.blue( f + ` Received message from "${queue}" queue`))
        console.log(color.yellow( person.Lastname + ` ,` + person.FirstName))
        resolve()
      })
   

}