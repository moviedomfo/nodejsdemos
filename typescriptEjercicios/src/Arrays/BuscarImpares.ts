/**
 * Retornar array de impares de un array de numeros . 
 * @param numbers
 */
export const BuscarImpares_reduce = (numbers: number[]): number[] => {
  //The return value of the callback function (in the reduce ) is the accumulated result, and is provided as an argument
  // in the next call to the callback function.
  const oddArray = numbers.reduce((prev, current) => {
    if (current % 2 !== 0) prev.push(current);
    return prev;

  }, []);

  return oddArray;

};

/**
 *Retornar array de impares de un array de numeros . 
 * @param numbers
 */
export const BuscarImpares_map = (numbers: number[]): number[] => {
  //The return value of the callback function (in the reduce ) is the accumulated result, and is provided as an argument
  // in the next call to the callback function.
  const oddArray = numbers.map((item) => {
    if (item % 2 !== 0) return item;

  }, []);

  return oddArray;

};
/**
* Retorna el primer impar de un array de nros
* @param number 
* @returns 
*/
export const buscarImpar_forEach = (number: number[]): number => {
  let foundValue;

  number.forEach((element) => {
    if (element % 2 !== 0) {
      foundValue = element; // se almacena el valor encontrado
      return; // se sale del forEach()
    }
  });
  return foundValue;
};