
import {
    createContainer,
    asClass,
    InjectionMode,
  } from 'awilix';


import ComercePubController from '../controllers/comercePub.controller';
import ComercePubService from "../services/comercePub.service";
import PersonPubController from '../controllers/pesonPub.controller';
import PersonPubService from '../services/personPub.service';


const container = createContainer({
    injectionMode: InjectionMode.PROXY,
  });
  
  container.register({
     comercePubService: asClass(ComercePubService).scoped(),
     comercePubController : asClass(ComercePubController).scoped(),
     personPubService : asClass(PersonPubService).scoped(),
     personPubController : asClass(PersonPubController).scoped()
  });
  export default container; 
  export const  comercePubController = container.resolve('comercePubController');
  export const  comercePubService = container.resolve('comercePubService');

  export const  personPubController = container.resolve('personPubController');
  export const  personPubService = container.resolve('personPubService');


