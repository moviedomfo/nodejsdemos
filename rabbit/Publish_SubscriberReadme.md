https://www.rabbitmq.com/tutorials/tutorial-three-javascript.html

Patron publish/subscribe donde se pueden repartir mensajes a mas de un consumidor
Exchange: Esta patron utiliza  este componente para que el publicador envie mensajes y es el exchange el que se bindea a la cola 
	ch.assertExchange('logs', 'fanout', {durable: false})
Binding : relacion establecida entre exchange y la cola
	channel.bindQueue(queue_name, exchange_name, '');
	channel.bindQueue(queue_name, exchange_name, exchange_ruting); 
	channel.bindQueue(queue_name, exchange_name, 'black'); 


Para escuchar todas los exchanges en el servidor ejecutar:
	rabbitmqctl list_exchanges
	
The default exchange (nameless exchang): 
Cuando usamos el patron direct -> work queues no se define ningun Exchange: pero por defecto si se crea uno de forma aleatoria
	aqui usamos default exchange channel.sendToQueue('hello', Buffer.from('Hello World!'));
	
	
# ExchangeFanout 
publisher
	1 typescript 
	2 Nodemom  para deteccion de cambio de codigo y reinicio del servicio https://www.npmjs.com/package/nodemon
	# to start
		npm start
subscriberNode
	1)Subscriber desarrollado en javascript puro
	start: node  start
	# to start
		node subscriber/subscriber.js
subscriberTypescript
	1) Subscriber desarrollado en Typescript 
	2) Nodemom
	# to start 
		npm start
	
# ExchangeTopic 
