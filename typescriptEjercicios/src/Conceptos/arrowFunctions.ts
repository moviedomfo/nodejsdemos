const number = [12, 4, 65, 12, 78];

//Regular Functions  We can write the regular function in two ways, i.e Function declaration, and Function expression.
// The main difference:is we can invoke the function ShowNumbers_declaration(2,3) before its declaration also

//declaration : we can invoke the function before its declaration
function ShowNumbers_declaration(args) {
  console.log(args);
  return Math.max(...args);
}

//expretion: needs to invoke after it is defined.
const ShowNumbers_expression = function (args) {
  console.log(args);
  return Math.max(...args);
};

// Arrow Functions  https://levelup.gitconnected.com/7-differences-between-arrow-functions-and-regular-functions-in-javascript-9152883a839f

// The Syntax
// No arguments (arguments are array-like objects)
// In the arrow function, there are no arguments  if we access arguments in the arrow function will throw an error like

// Cannot be invoked with a new keyword (Not a constructor function)
// It cannot be used as a Generator function
// Duplicate-named parameters are not allowed
const ShowNumbers = (args: any): number => {
  console.log(args);
  return Math.max(...args);
};

const User = {
  name: "Lion",
  getUserName: function () {
    return this.name;
  },
  getUserNameArrow: () => {
    return name;
  },
};

const ShowNumbersUse = () => {
  console.log("Use Arrow function");
  ShowNumbers(number);

  console.log("Regular functions prototype");
  console.log(ShowNumbers_expression.prototype);
  console.log("No prototype object for the Arrow function will be undefinded");
  console.log(ShowNumbersUse.prototype);

  console.log("No own this (call, apply & bind won't work as expected");

  console.log("User.getUserName : ", User.getUserName());
  try {
  } catch (err) {
    console.error(err);
  }
};

export {ShowNumbersUse, ShowNumbers};

//Functions expression
