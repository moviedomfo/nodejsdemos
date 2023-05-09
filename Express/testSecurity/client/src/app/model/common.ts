import { Http, Response, RequestOptions, Headers } from '@angular/http';

//let headers = new Headers({ 'Content-Type': 'application/json' });
let headers = new Headers();
headers.append('Content-Type', 'application/json');
     headers.append('Access-Control-Allow-Methods', 'GET, POST, PATCH, PUT, DELETE, OPTIONS');
     headers.append('Access-Control-Allow-Headers', 'Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With');
     headers.append('Access-Control-Allow-Origin', 'http://localhost:4200');

     let options = new RequestOptions({ headers: headers });
     export  const AppConstants={
       
          httpHeaders:headers,
          httpOptions:options,
          baseUrl_placeHolders : 'http://localhost:8080/api/placeHolders',
          baseUrl_security : 'http://localhost:8080/api/users',
          //tiempo de expiracion en segundos
          expiresIn: 60

     }