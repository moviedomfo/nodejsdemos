//Builder is a creational design pattern that lets you construct complex objects step by step
//The pattern allows you to produce different types and representations of an object using the same construction code.

import ISoftwareBuilder, {WebAppBuilder, DeskAppBuilder} from "./SoftwareBuilder";

const softwareDeveloper_Desk = new DeskAppBuilder();
const softwareDeveloper_web = new WebAppBuilder();

// Build a web app
BuildApp(softwareDeveloper_web);
// Build a desktop app
BuildApp(softwareDeveloper_Desk);

function BuildApp(builder: ISoftwareBuilder) {
  builder.plan();
  builder.design();
  builder.implement();
  builder.test();
  builder.deploy();
}
