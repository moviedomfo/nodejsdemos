let str = 'Hola mundo';

const resInvert = [...str].reduce((acc,el)=>(
   el.concat(acc)
 ),'');
 console.log(resInvert);
 
 // o

 const resInvert2 = [...str].reverse().join('');
 console.log(resInvert2);

