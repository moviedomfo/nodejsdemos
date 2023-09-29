import { IChair, ISofa, ModernChair, ModernSofa, VictorianChair, VictorianSofa } from "./AbstractProductsForniture.js";

/**
 * Creator class or client class treats all the products as abstract Transport
 * The client knows that all transport objects are supposed to have the deliver
 */
export abstract class FurnitureFactory {
  abstract createChair(): IChair;
  abstract createSofa(): ISofa;
}

/**
 * Road uses Trunck
 */
export class ModernFurnitureFactory extends FurnitureFactory {
  createChair() {
    return new ModernChair();
  }

  createSofa() {
    return new ModernSofa();
  }
}
/**
 * SeaLogistic uses Ships
 */
export class VictorianFurnitureFactory extends FurnitureFactory {
  createChair() {
    return new VictorianChair();
  }

  createSofa() {
    return new VictorianSofa();
  }
}

