1- Run docker compose

    docker-compose -f  docker-compose-kafka.yml up -d 

3-
docker exec -it f981091a38776821963fd925d8fa7b8a95a2ba3993c5f7793df020fad0874604 /bin/sh
or
docker exec -it  kafka /bin/sh

navigate to opt/bitnami/kafka/bin (cd cmd)

Result should list this files among others ..

kafka-topics.sh zookeeper-security-migration.sh kafka-cluster.sh kafka-delegation-tokens.sh   kafka-log-dirs.sh         
Execute this command to create TOPIC "farma0001" 


kafka-topics.sh --create --bootstrap-server  zookeeper:2181 --replication-factor 1 --partitions 1 --topic farma0001
kafka-topics.sh --create --bootstrap-server  localhost:9092 --replication-factor 1 --partitions 1 --topic farma0001


4- list all topics
kafka-topics.sh --list --bootstrap-server  localhost:2181