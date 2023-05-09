
# ADN Mutations
 API to validate ADN strings mutations
## Table of Contents

- [Intro](#Intro)
- [Push message to kafka topic ](#Push-message-to-kafka-topic )
- [Database](#mongodb-hosted)
- [Run locally](#Run-locally)
- [Dockerize](#Dockerize)

## Intro

This API provide two principal enpoints:
   
 - %URL_BASE%/api/comercepub

## Push message to kafka topic 

Path /api/comercepub/push

This POST enpoint recive an input with this format:
```
{
  "key": "2",
  "topic": "apple",
  "content": "Yes, asd example ofrrrrrrrrrrrr",
  "type": 1
}
```

Returns true if has mutations or false if not

```
        curl --location --request POST 'http://localhost:3008/api/comercepubs/push' \
        --header 'Content-Type: application/json' \
        --data-raw '{
            "key":"2", 
                "topic":"pepe",
                "content":"Yes, asd example ofrrrrrrrrrrrr",
                "type":1
                }'
    }

    }'
```



## Run locally
  [1] Firs clone the repo locally
  [2] run pnpm install or yarn install
  [3] run dev command 

    ```
        pnpm run dev
    ```
  [4] Additionally if you have dockerhub installed. We leave you a dockerfil ready!! 
       pleasse ref to [Dockerize](#Dockerize)

 



# Microservices

## Dockerize

You can use docker to deploy the api server. In this releasse we leave a dockerfile and a docker compose ready to use


 * generate image
 
```
    docker image build -t moviedomfo/express_comercepub .
```

 * run container
 
```
docker run -d -p 3008:3008 --name express_comercepub moviedomfo/express_comercepub  
```

 * Navigate to this url to check the if correctly docker container is running 
    
        http://localhost:3008
        


# swagger & tsoa 

Para documentar los controllers debemos usar tsoa
   "build-tsoa": "tsoa spec-and-routes" genera las Rutas en base a los controllers documentados con los docoradores tsoa
   "predev": "npm run swagger", genera el json para que swagger levante la pagina con la documentacion

   Hay que ver que tenemos configurado en ./tsoa.json para ver el destino de la transpilacion de las rutas


#  RegisterRoutes  
To generate Routes class run 
    ```
        pnpm build-tsoa  
    ```

# Kafka docker images

Apache Kafka is a distributed streaming platform designed to build real-time pipelines and can be used as a message broker or as a replacement for a log aggregation solution for big data applications

We use Apache Kafka packaged by Bitnami

## Run the application using Docker Compose

###  we use local docker-compose.yml content what should it contain this
 

```
    zookeeper:
        image: docker.io/bitnami/zookeeper:3.8
        ports:
        - "2181:2181"
        volumes:
        - "zookeeper_data:/bitnami"
        environment:
        - ALLOW_ANONYMOUS_LOGIN=yes
    kafka:
        image: docker.io/bitnami/kafka:3.2
        ports:
        - "9092:9092"
        volumes:
        - "kafka_data:/bitnami"
        environment:
        - KAFKA_CFG_ZOOKEEPER_CONNECT=zookeeper:2181
        - ALLOW_PLAINTEXT_LISTENER=yes
        depends_on:
        - zookeeper
```

Next run: 

```
     docker-compose up -d
```

or 

     1) docker-compose  -f docker-compose-kafka.yml down
     2) docker-compose up -d

### kafka documentations
    https://www.npmjs.com/package/kafkajs
    https://www.youtube.com/watch?v=EiDLKECLcZw

### kafka packages
    pnpm i kafkajs
    this trow this error ✕ missing peer openapi-types@>=7...so you have to install

        pnpm openapi-types@>=7
        
    pnpm i @kafkajs/confluent-schema-registry