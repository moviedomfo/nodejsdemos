
var dayjs = require('dayjs')
var localeData = require('dayjs/plugin/localeData')
dayjs.extend(localeData)
require('dayjs/locale/es')
// import 'dayjs/locale/de' // ES 2015 

dayjs.locale('es') // use locale globally
dayjs().locale('es').format() // use locale in a specific instance
import chalk from 'chalk';



var local =  dayjs();


console.log(chalk.yellow(local));

console.log(chalk.yellow('---------------metodos-----------------'));

console.log(chalk.red('toJSON              : ') +  chalk.blue(local.toJSON()));
console.log(chalk.red('toISOString ISO 8601: ') +  chalk.blue(local.toISOString()));

console.log(chalk.red('Unix timestamp      : ') +  chalk.blue(local.unix()));


//var d = DateTime.fromISO('2014-08-06T13:07:04.054').setLocale('es');
//console.log(chalk.red('setLocale : ') +  chalk.blue(d.toLocaleString(DateTime.DATETIME_FULL)));

console.log(chalk.yellow('---------------meses------------------'));
console.log( chalk.blue(dayjs.months()));

console.log(chalk.yellow('---------------text date------------------'));
console.log(local.format("dddd, MMMM D YYYY"));
console.log(local.format("YYYY-MM-DD"));

console.log(chalk.yellow('---------------Time------------------'));
console.log(local.format("HH:mm:ss"));
console.log(local.format("HH:mm:ss a"));
console.log(local.format("HH:mm"));

// console.log(chalk.yellow('---------------ISO 8601------------------'));

// console.log(chalk.yellow('---------------------------------'));




