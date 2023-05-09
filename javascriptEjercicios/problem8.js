// Resolviendo CallStack para responder que es el Event Loop';
// Javascript es mnonoporceso y ejecuta una cosa a la vez: tiene un solo CallStack
// CallStack: Estructura de datos que registra en que momento se encientra la ejec del porgrama
// Guarda los scope/contextos de ejecucion
  // usa LIFO : Pila o Stack Last in last out
  // Call => llamada
  // Stack  => Pila
  


const printCuadrado =(n)=>{
  debugger;
  let cuadrado =cuadrado_fn(n);
  console.log(`El cuadrado de ${n} = ${cuadrado}`);
 
}

function cuadrado_fn(n){
  
  let cuadrado = n * n;
  return cuadrado;
}

// fibonacci(9); / --throwRangeError: Maximum call stack size exceeded
// Ewto es infinito Stack es finito por lo tanto Explota
function fibonacci(n){
  
  return fibonacci(n-1) + fibonacci(n-2);
}

printCuadrado(30);

// 1 (anonymous) --> esto aparece en el debug de crome y es el main de la ejecucion
   // 1.1 call or add-scack printCuadrado
     // 1.2 call cuadrado_fn
     // 1.3 del-from-scack cuadrado_fn
     // 1.4 del-from-stack printCuadrado
     // fin (Anonymous)