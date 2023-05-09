// Given an array, rotate the array to the right by k steps, 
// where k is non-negative.
let nums = [1,2,3,4,5,6,7], k = 3



// El ultimo arg es el valor inicial de acc
// const reducido = nums.reduce((previousValue,currentValue)=>(
//   {
//     previousValue
//   }
// ),{});

// va e izq --> derecha
const reducido1 = nums.reduce((acum,actual,i,vector)=> {
  console.log({
    i,
    actual:actual,
    vector_i:vector[i],
    acum});

  return acum.concat(actual + vector[0] );
  
},[]);

console.log(reducido1);

let str = 'Hola mundo';

const resInvert = [...str].reduce((acc,el,i,array)=>{

 console.log({
  i,
  el,
  vector_i:array[i],
  acc});
  return el.concat(acc);
} ,'');
 console.log(resInvert);





