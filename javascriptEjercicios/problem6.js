var persons = [
  { name: 'Edward', value: 21 },
  { name: 'Sharpe', value: 37 },
  { name: 'And', value: 45 },
  { name: 'The', value: -12 },
  { name: 'Magnetic', value: 13 },
  { name: 'Zeros', value: 37 }
];


// aqui el valor inicial de acc es un objeto vacio
// Crea un nuevo vector donde pone nombre: {el objeto }
const resultIndexed = persons.reduce((acc,el)=>({
  ...acc,[el.name]: el
}),{});

console.log(resultIndexed);
