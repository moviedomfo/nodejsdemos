
import chalk from 'chalk';

// using spread operator to clone array

const numeros = [1,2,3,4,5,6];
const letras = ['A','B','C','D','E'];

console.log(chalk.cyan("--------------numeros----------------"));
console.log();

console.log(numeros);

console.log();


const arrayCopy = [...numeros];
arrayCopy[0] = 20000;
arrayCopy[4] = 4222.5;



console.log(chalk.cyan("--------------arrayCopy = [...numeros] Y LE AGREGAMOS ELEMENTOS----------------"));
console.log('arrayCopy[0] = 20000');
console.log('arrayCopy[4] = 4222.5');
console.log();
console.log(arrayCopy);
console.log();

console.log(chalk.cyan("-------------- [...numeros,100,233,500]----------------"));
console.log();
// creamos un array con copia de numeros y le pasamos 3 elementos mas
const arrayCopy2 = [...numeros,100,233,500];
console.log(`${chalk.yellow("arrayCopy2 = ")} ${arrayCopy2}`);
console.log();


// console.log(chalk.yellow("******************** OPERACIONES CON ARREGLOS*********************************"));
console.log(chalk.cyan("--------------[arrayCopy + arrayCopy2] CONTACT ALL IN STRING arrayUnion ----------------"));console.log();
const arrayUnion = [arrayCopy + arrayCopy2];
console.log(arrayUnion);
console.log();

console.log(chalk.cyan("--------------[...arrayCopy, ...arrayCopy2] Copiamos los elementos del vector 2 al primeros----------------"));console.log();
const sumOfArrays = [...arrayCopy, ...arrayCopy2];

console.log(sumOfArrays);
console.log();
console.log(chalk.cyan("-------------- [arrayCopy , arrayCopy2]; aqui generamos una matriz con dos vectores ----------------"));
const arrayDoble = [arrayCopy , arrayCopy2];
console.log(arrayDoble);


console.log(chalk.cyan("--------------  [...numeros,letras] Aqui metermos el segundo vector como elemento del primero----------------"));
console.log();
const arrayNumeresLetras = [...numeros,letras];
console.log();
console.log(arrayNumeresLetras);

console.log(chalk.cyan("--------------(numeros)=>[...numeros,letras] Asignamos----------------"));
console.log();
const vectorAsing = (numeros)=>[...numeros,'X']
console.log();
console.log(vectorAsing);
// const arr1= [2,'H1',5];
// [1,1,1,1,1,1] = arr1;
// console.log();