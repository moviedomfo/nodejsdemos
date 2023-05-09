
import { personPubService } from '../common/Container';
import {NextFunction, Request,Response} from 'express';
import { ImessageDto } from "models/MessageDto";

export default class PersonPubController {
  
  constructor() {       }


  public async Provider( req: Request, res: Response, next: NextFunction) {
    try {
      const messageDTO: ImessageDto = req.body as ImessageDto;
       await personPubService.Provider(messageDTO);
       res.status(200).send();
    } catch (e) {
      next(e);
    }
  }

 
  public async Customer( req: Request, res: Response, next: NextFunction) {
    try {
      const messageDTO: ImessageDto = req.body as ImessageDto;
       await personPubService.Customer(messageDTO);
       res.status(200).send();
      //else res.status(403).send();
    } catch (e) {
      next(e);
    }
  }

 
 
  public async GetAll(req: Request, res: Response, next: NextFunction) {
    try {

      const result = await personPubService.GetAll();

      if (result) res.status(200).send(result);
      else res.status(204).send();
    } catch (e) {
      next(e);
    }
  }

  public async GetById( req: Request, res: Response, next: NextFunction) {
    try {
      const id =  req.params.id;
      const result = await personPubService.GetById(id);

      if (result) res.status(200).send(result);
      else res.status(204).send();
    } catch (e) {
      next(e);
    }
  }

  public async ClearAll(  req: Request, res: Response, next: NextFunction) {
    try {
      
      const result = await personPubService.ClearAll();
       res.status(200).send(true);
    } catch (e) {
      next(e);
    }
  }
}

export interface IPersonPubControlles{
  Customer (  req: Request, res: Response, next: NextFunction);
  Provider (  req: Request, res: Response, next: NextFunction);
   GetById( req: Request, res: Response, next: NextFunction);
   GetAll( req: Request, res: Response, next: NextFunction);
   ClearAll(req: Request, res: Response, next: NextFunction);
   
}
