#Execute :
node index

APIs:
https://rapidapi.com/apilayernet/api/rest-countries-v1

Spread Operator en Arrays y Objetos
https://www.youtube.com/watch?v=gFvWNjVy-wc

Cómo funciona Async/Await en menos de 15 minutos
https://www.youtube.com/watch?v=u2axmPnxUoo

¿Cómo funcionan las Promises y Async/Await en JavaScript?
https://www.youtube.com/watch?v=rKK1q7nFt7M&t=638s

## index5 index 7

    uso de Axios, unirest, node-fetch y rapidapi

Si usamos node app.js arrojara error
. FetchError: request to https://localhost:5100/api/Facturas/getByNroFactura?nroFact=297739 failed, reason: write EPROTO 4988:error:1425F102:SSL routines:sssl_choose_client_version:unsupported protocol:c:\ws\deps\openssl\openssl\ssl\statem\statem_lib.c:1922:
Esto se debe a que la version 12 o mayor de nodejs solo admite SSL/TLS v 2.0 en adelante por lo tanto explisitamente debenmos usar el siguiente comando:

    node --tls-min-v1.0 index7.js

Con lo que obtendremos el siguiente error si no le espesificamos el certificado

    Error
      message: 'request to https://localhost:5100/api/Facturas/getByNroFactura?nroFact=297739 failed, reason: unable to verify the first certificate',

Soucion 1 --> ignore SSL issues

    To allow any certificate, you have to add this line near the top of your code;
    This will allow just about anything, but it's also dangerous, so use with caution.

    process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

Solucion 2 --set setting headers

    const agent = new https.Agent({
        rejectUnauthorized: false
    });

A Complete Guide to Securely Connecting Node.js and Axios (JS) Using Mutual TLS
https://smallstep.com/hello-mtls/doc/combined/nodejs/axios

## index index Spreed operator Samples

## index3 Uso de map

## index4 - Uso de Copy con: VECTORES Referencia spread, map , slice

## index5 , 6 7 Async Callbacks unirest Axios fetch

## index8 var let const

## index9 Desestructuración + Arrow Function

## index10 Sort By name, use of reduce

## index11 Array -> Sort by property

## index12 Retorna un array de una enumeracion

## index13 paso de objetos destructurados a funciones

## index14 Nullish coalescing

## index15 Split array into chunks : take an Array as imput and split it into others with a certain number of elements (items per chunk)

## index16 Array ->Group

## index17 DaysJS

## index17 Array -> Eliminar duplicados
