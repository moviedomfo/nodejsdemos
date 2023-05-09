let str = 'Hola mundo';

const reverse =(str)=>{
    
  return [...str].reverse().join('');
 
}

function invertWordsIncorrecto(str){
  let array = str.split(' ');
  let arrayRes = [];
  array.map(p=>{
      let rev = [...p].reverse().join('');
      arrayRes.push(rev);
  })
  
  return arrayRes.join(' ');
}
// que esta poco practico la solucion anterior:No necesitas crear un nuevo array y hacer push en él.
// map retorna el producto final de una vez, por lo que podemos simplemente asignar el valor de retorno a una nueva variable.
function invertWords(str){
  let array = str.split(' ');
  let inv =  array.map(p=>reverse(p));
  //console.log(inv);
  return inv.join(' ');
}

console.log(invertWords(str))