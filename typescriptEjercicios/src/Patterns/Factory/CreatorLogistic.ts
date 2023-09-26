import ITransportMedia from "./ITransportMedia";
import {Ships, Trunck} from "./AbstractProducts";

/**
 * Creator class or client class treats all the products as abstract Transport
 * The client knows that all transport objects are supposed to have the deliver
 */
export abstract class Logistic {
  abstract createTransport(): ITransportMedia;

  planDelivery() {
    const transport = this.createTransport();
    console.log("Planificando la entrega...");
    transport.Deliver();
  }
}

/**
 * Road uses Trunck
 */
export class RoadLogistic extends Logistic {
  createTransport() {
    return new Trunck();
  }
}
/**
 * SeaLogistic uses Ships
 */
export class SeaLogistic extends Logistic {
  createTransport() {
    return new Ships();
  }
}

export default Logistic;
