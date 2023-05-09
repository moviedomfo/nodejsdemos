
import { comercePubService } from '../common/Container';
import {NextFunction, Request,Response} from 'express';
import { ImessageDto } from "models/MessageDto";

export default class ComercePubController {
  
  constructor() {       }

  public async Product( req: Request, res: Response, next: NextFunction) {
    try {
      const productDTO: ImessageDto = req.body as ImessageDto;
       await comercePubService.Product(productDTO);
       res.status(200).send();
      //else res.status(403).send();
    } catch (e) {
      next(e);
    }
  }

 
  public async GetAll(req: Request, res: Response, next: NextFunction) {
    try {

      const result = await comercePubService.GetAll();

      if (result) res.status(200).send(result);
      else res.status(204).send();
    } catch (e) {
      next(e);
    }
  }

  public async GetById( req: Request, res: Response, next: NextFunction) {
    try {
      const id =  req.params.id;
      const result = await comercePubService.GetById(id);

      if (result) res.status(200).send(result);
      else res.status(204).send();
    } catch (e) {
      next(e);
    }
  }

  public async ClearAll(  req: Request, res: Response, next: NextFunction) {
    try {
      
      const result = await comercePubService.ClearAll();
       res.status(200).send(true);
    } catch (e) {
      next(e);
    }
  }
}

export interface IcomercePubControlles{
   Push (  req: Request, res: Response, next: NextFunction);
   GetById( req: Request, res: Response, next: NextFunction);
   GetAll( req: Request, res: Response, next: NextFunction);
   ClearAll(req: Request, res: Response, next: NextFunction);
   
}
