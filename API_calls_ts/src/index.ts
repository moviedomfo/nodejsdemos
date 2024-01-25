
import { PersonAPIService } from "./PersonSvc";
//to ignore SSL certificate validation in node requests
process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const personAPIService: PersonAPIService = new PersonAPIService();
personAPIService.GetCombos1().then(res => {
    console.log(res);
}).catch(er => {
    console.log(er);
});
// const message = "Pelsoft";
// console.log(`Wellcome to ${message} interview  exercises `);
// console.log(`-----------------------------Tipos animales repetidos--------------------------------------------------`);
// buscarRepetidos().then((res) => {});

// const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
// console.log(`-----------------------------BuscarImpares--------------------------------------------------`);
// const impares = BuscarImparesMap(numbers);
// const imparesMap = BuscarImparesMap(numbers);
// console.log(impares);
// console.log(`-----------------------------BuscarImparesMap--------------------------------------------------`);

// console.log(imparesMap);
// console.log(`-----------------------------DetermineMaxInArray-------------------------------------------------`);

// const max = DetermineMaxInArray(numbers);
// console.log(max);

// console.log(`-----------------------------IsPalindrome-------------------------------------------------`);
// const phrase = "Only good can juds me";
// const str = "Yo dono rosas, oro no doy";
// const is = IsPalindrome(phrase);
// console.log(is);

// console.log(`------Dado un array de numeros encontrar aquel que aparece un numero impar de veses-------------------------------------------------`);
// buscarImparFromArrays();

// UsoAND()
// UsoOR();

// const server = new ServerSocket();

// server.NewUser("moviedo");
// server.NewUser("catalina");
// server.NewUser("brenda");
// const users = server.getAll();

// console.log(users);

// console.info(server.GetByName("catalina"));

//ShowNumbersUse();

// CloneMethods.DeepBlue();


