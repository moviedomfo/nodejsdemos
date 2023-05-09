'use strict';

let numberUser = 0;

let app = require('express')();
let http = require('http').Server(app);
let io = require('socket.io')(http);

io.on('connection', (socket) => {
  console.log('user connected');
  
  // socket.connectUser(client, this.io);

  //       socket.disconnected(client, this.io);

  //       socket.getUsers(client, this.io);

  //       socket.message(client, this.io);
        
  socket.on('disconnect', function(){
    console.log('user disconnected');
  });
  
  socket.on('add-message', (message) => {
    console.log(message);
    io.emit('message', {type:'new-message', text: message});    
  });
});

http.listen(5000, () => {
  console.log('started on port 5000');
});


// export const connectUser = (client: Socket, io: socketIO.Server) => {
//   numberUser++;
//   const user = new User(client.id, 'user-' + numberUser);
//   console.log('User connected:    ' + user.name + ' -> ' + client.id);
//   connectedUsers.addUser(user);

//   io.emit('active-users', connectedUsers.getList());
// };
// export const disconnected = (client: Socket, io: socketIO.Server) => {
//   client.on('disconnect', () => {
//       console.log('User disconnected: ', client.id);
//       connectedUsers.deleteUser(client.id);

//       io.emit('active-users', connectedUsers.getList());
//   });
// }
// export const getUsers = (client: Socket, io: socketIO.Server) => {
//   client.on('get-users', () => {
//       io.to(client.id).emit('active-users', connectedUsers.getList());
//   });
// };

// export const message = (client: Socket, io: socketIO.Server) => {
//   client.on('message', (payload: {from: string, body: string}) => {
//       console.log('Received message  >| ', payload.body);

//       io.emit('new-message', payload);
//   });
