// const fetch = require('node-fetch');
// var unirest = require("unirest");
// const axios = require('axios').default;
// var https = require('https');
// const fs = require('fs');
import axios from 'axios';
import chalk from 'chalk';
import fs from 'fs';
import chalk from 'chalk';

function call_rapidapi() {


    unirest
        .get('https://restcountries-v1.p.rapidapi.com/all')
        .headers({
            "x-rapidapi-key": "4c297f35c7mshb4a17c73baebf7dp1da86fjsne55786fb4afb",
            "x-rapidapi-host": "restcountries-v1.p.rapidapi.com",
            "useQueryString": true
        })
        //.send({ "parameter": 23, "foo": "bar" })
        .then((res) => {
            console.log(res.body)
            
            let resToJson = JSON.stringify(res);
            
            console.log(resToJson);
            fs.writeFileSync('countries.json', resToJson );
        })


}

async function call_rapidapi_async() {

    console.log(chalk.cyan('call_rapidapi_async'));
    const response = await unirest
        .get('https://restcountries-v1.p.rapidapi.com/all')
        .headers({
            "x-rapidapi-key": "4c297f35c7mshb4a17c73baebf7dp1da86fjsne55786fb4afb",
            "x-rapidapi-host": "restcountries-v1.p.rapidapi.com",
            "useQueryString": true
        });


    return response;

}


async function call_rapidapi_Axios_async() {

    console.log(chalk.cyan('call_rapidapi_Axios_async'));
    let url = 'https://restcountries-v1.p.rapidapi.com/all';

    let config = {
        headers: {
            "x-rapidapi-key": "4c297f35c7mshb4a17c73baebf7dp1da86fjsne55786fb4afb",
            "x-rapidapi-host": "restcountries-v1.p.rapidapi.com",
            "useQueryString": true
        }
    }

    const response = await axios.get(url, config);

    return response;

}










//getNombre(297739);


//getNombre_Unirest();
//getNombre_Axios();

//call_rapidapi();

//Funciona llamada async y store in file
// var promise = call_rapidapi_async();
// promise.then((res) =>{
//     let resToJson = JSON.stringify(res);
//      fs.writeFileSync('countries.json',resToJson);

//         fs.writeFile('countries-3.json', resToJson, (err) => {
//             if (err) throw err;
//             console.log('Data written to file');
//         });

//         console.log(chalk.italic("se retornaron " + res.length + 'resultados' ));
// })


call_rapidapi();
return; 
///Misma llamada que la anteirir pero con lib Axios
var promise = call_rapidapi_Axios_async();
promise.then(function (res) {
            console.log(res.length);
        let resToJson = JSON.stringify(res[0]);
        
        console.log(resToJson);
        fs.writeFileSync('countries.json', resToJson );

        // fs.writeFile('countries-3.json', resToJson, (err) => {
        //     if (err) throw err;
        //     console.log('Data written to file');
        // });

        console.log(chalk.italic("se retornaron " + res.length + 'resultados'));
    })
    .catch(function (error) {
        console.log(chalk.red(error));

    }) ;