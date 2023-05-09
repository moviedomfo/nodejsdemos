import chalk from 'chalk';
import {products, sociosMutual} from './data.js';

// Uso de Spreead operator

console.log("--------------{...object,}----------------".blue);
const person = { id: 5001, name: "Lemoneto" , age: 12};
const personCopy = {...person,name: " GranuxCa"};
console.log(chalk.green(person));
console.log(chalk.green(personCopy));



console.log("--------------MERGE---------------".blue);
//Aquí, el operador spread no trabaja como uno podría esperar: este dispersa un arreglo de argumentos en el literal Tipo Objeto, debido al parámetro rest.
let obj1 = { foo: 'bar', x: 42 };
let obj2 = { foo: 'baz', y: 13 };
const merge = ( ...obj1 ) => ( { ...obj2 } );
var mergedObj = merge ( obj1, obj2);
console.log(chalk.yellow(mergedObj));
return;
console.log(chalk.cyan("--------------Uso de Spreead operator----------------"));

//using spread operator
//const arrayCopy = [...prod];
const arrayCopy = [...prod,  {id: 200 , name:"Cereza"} ];

// arrayCopy.map(i=>{
//     //i.name = {...i, name  : i.name + "_modificado"};
//     i.name =  i.name + "_modificado";
// });
arrayCopy[0].id = 20000;
console.log(chalk.cyan("--------------Prod----------------"));
console.log(prod);
console.log(chalk.cyan("--------------Prod arrayCopy----------------"));
console.log(arrayCopy);

//console.log(JSON.stringify(array).green);
