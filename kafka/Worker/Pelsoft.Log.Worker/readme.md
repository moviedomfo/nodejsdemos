
Introducción a CQRS (Demo) & Event Sourcing con Kafka
https://www.youtube.com/watch?v=W3JJ6eXoiuY


# Dockerize 
## Create docker image
    docker rmi workerservicekon:latest

    -rm to remove intermediate images  not work
    -t to tag image 

    docker build -t workerservicekon:latest ./ -f .

## Run image    
    docker run --name epi-log-supscriptor -d workerservicekon
### Inspect or  image    
    docker exec -it epi-log-supscriptor


## Manual generate build

    dotnet build -c Release -o ./dist

## Generat distribuible release (./dist/publish can be changes to suit each one )
    dotnet publish -c Release -o ./dist/publish 

## Run worker

    Go to dist folder and run :

        dotnet WorkerServiceKon.dll