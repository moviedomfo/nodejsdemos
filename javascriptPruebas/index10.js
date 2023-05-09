

const persons = [
    { name: 'Edward', value: 21 },
    { name: 'Sharpe', value: 37 },
    { name: 'And', value: 45 },
    { name: 'The', value: -12 },
    { name: 'Magnetic', value: 13 },
    { name: 'Zeros', value: 37 }
  ];

// Sort By name
persons.sort(function (a, b) {
    if (a.name > b.name) {
      return 1;
    }
    if (a.name < b.name) {
      return -1;
    }
    // a must be equal to b
    return 0;
  });

 // console.log (items);

// El ultimo arg es el valor inicial de acc
const reducido = [].reduce((acc,el)=> acc+el,0);


// aqui el valor inicial de acc es un objeto vacio
// Crea un nuevo vector donde pone nombre: {el objeto }
const resultIndexed = persons.reduce((acc,el)=>({
  ...acc,[el.name]: el
}),{});


 console.log(resultIndexed);

// Concat
const numeros = [1,2,3,4,5,6];
const letras = ['a','b','c'];
const c = numeros.concat(letras);
// console.log(c);
// Aplanar vector
const matrices = [100,[200,400],100];

const aplanado = matrices.reduce((acc,el)=> acc.concat(el),[]);
console.log(aplanado);


const person1 = { name: 'Edward', value: 21 };

const person2 = {...person1,['key']:1000};

// console.log(person2);
// Creamos otro objeto con copia  del 
// person2 + nomnbre como atributo = al objeto mismo, 
// Tipo = una constante
const person3 = {
  ...person1,
  [person1.name]:person1,
  ['Tipo']: 'EMPLEADO'};

console.log(person3);




