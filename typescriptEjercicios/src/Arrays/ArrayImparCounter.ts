// dado un array de numeros encontrar aquel que aparece un numero impar de veses
// Retornar lista donde diuca el X (que es impar) aparece Y veses

import { buscarImpar_forEach } from "./BuscarImpares";

// Solo hay un impar por array
const A1 = [7, 6, 4, 7, 458, 2, 200, 7]; // => 7 aparece 3 veses

const store = {};




/**
 * Busca repedidos en un array de nros. utilizando filter para obtener la cantidad de elementos oddNumber
 * @param numbers Array
 * @param oddNumber nro 
 * @returns 
 */
const buscarRepeticiones = (numbers: number[], nro): number => {
  const s = numbers.filter((p) => p === nro);
  return s.length;
};
const buscarImparFromArrays = () => {
  const nro = buscarImpar_forEach(A1);

  if (nro) console.log(`El nro ${nro} se repite ${buscarRepeticiones(A1, nro)}`);
};

export default buscarImparFromArrays;
