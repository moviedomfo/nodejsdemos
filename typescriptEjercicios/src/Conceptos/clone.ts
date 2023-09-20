// Deep clone
// youtube https://www.youtube.com/watch?v=8Je1IylXLeo
import {Address, Customer} from "src/Entities";
// Usando CloneDeep Ejemplo de uso para clonar un cliente de manera profunda
const originalAddress = new Address("123 Main St", "Ciudad Ejemplo", "12345");
const originalCustomer = new Customer(1, "Juan", originalAddress);
export class CloneMethods {
  constructor() {}

  public static DeepBlue = () => {
    console.log("DeepBlue");
    const clonedCustomer = originalCustomer.clone();
    //En este ejemplo, hemos agregado métodos clone() a las clases Address y Customer para realizar una clonación profunda. El resultado es que clonedCustomer es una instancia completamente independiente de originalCustomer, incluida su dirección, lo que garantiza una clonación profunda de los objetos.
    // Verificamos que la clonación profunda funcionó
    console.log("originalCustomer === clonedCustomer ", originalCustomer === clonedCustomer); // Debería ser false (son instancias diferentes)
    console.log("originalCustomer.address === clonedCustomer.address ", originalCustomer.address === clonedCustomer.address); // Debería ser false (las direcciones son instancias diferentes)
  };

  public static Spreed_Prototype = () => {
    console.log(" Object.setPrototypeOf(clone, Object.getPrototypeOf(obj));");

    const clonedCustomer = cloneObjectArrow<Customer>(originalCustomer);
    console.log(clonedCustomer);
  };

  public static Structured_Clone = () => {
    console.log("Usando Structured Clone");
    const otroClienteClonado1 = structuredClone(originalCustomer);
    console.log("otroClienteClonado1 === originalCustomer ", otroClienteClonado1 === originalCustomer);
  };
  public static Shadow_Copy = () => {
    //Shadow copy -> Se clonan los escalares pero los objetos complejos no. solo se pasan las referencias
    console.log("usando Shadow copy Object.assig");
    const otroClienteClonado2 = Object.assign(originalCustomer);
    console.log("otroClienteClonado2 === originalCustomer ", otroClienteClonado2 === originalCustomer);
    console.log("otroClienteClonado");
    console.log(otroClienteClonado2);
    console.log("originalCustomer");
    console.log(originalCustomer);
  };
}

const cloneObjectArrow = <T extends object>(obj: T) => {
  const clone = {...obj};

  Object.setPrototypeOf(clone, Object.getPrototypeOf(obj));
  return clone;
};
function cloneObject<T extends Object>(obj: T) {
  const clone = {...obj};

  Object.setPrototypeOf(clone, Object.getPrototypeOf(obj));
  return clone;
}

console.log("usando Spread operator funciona igual pero agregamos uso de setPrototypeOf y getPrototypeOf");
const otroClienteClonado3_spread = {...originalCustomer};
Object.setPrototypeOf(otroClienteClonado3_spread, Object.getPrototypeOf(originalCustomer));

console.log(otroClienteClonado3_spread);
