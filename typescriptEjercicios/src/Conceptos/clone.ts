// Deep clone
// youtube https://www.youtube.com/watch?v=8Je1IylXLeo
import {Address, Customer} from "src/Entities";
// Usando CloneDeep Ejemplo de uso para clonar un cliente de manera profunda
const originalAddress = new Address("123 Main St", "Ciudad Ejemplo", "12345");
const originalCustomer = new Customer(1, "Juan", originalAddress);
export class CloneMethods {
  constructor() {}

  public static DeepCopy = () => {
    console.log("DeepCopy");
    const clonedCustomer = originalCustomer.clone();
    //En este ejemplo, hemos agregado métodos clone() a las clases Address y Customer para realizar una clonación profunda. El resultado es que clonedCustomer es una instancia completamente independiente de originalCustomer, incluida su dirección, lo que garantiza una clonación profunda de los objetos.
    // Verificamos que la clonación profunda funcionó
    console.log("originalCustomer === clonedCustomer ", originalCustomer === clonedCustomer); // Debería ser false (son instancias diferentes)
    console.log("originalCustomer.address === clonedCustomer.address ", originalCustomer.address === clonedCustomer.address); // Debería ser false (las direcciones son instancias diferentes)
  };

  public static Spreed_Prototype = () => {
    console.log(" Object.setPrototypeOf(clone, Object.getPrototypeOf(obj));");

    //const clonedCustomer = cloneObjectArrow<Customer>(originalCustomer);
    const clone = {...originalCustomer};

    Object.setPrototypeOf(clone, Object.getPrototypeOf(originalCustomer));
    console.log("clone === originalCustomer ", clone === originalCustomer);
    console.log("clone.address === originalCustomer.address ", clone.address === originalCustomer.address);
    console.log("clone");
    console.log(clone);
    console.log(clone);
  };

  public static Structured_Clone = () => {
    console.log("Using Structured Clone");
    const clone = structuredClone(originalCustomer);
    console.log("clone === originalCustomer ", clone === originalCustomer);
    console.log("clone.address === originalCustomer.address ", clone.address === originalCustomer.address);
    console.log("clone");
    console.log(clone);
  };
  public static Shadow_Copy = () => {
    //Shadow copy -> Se clonan los escalares pero los objetos complejos no. solo se pasan las referencias
    console.log("Using Shadow copy Object.assig");
    const clone = Object.assign(originalCustomer);
    console.log("clone === originalCustomer ", clone === originalCustomer);
    console.log("clone.address === originalCustomer.address ", clone.address === originalCustomer.address);
    console.log("clone");
    console.log(clone);
  };

  //SON.parse and JSON.stringify (Deep Copy):
  public static JSON_parse_strinfy = () => {
    console.log("Using JSON.parse and JSON.stringify  (Deep Copy)");
    const clone = JSON.parse(JSON.stringify(originalCustomer));
    console.log("clone === originalCustomer ", clone === originalCustomer);
    console.log("clone.address === originalCustomer.address ", clone.address === originalCustomer.address);
    console.log("clone");
    console.log(clone);
  };
}

/**
 * using setPrototypeOf & getPrototypeOf
 * @param obj
 * @returns
 */
const cloneObjectArrow = <T extends object>(obj: T) => {
  const clone = {...obj};

  Object.setPrototypeOf(clone, Object.getPrototypeOf(obj));
  return clone;
};

/**
 *  using setPrototypeOf & getPrototypeOf
 * @param obj
 * @returns
 */
function cloneObject<T extends Object>(obj: T) {
  const clone = {...obj};

  Object.setPrototypeOf(clone, Object.getPrototypeOf(obj));
  return clone;
}
