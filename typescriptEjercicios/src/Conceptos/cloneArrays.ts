
import { animales } from "src/Entities";

export class CopyArrayMethods {
  constructor() { }

  /**
    **This also assigns objects/arrays by reference instead of by value. 
   */
  public static ArrayMap = () => {
    console.log("Array map");
    const animals_copy = animales.map(a => a);

    console.log(JSON.stringify(animals_copy));
  };

  /**This also assigns objects/arrays by reference instead of by value. */
  public static Spreed_Copy = () => {

    console.log("Array map");
    const animals_copy = { ...animales };

    console.log(JSON.stringify(animals_copy));
  };


  /**This also assigns objects/arrays by reference instead of by value. */
  public static Shadow_Copy_filter = () => {
    //Shadow copy -> Se clonan los escalares pero los objetos complejos no. solo se pasan las referencias
    console.log("Shadow copy using map");
    const animals_copy = animales.filter((x) => x);

    console.log(JSON.stringify(animals_copy));
  };

  /**This also assigns objects/arrays by reference instead of by value. */
  public static Shadow_Copy_reduce = () => {
    //reduce transforms an initial value as it loops through a list
    //Here the initial value is an empty array, and we’re filling it with each element as we go. 
    //That array must be returned from the function to be used in the next iteration.
    console.log("Shallow copy using reduce");
    const animals_copy = animales.reduce((newArray, element) => {
      newArray.push(element);

      return newArray;
    }, []);

    console.log(JSON.stringify(animals_copy));
  };


  /**This also assigns objects/arrays by reference instead of by value. */
  public static Shadow_Copy_slice = () => {
    //slice returns a shallow copy of an array based on the provided start/end index you provide.
    console.log("Shadow copy using slice");
    const animals_copy = animales.slice(1, 2);

    console.log(JSON.stringify(animals_copy));
  };

  /**JSON.parse and JSON.stringify (Deep Copy): */
  public static JSON_parse_strinfy = () => {
    console.log("Using JSON.parse and JSON.stringify  (Deep Copy)");
    const clone = JSON.parse(JSON.stringify(animales));

    console.log(clone);
  };
}



