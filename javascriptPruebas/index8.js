
import chalk from 'chalk';



//Podemos ver que cuando la variable es actualizada dentro del condicional if, el valor de esta se actualiza a 20 a nivel global
function explainVar(){
    console.log(chalk.italic('---------var------------'));  
    var a = 10;
    console.log(chalk.blue(a));  // output 10
    if(true){
     var a = 20;
     console.log(chalk.green(a)); // output 20
    }
    console.log(chalk.green(a));  // output 20
    console.log(chalk.italic('-----------------------'));  
   }

   function explainVar2(){
    console.log(chalk.bgWhite('---------var 2------------'));  
    var i = 60;
    
     for( var i = 0; i < 5; i++){
      console.log(chalk.red(i))                   //Output 0, 1, 2, 3, 4
     }
    
    console.log(chalk.cyan("Despues del loop", i)); // Output 60
    
   }

   function explainlet(){
    console.log(chalk.italic('---------let------------'));  

    let a = 100;
    console.log(chalk.blue(a));  // output 10
    if(true){
        let a = 200;
        console.log(chalk.green(a)); // output 20
    }
    console.log(chalk.blue(a));  // output 20

    console.log(chalk.italic('-----------------------'));  
   }

   explainVar();
   explainVar2();
   explainlet();



   //Const es igual que let, con una pequeña gran diferencia: no puedes re asignar su valor.
   //Cada vez que uses let es bueno analizar si realmente cambiará el valor de esta variable en algún momento. De no ser así, utiliza const.
   function explainConst(){
    const x = 10;
    console.log(x); // output 10
    x = 20; //throws type error
    console.log(x);
   }


   explainConst();