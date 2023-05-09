/**
 * Retornar array de impares de un array de numeros . 
 * @param numbers
 */
export const BuscarImpares = (numbers: number[]): number[] => {
  //The return value of the callback function (in the reduce ) is the accumulated result, and is provided as an argument
  // in the next call to the callback function.
    const oddArray = numbers.reduce((prev, current) => {
      if (current % 2 !== 0) prev.push(current);
      return prev;

    }, []);

  return oddArray;

};
/**
 *
 * @param numbers
 */
export const BuscarImparesMap = (numbers: number[]): number[] => {
    //The return value of the callback function (in the reduce ) is the accumulated result, and is provided as an argument
    // in the next call to the callback function.
      const oddArray = numbers.map((item) => {
        if (item % 2 !== 0) return item;
  
      }, []);
  
    return oddArray;
  
  };
