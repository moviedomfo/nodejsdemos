import dayjs from "dayjs";
dayjs.locale('es') // use locale globally
dayjs().locale('es').format(); // use locale in a specific instance

const JWT_ExpiresRefreshToken = 40;
let currentDate = dayjs();
let expireAt = dayjs(currentDate).add(JWT_ExpiresRefreshToken, "minute");

console.log('Fecha hoy       : ' + currentDate.format('YYYY-MM-DDTHH:mm:ss'));
console.log('Fecha expiracion : ' + expireAt.format('YYYY-MM-DDTHH:mm:ss'));

const isAfter = expireAt.isAfter(dayjs(currentDate));
console.log(isAfter)
if (isAfter)
    console.log('todavia no expiro  ');
else
    console.log('Ya expiro');

console.log('currentDate epoc :        ' + currentDate.valueOf());
console.log('expireAt epoc    :        ' + expireAt.valueOf());
// const currentDateUTC = new Date().toUTCString();
// console.log('currentDateUTC :        ' + currentDateUTC);
// const currentDateUTC_toDate = new Date(currentDateUTC);
// console.log('currentDateUTC_toDate : ' + currentDateUTC);
