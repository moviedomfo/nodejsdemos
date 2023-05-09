
import  container from "../common/Container";
import express  from "express";
export const personRouter = express.Router();
const personPubController =  container.resolve('personPubController');



personRouter.post("/provider", personPubController.Provider);
personRouter.post("/customer", personPubController.Customer);
personRouter.get("/",  personPubController.GetAll);
personRouter.get("/:id",  personPubController.GetById);


