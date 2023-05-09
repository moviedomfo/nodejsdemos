import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ChatComponent } from './chat/chat.component';
import { SocketIoModule, SocketIoConfig } from 'ngx-socket-io';
import { environment } from 'src/environments/environment';
import { FormsModule } from '@angular/forms';
import { ChatService } from './chat-service.service';


const config: SocketIoConfig = {
	url: environment.socketUrl, // socket server url;
	options: {
		transports: ['websocket']
	}
}

@NgModule({
  declarations: [
    AppComponent,
        ChatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,		SocketIoModule.forRoot(config),


  ],
  providers: [ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }
