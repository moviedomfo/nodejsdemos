//Builder is a creational design pattern that lets you construct complex objects step by step
//The pattern allows you to produce different types and representations of an object using the same construction code.

import ISoftwareBuilder, {WebAppBuilder, DeskAppBuilder} from "./SoftwareBuilder";

const softwareDeveloper_Desk = new DeskAppBuilder();
const softwareDeveloper_web = new WebAppBuilder();

// DirectordApp  Es una clase director que conduce la creacion paso a paso de los productos basandoce en 
// los metodos o pasos q ofrece nuestro Builder
// Build a web app
DirectordApp(softwareDeveloper_web);
// Build a desktop app
DirectordApp(softwareDeveloper_Desk);

function DirectordApp(builder: ISoftwareBuilder) {
  builder.plan();
  builder.design();
  builder.implement();
  builder.test();
  builder.deploy();
}
