let str = 'Hola mundo';

const reverse =(str)=>{
   
   let res = '';
    for (let i = 0; i < str.length; i++) {
        res = str[i] + res ;
     }
     console.log(res);
}


const reverse2 =(str)=>{
   
    let res = '';
     for (item of str) {
         res = item + res ;
      }
      console.log(res);
 }


//reverse(str);

reverse2(str);