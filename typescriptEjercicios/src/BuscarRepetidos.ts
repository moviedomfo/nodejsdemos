// Dado un json con tipos de animales retornar un array donde se liste cuantes veses se repite cada uno
// Solo mostrar aquellos que repiten

import {FileFunctions} from "fwk-libs/dist/helpers/fileFunctions";
import {Animal} from "./animales";

const animalesPath = "./files/animales.json";

const buscarRepetidos = async () => {
  const jsonContent = await FileFunctions.OpenFile(animalesPath);
  const animals: Animal[] = JSON.parse(jsonContent) as Animal[];

  let repited = [];
  animals.forEach((p) => {
    const exist = repited.find((f) => f.NombreEspecie === p.NombreEspecie);
    if (!exist) {
      const count = animals.filter((f) => f.NombreEspecie === p.NombreEspecie).length;
      if (count > 1) {
        repited.push({count, NombreEspecie: p.NombreEspecie});
      }
    }
  });
  console.log(repited);
};

export default buscarRepetidos;
