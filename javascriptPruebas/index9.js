
import chalk from 'chalk';

// desestructuración
const product =     { 
    id: 5001, 
    name: "David",
    dni: 243456987,
    email: 'david@gmail.com' 
    };

function test(){

    //aqui desestructuración
    const { name ,email} = product;
    //console.log(chalk.blue('product.name : ' + product.name));  
    console.log(chalk.blue('nombre : ' + name));  
    console.log(chalk.yellow('nombre : ' + email));  
}

const onTest = () =>{

    //aqui desestructuración
    const { name ,email} = product;
    //console.log(chalk.blue('product.name : ' + product.name));  
    console.log(chalk.blue('nombre : ' + name));  
    console.log(chalk.yellow('nombre : ' + email));  
}


var items = [
    { name: 'Edward', value: 21 },
    { name: 'Sharpe', value: 37 },
    { name: 'And', value: 45 },
    { name: 'The', value: -12 },
    { name: 'Magnetic', value: 13 },
    { name: 'Zeros', value: 37 }
  ];
  items.sort(function (a, b) {
    if (a.name > b.name) {
      return 1;
    }
    if (a.name < b.name) {
      return -1;
    }
    // a must be equal to b
    return 0;
  });

  
onTest();
//test();