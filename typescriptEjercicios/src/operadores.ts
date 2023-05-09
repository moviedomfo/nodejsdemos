

// Evaluación de "corto circuito"
// && y || funcionan como operadores de corto circuito.
// Con el operador lógico AND, si el primer operando se devuelve como falso, el segundo nunca será evaluado y se devolverá el primer operando.
// Con el operador lógico OR, si el primer valor se devuelve como verdadero, el segundo nunca será evaluado y el primer operando será devuelto.
// && devuelve el primer valor
// || devuelve el segundo valor y vice versa.

// Operador lógico AND (&&)
// Este operador lógico compara dos expresiones. Si la primera se evalúa como "verdadera" (truthy), 
// la sentencia devolverá el valor de la segunda expresión. Si la primera expresión se evalúa como "falsa"(falsy), 
// la sentencia devolverá el valor de la primera expresión.

export const UsoAND = () => {
  let message = "hola";
  //devuelve el segundo valor, verdadero 
  let result = message && 44;

  console.log("message && 44 = " + result);

  UsoAND_Nulo();
};

const UsoAND_Nulo = () => {
  const message = undefined;
  //devuelve el primer valor, undefined
  const result = message && 44;
  console.log("message && 44 = " + result);
};
// Operador lógico OR ( || )
// Este operador lógico compara dos expresiones. Si la primera se evalúa como "falsa", 
// la sentencia devolverá el valor de la segunda expresión. 
// Si la primera se evalúa como "verdadera", la sentencia devolverá el valor de la primera expresión.

export const UsoOR = () => {
  let message = "hola";

  let result = message || 44;

  console.log("message || 44 = " + result);
  UsoOR_Nulo();
};

const UsoOR_Nulo = () => {
  const message = undefined;

  const result = message || 44;
  console.log("message || 44 = " + result);
};
