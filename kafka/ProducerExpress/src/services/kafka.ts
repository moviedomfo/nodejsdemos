import { Kafka ,KafkaConfig} from 'kafkajs';

import {SchemaRegistry} from '@kafkajs/confluent-schema-registry';
import { IMessageShema } from '../models/message.schema';


const sendMessageToTopic = async (message:IMessageShema) => {

    //brokers:['kafka1:9092', 'kafka2:9092']
    const kconfig:KafkaConfig={
        brokers:['localhost:9092'], 
        ssl: false,
        clientId:'comerce0001',

    }
   
   const kafkaClient :Kafka= new  Kafka( kconfig);

   await kafkaClient.producer().connect();
   await kafkaClient.producer().send({
    topic: 'test-topic',
    messages: [
      { value: 'Hello KafkaJS user!' },
    ],
  });

//    const SchemaRegistry
//    const encodedPayload = await re
    
  }