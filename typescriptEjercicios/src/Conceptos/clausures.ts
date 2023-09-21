// run ts-node ./src/conceptos/clausures.ts

// Las funciones en javascript son objetos y pueden contener otras funciones dentro de ellas
// Closures : permite acceder al ámbito de una función exterior desde una función interior.
// En JavaScript, las clausuras se crean cada vez que una función es creada.

// La función iniciar() crea una variable local llamada nombre y una función interna llamada mostrarNombre().
// Por ser una función interna, esta última solo está disponible dentro del cuerpo de iniciar().
function iniciar() {
  let _Nombre = "Mozilla";
  function mostrarNombre() {
    // La función mostrarNombre es una función interna, una clausura.
    return _Nombre; // Usa una variable declarada en la función externa.
  }
  function addApellido(apellido :string ) {
    // La función mostrarNombre es una función interna, una clausura.
    _Nombre += ` ${apellido}`;  // Modifica una variable declarada en la función externa.
    return _Nombre;
  }
  return {mostrarNombre,addApellido};
}

const getInit = iniciar();
console.log(getInit);
console.log(getInit.mostrarNombre());
console.log(getInit.addApellido('Olecram'));
// mostrarNombre() no tiene ninguna variable propia,
// dado que las funciones internas tienen acceso a las variables de las funciones externas,
// mostrarNombre() puede acceder a la variable nombre declarada en la función iniciar().

// If we use the browser to inspect the code and see the Function We don´t have access to local
// pproperty secret (nombre in this case) because it is internal inside a function

//return function with string
const someFn = () => {
  const secret = "secret";
  return () => secret;
};
//return  string
const someFn2 = () => {
  const secret = "secret";
  return secret;
};
const getSecret = someFn();
console.log(getSecret());

const getSecret2 = someFn2();
console.log(getSecret2);
