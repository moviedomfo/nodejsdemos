
import  container from "../common/Container";
import express  from "express";
//import comercePubController from "../controllers/comercePub2.controller";
//import ServiceFactory from "../services/ServiceFactory";


export const comerceRouter = express.Router();
//const comercePubControlles= new comercePubControlles(ServiceFactory.CreateIcomercePubService());
const comercePubController =  container.resolve('comercePubController');



comerceRouter.post("/product", comercePubController.Product);
comerceRouter.get("/",  comercePubController.GetAll);
comerceRouter.get("/:id",  comercePubController.GetById);


