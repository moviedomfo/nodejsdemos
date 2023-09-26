// En este ejemplo, hemos definido las interfaces IChair e ISofa para los productos y la interfaz FurnitureFactory para la
// fábrica abstracta.
// Luego, implementamos las clases concretas para los productos
// (VictorianChair, VictorianSofa, ModernChair, ModernSofa) y
// las fábricas concretas (VictorianFurnitureFactory y ModernFurnitureFactory).
// Finalmente, mostramos cómo un cliente puede crear y utilizar muebles victorianos o modernos utilizando
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
