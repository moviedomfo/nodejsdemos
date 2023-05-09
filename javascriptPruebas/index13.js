
let animal= {
    name:'dog',
    raza:'bulldog',
    age:2
};


// let a = {animal: {age,raza}};

const getData =( {age,raza})=>{
    console.log (age,raza);
}

// const getData2 =(animal)=>{
//     console.log (animal.age,animal.raza);
// }
getData(animal);