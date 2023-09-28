// permite copiar objetos existentes sin que el código dependa de sus clases.
// Solución --> La clonacion
// El patrón declara una interfaz común para todos los objetos que soportan la clonación.
// Esta interfaz nos permite clonar un objeto sin acoplar el código a la clase de ese objeto.
// Normalmente, dicha interfaz contiene un único método

import Circle from "./Circle";
import Rectangle from "./Rectangle";
import {Shape} from "./Shape";

// Utiliza el patrón Prototype cuando tu código no deba depender de las clases concretas de objetos que necesites copiar.

const shapes: Shape[] = [];

const circle: Circle = new Circle();
circle.x = 10;
circle.y = 10;
circle.radius = 20;
shapes.push(circle);
const anotherCircle = circle.clone();
shapes.push(anotherCircle);

const rectangle: Rectangle = new Rectangle();
rectangle.width = 10;
rectangle.height = 20;
shapes.push(rectangle);

console.log(shapes);
