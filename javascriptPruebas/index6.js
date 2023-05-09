
import chalk from 'chalk';


const  geDatos = () =>{
    console.log(chalk.blue( 'llamando geDatos asincrono'));
    return new Promise ((resolve,reject) => {
                if(productos.length !==0){
                    reject (new Error("error provocado"));                    
                }

                else{
                    setTimeout(() => {
                        resolve (productos);
                    }, 4000);
                }
                
        });
 
  //reject
    
}




 async function geDatosAsync  (){

    try {
        const data = await geDatos();
    //console.log(chalk.bgYellow( data));
      
    return data;
    } catch (error) {
        console.log(chalk.red( error.message));
    }
    
}

// geDatos().then((res)=>{
//     console.log(chalk.blue( res));
// }).catch(err=>{
//     console.log(chalk.red( err));
// });



//console.log(chalk.bgYellow( data));



//Se lo puede llamar de ambas formas
//geDatosAsync();
//o como rpomesa
let d = geDatosAsync();
d.then(res=>{
    console.log(chalk.yellow( res));
})
