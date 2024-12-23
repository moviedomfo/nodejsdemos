// Aplanar una matriz
import { FileFunctions } from "fwk-libs/dist/helpers/fileFunctions";
import { Person } from "src/Entities";

const personsPath = "./files/persons.json";
//El método flatMap() primero mapea cada elemento usando una función de mapeo, 
//luego aplana el resultado en una nueva matriz. Es idéntico a un map seguido de un flattende profundidad 1, 
// pero flatMap es a menudo útil y la fusión de ambos en un método es ligeramente más eficiente
// //https://developer.mozilla.org/es/docs/Web/JavaScript/Reference/Global_Objects/Array/flatMap
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
