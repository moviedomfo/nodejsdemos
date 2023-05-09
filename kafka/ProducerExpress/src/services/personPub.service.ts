import { ImessageDto } from "../models/MessageDto";
import { Kafka, KafkaConfig, Partitioners } from "kafkajs";
import MessageShema, { IMessageShema } from "../models/message.schema";
import { AppConstants } from "../common/commonConstants";

export interface IPersonPubService {
  Customer: (req: ImessageDto) => Promise<void>;
  Provider: (req: ImessageDto) => Promise<void>;
  GetById: (id: string) => Promise<IMessageShema>;
  GetAll: () => Promise<IMessageShema[]>;
  ClearAll: () => Promise<void>;
}
const kconfig:KafkaConfig={
  brokers: AppConstants.Brokers,
  ssl: false,
  clientId: AppConstants.ClientId };
// @Route("PersonPubService")
export default class PersonPubService implements IPersonPubService {
  // @Post("/Client")
  public async Customer(req: ImessageDto): Promise<void> {
    try {
      await pushToQueue(req,"customers");
    } catch (err) {
      console.log("push err  " + JSON.stringify(err));
    }
  }

  public async Provider(req: ImessageDto): Promise<void> {
    try {
      await pushToQueue(req,"providers");
    } catch (err) {
      console.log("push err  " + JSON.stringify(err));
    }
  }

  // @Get("/getById")
  public async GetById(id: string): Promise<IMessageShema> {
    return getById(id);
  }

  // @Get("/getAll")
  public async GetAll(): Promise<IMessageShema[]> {
    return getAll();
  }

  // @Get("/clearAll")j
  public async ClearAll(): Promise<void> {
    return clearAll();
  }
}

const pushToQueue = async (req: ImessageDto,topic :string) => {

  
  try {
    const kafka = new Kafka(kconfig);
    const producer = kafka.producer({createPartitioner: Partitioners.DefaultPartitioner});
    await producer.connect();
  
      await producer.send({
      topic,
      messages: [
        {
          key: req.key,
          value: JSON.stringify(req.content),
        },
      ],
    });
    console.log(`message wa sended to TOPIC ${topic} from ${req.origin}`);
  } catch (err) {
    console.log(
      "*****************************Error from KAFKA *****************************        "
    );
      console.log( err.message  );
      
    console.log(
      "*****************************End ERROR  KAFKA *****************************        "
    );
  }
};

const getById = async (id: string): Promise<IMessageShema> => {
  return new Promise<IMessageShema>((resolve, reject) => {
    const res = MessageShema.findById(id);

    resolve(res);
  });
};

const getAll = async (): Promise<IMessageShema[]> => {
  return new Promise<IMessageShema[]>((resolve, reject) => {
    // const res =  PersonShema.find();
    const res: IMessageShema[] = [
      { key: "122", topic: "tweet", content: "Yes, your example of", type: 1 },
      { key: "1232", topic: "fase", content: "Yes, your example of", type: 2 },
      {
        key: "1342322",
        topic: "tweet",
        content: "Yes, your example of",
        type: 1,
      },
    ];

    resolve(res);
  });
};

const clearAll = async (): Promise<void> => {
  await MessageShema.collection.deleteMany({});
};

