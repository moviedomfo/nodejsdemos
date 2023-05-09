// Nullish coalescing ?? : returns its right-hand side operand when its left-hand side operand is null or undefined
//                         and otherwise returns its left-hand side operand.
let Y = 300
let Z = 200
let X11 = Y ?? Z
Y = undefined
let X12 = Y ?? Z


let X22 = Y || Z
console.log (`X11 = ${X11}`);
console.log (`X12 = ${X12}`);

console.log (`X22 = ${X22}`);


const A = null ?? 'default string';
console.log(`A = ${A}`);
const B = 0 ?? 42;
console.log(`B = ${B}`);


console.log(`--------------------Problem with  (||)---------------------------------------`);
//Problem with  OR operator (||): When one wanted to assign a default value to a variable

let foo;
//  foo is never assigned any value so it is still undefined
let someDummyText = foo || 'Hello!';

console.log(`someDummyText = ${someDummyText}`);

let myText = '';
let notFalsyText = myText || 'Hello world';
console.log(`notFalsyText = ${notFalsyText}`);
let preservingFalsy = myText ?? 'Hi neighborhood';
console.log(`preservingFalsy = ${preservingFalsy}`); // '' (as myText is neither undefined nor null)