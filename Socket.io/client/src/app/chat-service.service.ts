import { EventEmitter, Injectable } from '@angular/core';
import { Socket } from 'ngx-socket-io';
import { BehaviorSubject } from 'rxjs';
import { IMessage } from './IMessage';


@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public newItemValue = new EventEmitter<string>();
  public message$: BehaviorSubject<IMessage> = new BehaviorSubject({text:'',type:'message'});

  constructor(private socket: Socket) { }

  sendMessage(message?:string){
    this.socket.emit('add-message', message);
  }


  getNewMessage() {
    this.socket.on('message', (message?:IMessage) =>{
      if(message)
        this.message$.next(message);
        //this.newItemValue.emit(message.text)
    });
    return this.message$.asObservable();
  }
}
