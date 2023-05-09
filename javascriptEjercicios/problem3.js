let str = 'Hola mundo';


const reverse =(str)=>{
  if(str === '')
    return '';     
  return reverse(str.substring(1)) + str.charAt(0);
 
}


console.log(reverse(str));
 



