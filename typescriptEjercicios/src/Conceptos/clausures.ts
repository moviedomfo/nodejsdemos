// Closures : permite acceder al ámbito de una función exterior desde una función interior.
// En JavaScript, las clausuras se crean cada vez que una función es creada.

// La función iniciar() crea una variable local llamada nombre y una función interna llamada mostrarNombre().
// Por ser una función interna, esta última solo está disponible dentro del cuerpo de iniciar().
function iniciar() {
  var nombre = "Mozilla"; // La variable nombre es una variable local creada por iniciar.
  function mostrarNombre() {
    // La función mostrarNombre es una función interna, una clausura.
    alert(nombre); // Usa una variable declarada en la función externa.
  }
  mostrarNombre();
}
iniciar();

// mostrarNombre() no tiene ninguna variable propia,
// dado que las funciones internas tienen acceso a las variables de las funciones externas,
// mostrarNombre() puede acceder a la variable nombre declarada en la función iniciar().
