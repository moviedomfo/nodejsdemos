//Interface para un conjunto relacionado de productos
import ITransportMedia from "./ITransportMedia";

// Paso 2: Implementar las clases concretas del producto (Trunck y Ships)
export class Trunck implements ITransportMedia {
    Deliver() {
      console.log("Entregando por carretera (Trunck)");
    }
  }
  export class Ships implements ITransportMedia {
    Deliver() {
      console.log("Entregando por mar (Ships)");
    }
  }