import { ImessageDto } from '../models/MessageDto';
import comerceShema, { IMessageShema } from '../models/message.schema';
import { Kafka ,KafkaConfig, Partitioners} from 'kafkajs';

export interface IComercePubService{
  Product : (req:ImessageDto)=> Promise<void> ;
  GetById:(id:string)=> Promise<IMessageShema>;
  GetAll:()=> Promise<IMessageShema[]> ;

  

}

// @Route("comercePubService")
export default class ComercePubService implements IComercePubService {



  // @Post("/message")
  public async Product(req:ImessageDto): Promise<void> {
      pushToQueue(req);
  }


  // @Get("/getById")
  public async GetById(id:string): Promise<IMessageShema> {
    return  getById(id);
  }


  // @Get("/getAll")
  public async GetAll(): Promise<IMessageShema[]> {
    return  getAll();
  }


 
}


const pushToQueue = async (req: ImessageDto) => {
  
  const kconfig:KafkaConfig={
    brokers:['localhost:9092'], 
    ssl: false,
    clientId:req.origin,

}
  // this is the topic to which we want to write messages
  const kafka = new Kafka(kconfig);

  const producer = kafka.producer({ createPartitioner: Partitioners.DefaultPartitioner });
  
  try {
    
    await producer.connect();
    console.log("Sendng message " + req.key );

    await producer.send({
      topic: "products",
      messages: [
        {
          key: req.key,
          value: JSON.stringify(req.content),
        },
      ],
    });
  } catch (err) {
    console.log("*****************************Error from KAFKA *****************************        ");
    console.log("Error from KAFKA         " + JSON.stringify(err));
    console.log("*****************************Fin ERROR  KAFKA *****************************        ");
  }
};



 const getById = async (id:string): Promise<IMessageShema> => {

  return new Promise<IMessageShema>((resolve, reject) => {

    const res =  comerceShema.findById(id)

      resolve(res);

  });
  
}

 const getAll = async (): Promise<IMessageShema[]> => {

  return new Promise<IMessageShema[]>((resolve, reject) => {

    // const res =  comerceShema.find();
    const res :IMessageShema []= [  
      {key:"122", topic:"tweet",content:"Yes, your example of",type:1},  
      {key:"1232", topic:"fase",content:"Yes, your example of",type:2},  
      {key:"1342322", topic:"tweet",content:"Yes, your example of",type:1},  

  ] ;

    resolve(res);

  });
  
}




