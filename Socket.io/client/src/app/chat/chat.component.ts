import { IMessage } from './../IMessage';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChatService } from '../chat-service.service';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import {  Subject } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public newMessage: string = '';
  //userQuestionUpdate = new Subject<string>();
  userMessageEvent = new Subject<KeyboardEvent>();

  public messageList: IMessage[] = [];
  constructor(private chatService: ChatService) { }

  ngOnInit() {
    this.chatService.getNewMessage().subscribe((message: IMessage) => {
      this.messageList.push(message);
    });

     // Debounce used by input box.
     this.userMessageEvent.pipe(
      debounceTime(700),
      distinctUntilChanged()).subscribe($event => {
        this.inputKeyUp($event);


      });
  }


    inputKeyUp(event: { key: string; }){
    if (event.key === "Enter")
      this.sendMessage();
  }

  sendMessage() {

    console.log(this.newMessage);
    this.chatService.sendMessage(this.newMessage);
    //this.consoleMessages.push(this.newMessage);
    this.newMessage = '';
  }





}
