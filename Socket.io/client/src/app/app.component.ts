import { Component } from '@angular/core';
import { AppConstants } from './common.constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Socket.io client (' + AppConstants.AppProducion + ')';
  version =AppConstants.AppVersion;
  constructor(){

  }
}
