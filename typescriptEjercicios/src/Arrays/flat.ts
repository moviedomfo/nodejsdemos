// Aplanar una matriz
import { FileFunctions } from "fwk-libs/dist/helpers/fileFunctions";
import { Person } from "src/Entities";

const personsPath = "./files/persons.json";
//crea una nueva matriz con todos los elementos de sub-array concatenados recursivamente hasta la profundidad especificada.
//https://developer.mozilla.org/es/docs/Web/JavaScript/Reference/Global_Objects/Array/flat
export const flatPersons = async () => {
    const depth = 1;
    const jsonContent = await FileFunctions.OpenFile(personsPath);

    const persons: Person[] = JSON.parse(jsonContent) as Person[];
    const newArray = persons.flat(depth);


    console.log(JSON.stringify(newArray))
}
