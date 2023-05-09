//const axios = require('axios').default;
import axios from 'axios';
// const fetch = require('node-fetch');
import fetch from 'node-fetch';
// var https = require('https');
import https from 'https';
//const fs = require('fs');
//import {fs} from 'fs';
import chalk from 'chalk';

const url = 'https://localhost:5100/api/Facturas/getByNroFactura?nroFact=297739';


function call_fetch_async() {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';
    // fetch('https://github.com/')
    // .then(res => res.text())
    // .then(body => console.log(body));
    //const url = 'https://localhost:5100/api/Facturas/getByNroFactura?nroFact=297739';
    console.log(chalk.blue('call_fetch_async '));
    try {
        //let promise = 
        fetch(url, {
                'rejectUnauthorized': false

            })
            .then(res => { //esta promesa retorna json
                //console.log(chalk.blue('return  call_fetch_async '));
                return res.json()
            })
            .then(json => { // esta muestra el json 
                console.log(chalk.blue(json));
            }).catch(error => console.error(chalk.red('Error llamando la api.'), error));
    } catch (e) {
        alert(e);
    }


}


function call_Axios_async1() {

    console.log(chalk.cyan('call_Axios_async1'));
    // At request level
    const agent = new https.Agent({
        rejectUnauthorized: false
    });

    axios.get(url, {
            httpsAgent: agent
        })
        .then(res => {
            console.log(chalk.blue(res.data));
        }).catch(function (error) {
            console.log(chalk.red(error));
        });

    }





function call_Axios_async2() {

    console.log(chalk.cyan('call_Axios_async2'));


    const instance = axios.create({
        httpsAgent: new https.Agent({
            rejectUnauthorized: false
        })
    });
    instance.get(url)
        .then(function (response) {
            console.log(chalk.blue(JSON.stringify(response.data)));
        })
        .catch(function (error) {
            console.log(error);
        });


}
function call_Axios_async3() {

   
    axios({
            url: url,
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            responseType: 'json',
            httpsAgent: new https.Agent({
                rejectUnauthorized: false
            })
        })
        .then(response => {console.log(
            chalk.blue(response.data));}
            )
        .catch(error => {console.log(chalk.red(error));})
}

//all_Axios_async();

//call_Axios_async2();


call_fetch_async();