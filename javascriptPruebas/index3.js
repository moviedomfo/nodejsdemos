
// El método map() crea un nuevo array con los resultados de la llamada a la función indicada aplicados a cada uno de sus elementos.
//const products = require('./data.js');
import {products} from './data.js';

import chalk from 'chalk';
const max = 200
const min = 1200

// let listaNombre = products.map(producto =>  producto.name);console.log(chalk.yellow('----------------------map para recorrer data.js------------------'));

// products.map(p => 
//        console.log('El gusto es ' + p.name)
//      );

// console.log(listaNombre);

console.log(chalk.yellow('----------------------COPY VECTORES CON MAP Y MODIFICANDO RESULTADO------------------'));
console.log(chalk.yellow('---->arrayCopy'));
//Aqui ..item copia el registro completamente y le modifica name
const arrayCopy =  products.map(item=>{
        return {...item, name: item.name + "_modificado"}
    });

console.log( arrayCopy);
console.log(chalk.yellow('---->products original'));
console.log(products);

console.log(chalk.blue('----------------------COPY VECTORES CON MAP Y generacion de un nuevo vector O------------------'));

const arrayCopy2 =  products.map(item=>{
   const id = Math.floor(Math.random() * (max - min)) + min;
      return { name: item.name ,id: id, label: '_modificado'}
  });

console.log( arrayCopy2);





console.log('----------------------SUMAR VECTORES------------------');
const numerosA = [1,2,3,4,5,6];
const numerosB = [100,200,300];

// Este alg es mas largo pero valida Null en numerosB
const result = numerosA.map(Number).map( (item, ix) =>{

       let b = numerosB[ix] ? Number(numerosB[ix])  : 0;
       return item + b;

    } )
  .map( (item) => item.toString() );

//var result =  numerosA.map( (item, ix) => (Number(item) + Number(numerosB[ix])).toString() );

  // console.log("RESULT :" + result);
  // console.log("numerosA :" + numerosA);

let pares = numerosA.filter(p=>  p % 2 === 0);



let paresCopia =[];

pares.map(p=> {
  paresCopia.push(p)
});
pares[2] = 123;
console.log("PARES :" + pares);
console.log("paresCopia :" + pares);
