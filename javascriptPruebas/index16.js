
import chalk from 'chalk';
const inventory = [
  { name: 'asparagus', type: 'vegetables', quantity: 5 },
  { name: 'bananas',  type: 'fruit', quantity: 0 },
  { name: 'goat', type: 'meat', quantity: 23 },
  { name: 'cherries', type: 'fruit', quantity: 5 },
  { name: 'fish', type: 'meat', quantity: 22 }
];

// groups the elements by the value of their type property
// let result = inventory.groupBy( ({ type }) => type );

// console.log(chalk.cyan("--------------groupBy----------------"));
// console.log(result);


// result = inventory.groupBy( myCallback );
// console.log(chalk.cyan("--------------groupBy + myCallback ----------------"));
// console.log(result);

// generate 2 arrays
const groups = customGroupBy(inventory, 'type');


console.log(chalk.cyan("--------------customGroupBy----------------"));
console.log(groups);

function myCallback( { quantity } ) {
  return quantity > 5 ? 'ok' : 'restock';
}

function customGroupBy(arr, prop) {
  const map = new Map(Array.from(arr, obj => [obj[prop], []]));
  arr.forEach(obj => map.get(obj[prop]).push(obj));
  return Array.from(map.values());
}



//https://dev.to/solexy/implementing-groupby-function-on-array-of-object-1gdp
// function groupBy<T>(collection:T[],key: keyof T){
//   const groupedResult =  collection.reduce((previous,current)=>{

//   if(!previous[current[key]]){
//     previous[current[key]] = [] as T[];
//    }

//   previous[current[key]].push(current);
//          return previous;
//   },{} as any); // tried to figure this out, help!!!!!
//     return groupedResult
// }
// console.log(groupBy<Inventory>(inventory,"name"));
// console.log(groupBy<Inventory>(inventory,"quantity"));
// console.log(groupBy<Inventory>(inventory,"type"));