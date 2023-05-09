
# 💎 This proyect 

 This is an daemon that implement a loop alghoritmic that ramdom generate Persoms Objects and send to redis

## Components to install

## Redis 
- [Commands doc](https://redis.io/commands)
    npm install redis                
    npm install --save @types/redis

- [Redis UI](https://www.npmjs.com/package/redis-commander)

    npm install -g redis-commander

    Util dasboard to browse and view in the explorer all redis messages
    
    1-You need to run the following command

        redis-commander --redis-password pletorico28

    2- Next, access via browser with http://127.0.0.1:8081

 - [git sample](https://github.com/redis/node-redis)

     
- [docker compose](https://kb.objectrocket.com/redis/run-redis-with-docker-compose-1055)

   docker run --name redis_cont -p 6379:6379 -d redis
        
    This command will do the following:
    1-Pull the latest Redis image from the Docker hub (the place where all the public 3rd party images are stored)
    2-Create and run the container and name it: my-redis
    3-Route port 6379 on my laptop to port 6379 inside the container. 6379 is Redis default port and can be changed


## devDependencies

 **types/node**
```
    npm i --save-dev @types/node
```

 **nodemon** is a tool that helps develop node.js based applications by `automatically restarting` the node application when file changes in the 
 -[nodemom](https://www.npmjs.com/package/nodemon)
    You can also install nodemon as a development dependency:
```
    npm install --save-dev nodemon 
 ```
    And nodemon will be installed globally to
     your system path.
```
    npm install -g nodemon 
 ```

 So, that the .ts files are recognized, youi must install
    
    ``` 
        npm install -g ts-node
    ```
 directory are detected.
    "scripts": {
               "start": "tsc && nodemon  --tls-min-v1.0   dist/app.js",
            "prod": "node app"}
        }

    configure: 
      a)package.json{
                    "nodemonConfig": {
                     "ignore": ["test/*", "docs/*"],
                    "delay": "2500"
            }
      }
          
      b)  nodemon.json

       If you specify a --config file or provide a local nodemon.json any package.json config is ignored.

**cron scheduler**
[cron jobs](https://crontab.guru/)
[tutorials](https://www.digitalocean.com/community/tutorials/nodejs-cron-jobs-by-examples)

**fs**  For file management : Note:  doesn’t work correctly on cross partitions or virtual file systems.
```
    npm install mv
```

 **pm2**
    https://desarrolloweb.com/articulos/ejecutar-aplicacion-nodejs-pm2.html

**Faker** Faker git https://github.com/marak/Faker.js/
    Esta libreria es usada para la generacion de datos de prueba 
    
```
    npm i faker 
```

## run app
```
    npm start [args]
    npm run prod
```
    
[ars] : numbers of person sended to redis
    
 
### deploy
```
    npm install --production
```

## Redis with compose 

[likn](https://kb.objectrocket.com/redis/run-redis-with-docker-compose-1055)
```
     docker-compose up --build
```

 **Errores**
    - The PWD variable is not set. Defaulting to a blank string.

 

