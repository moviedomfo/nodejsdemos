const arr = [1, 2, 3, 'b', 2, 'a', 3, 4];
/**
 * Esta es la manera fasil
 */
const uniqueArr = [...new Set(arr)];



const arr3 = [[1, 2], 3, ['b', 2], ['a', 3], [1, 2]];

/**
 * Convierte cada arra en un json elimina los duplicados y retorna el resultado
 */
const uniqueArr2 = Array.from(new Set(arr3.map(JSON.stringify()), JSON.parse));
console.log(uniqueArr2)