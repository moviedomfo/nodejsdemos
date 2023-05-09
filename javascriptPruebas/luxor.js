const { DateTime } = require("luxon");
const {Settings} =  require("luxon");
import chalk from 'chalk';

Settings.defaultLocale = 'es';

var local = DateTime.local().setLocale('es');

DateTime.now()
  .setLocale("es")
  .toLocaleString(DateTime.DATE_FULL); 

console.log(chalk.yellow(DateTime.now().setLocale('es').toLocaleString(DateTime.DATETIME_FULL)));

console.log(chalk.yellow('---------------DateTime.local()-----------------'));
console.log(chalk.red('local.toString() : ') + chalk.blue(local.toString()));
console.log(chalk.red('local.zoneName : ') + chalk.blue(local.zoneName));

console.log(chalk.red('local DATE_HUGE : ') + chalk.blue(local.toLocaleString(DateTime.DATE_HUGE)));
console.log(chalk.red('local DATETIME_FULL : ') + chalk.blue(local.toLocaleString(DateTime.DATETIME_FULL)));
console.log(chalk.red('local DATETIME_SHORT_WITH_SECONDS : ') + chalk.blue(local.toLocaleString(DateTime.DATETIME_SHORT_WITH_SECONDS)));

console.log(chalk.yellow('----------------setZone-----------------'));
let fecha = local.setZone('America/Buenos_Aires').toLocaleString();
console.log(chalk.red('setZone America/Buenos_Aires : ') +  chalk.blue(fecha));
let fecha_toISO = local.setZone('America/Buenos_Aires').toISO(DateTime.DATETIME_FULL);
console.log(chalk.red('setZone America/Buenos_Aires toISO: ') +  chalk.blue(fecha_toISO));

var d = DateTime.fromISO('2014-08-06T13:07:04.054').setLocale('es');
console.log(chalk.red('setLocale : ') +  chalk.blue(d.toLocaleString(DateTime.DATETIME_FULL)));

console.log(chalk.yellow('---------------DateTime.fromISO------------------'));
var iso = DateTime.fromISO(local.toString());
console.log(chalk.red('iso.toString : ') + chalk.blue(iso.toString()));
console.log(chalk.red('iso.zoneName : ') + chalk.blue(iso.zoneName));

var d = DateTime.fromISO(local.toString()).toFormat('yyyy-MM-dd HH-mm-ss');
console.log(chalk.red('iso formateado : ') + chalk.blue(d.toString()));
console.log(chalk.yellow('---------------------------------'));


console.log(chalk.yellow('---------------ISO 8601------------------'));
console.log(chalk.red('local.toISO : ') + chalk.blue(local.toISO()));
console.log(chalk.red('local.toISODate : ') + chalk.blue(local.toISODate()));
console.log(chalk.yellow('---------------------------------'));





const fechaManual = '2021-01-22';
const convertida =  DateTime.fromISO(fechaManual+'T13:00:00.00');

console.log(chalk.yellow('FECHA :' + fechaManual + ' --> ' + convertida.toSQLDate()  ));


johonmircha
