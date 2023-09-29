//Abstract Factory es un patrón de diseño creacional que nos permite producir familias de objetos
//                 relacionados sin especificar sus clases concretas.

// Familia de porductos (objetos a crear) Silla y Sofa
// IChair, ISofa
// Fábrica abstracta. --> IFornitureFactory
// Necesitamos una forma de crear objetos individuales de mobiliario para que combinen con otros objetos de la misma familia.
// Las clases concretas para los productos:
//  -VictorianChair
//  -VictorianSofa
//  -ModernChair
//  -ModernSofa
// Las fábricas concretas para las fábricas
//  -VictorianFurnitureFactory
//  -ModernFurnitureFactory

// Finalmente, un cliente puede crear y utilizar muebles victorianos o modernos utilizando
// las fábricas correspondientes.
// Este es un ejemplo básico del patrón Abstract Factory en TypeScript.
// Puedes expandirlo según tus necesidades específicas y agregar más productos y fábricas si es necesario.

import {FurnitureFactory, ModernFurnitureFactory, VictorianFurnitureFactory} from "./CreatorLogistic";
const victorianFactory = new VictorianFurnitureFactory();
const modernFactory = new ModernFurnitureFactory();

// Paso 4: Uso del patrón Abstract Factory
function createAndUseFurniture(factory: FurnitureFactory) {
  const chair = factory.createChair();
  const sofa = factory.createSofa();

  chair.SitOn();
  sofa.Relax();

  //Common methods
  chair.HasLegs();
  chair.HasLegs();
}

console.log("Cliente eligiendo muebles victorianos:");
createAndUseFurniture(victorianFactory);

console.log("\nCliente eligiendo muebles modernos:");
createAndUseFurniture(modernFactory);
