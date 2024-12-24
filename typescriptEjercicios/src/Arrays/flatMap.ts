// Aplanar una matriz
import { FileFunctions } from "fwk-libs/dist/helpers/fileFunctions";
import { Person } from "src/Entities";

const personsPath = "./files/persons.json";
//Combina el mapeo y el aplanado en un solo paso
// Suponer que se cuenta con un array de personas que tienen un  array de direcciones 
// y queremos un listado de todas las direcciones en un solo vector que no sea persona sino que sea AllAddressess
// - Aplanado limitado a un nivel 1
// El método flatMap() primero mapea cada elemento usando una función de mapeo, 
// luego aplana el resultado en una nueva matriz. Es idéntico a un map seguido de un flattende profundidad 1, 
// flatMap es ligeramente más eficiente porque evita crear arrays intermedios.
// https://developer.mozilla.org/es/docs/Web/JavaScript/Reference/Global_Objects/Array/flatMap
export const flatMapPersons = async () => {
    const depth = 1;
    const jsonContent = await FileFunctions.OpenFile(personsPath);

    const persons: Person[] = JSON.parse(jsonContent) as Person[];

    // este retorna 10 elemento uno pór cada address de las Persons. 
    // Aplana las direcciones en un solo array
    const newArray = persons.flatMap(item => item.address.map(a => ({
        ...a,
        city: "ciudad " + item.lastName
    })) || []
    );

    //Este retorna 4 elementos 0,1,2 sin los atributos de Persona, y en cada indice las direcciones
    //map retorna un elemento por cada item del root
    const newArray2 = persons.map(item => item.address.map(a => ({
        ...a,
        city: "ciudad " + item.lastName
    })) || []
    );

    //console.log(JSON.stringify(newArray))
    console.log(JSON.stringify(newArray2))
}

export const simpleSample = async () => {
    const strings = ["hello", "world"];

    const mapped = strings.map((str) => str.split("")); // [["h", "e", "l", "l", "o"], ["w", "o", "r", "l", "d"]]
    const flattened = strings.flatMap((str) => str.split("")); // ["h", "e", "l", "l", "o", "w", "o", "r", "l", "d"]
    console.log('2) Con map')
    console.log(mapped);
    console.log('2) Con flatMap')
    console.log(flattened);

}