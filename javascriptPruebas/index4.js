

import chalk from 'chalk';
import {products, sociosMutual} from './data.js';

console.log(sociosMutual);
console.log(JSON.stringify(sociosMutual));

// console.log(products);



console.log(chalk.red('----------------------COPY VECTORES Splice------------------'));
const products1 = products.slice();
console.log(chalk.yellow(products1));
products[0].name = "Modificado 1";
console.log(chalk.yellow(products1));

console.log(chalk.red('----------------------COPY VECTORES Referencia------------------'));
const products2 = products;
console.log(chalk.green(products2));
products[0].name = "Modificado 2";
console.log(chalk.green(products2));



console.log(chalk.red('----------------------COPY VECTORES spread------------------'));
const products3 = [...products];
console.log(chalk.gray(products3));
products[0].name = "Modificado 3";
console.log(chalk.gray(products3));


console.log(chalk.red('----------------------COPY VECTORES JSON------------------'));
const products4 = JSON.parse( JSON.stringify( products ) );
console.log(chalk.gray(products4));
products[0].name = "Modificado 4";
console.log(chalk.gray(products4));