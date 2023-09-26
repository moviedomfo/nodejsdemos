// dado un array de numeros encontrar aquel que aparece un numero impar de veses
// Retornar lista donde diuca el X (que es impar) aparece Y veses
// Solo hay un impar por array
const A1 = [7, 6, 4, 7, 458, 2, 200,7]; // => 7 aparece 3 veses

const store = {};
const buscarImpar = (number: number[]): number => {
  let foundValue; 

  number.forEach((element) => {
    if (element % 2 !== 0) {
      foundValue = element; // se almacena el valor encontrado
      return; // se sale del forEach()
    }
  });
  return foundValue;
};
const buscarRepeticiones = (numbers: number[], oddNumber): number => {
  const s = numbers.filter((p) => p === oddNumber);
  return s.length;
};
const buscarImparFromArrays = () => {
  const odd = buscarImpar(A1);

  if (odd) console.log(`El impar ${odd} se repite ${buscarRepeticiones(A1, odd)}`);
};

export default buscarImparFromArrays;
