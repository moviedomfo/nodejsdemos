import { Schema, model } from 'mongoose';

export interface IMessageShema  {
  key:string ;
  topic:string;
  content:string ;
  type:number;
}


// 2. Create a Schema corresponding to the document interface.
const MessageShema = new Schema<IMessageShema>({
    content: { type: String, required: true },
    type:{ type: Number, required: true }

  },{
    versionKey:false,
    timestamps:true,
  });

  export default model('Messages',MessageShema);