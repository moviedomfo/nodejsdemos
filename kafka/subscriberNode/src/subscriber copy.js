require('dotenv').config()


const color = require('colors') 
const { Kafka } = require('kafkajs')

const topic = process.env.TOPIC ;
const clientId = process.env.TOPIC; 
const brokers = process.env.BROKERS; 

const kafkaConfig ={
    clientId: clientId,
    brokers: brokers,
}

async function subscriber() {
    // leemos los parametros o args
    // const args = process.argv.slice(2);
   //pattern  = args.length > 0 ? args[2] : "insterets";
      console.log(color.blue(`------------------${clientId} listening   ${topic} --------------------` )  );
    
    
    const kafka = new Kafka(kafkaConfig);

    console.log(kafkaConfig);
    const consumer = kafka.consumer({ groupId: 'test-group' })

    await consumer.connect()
    await consumer.subscribe({ topic: topic, fromBeginning: true })
    
    await consumer.run({
      eachMessage: async ({ topic, partition, message }) => {
        await log(message.value.toString(),topic,partition) 

      },
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
  
function log(message,topic,partition ) {
    return new Promise((resolve) => {
        const f = new Date().toISOString()
        console.log(color.blue( f + ` Received message from "${topic}" topic`))
        console.log(color.yellow( message))
        resolve()
      })
   

}