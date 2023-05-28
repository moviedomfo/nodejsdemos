import {v4} from "uuid";
export class ServerSocket {
  public users: {[uid: string]: string};
  constructor() {
    this.users = {};
  }
  NewUser = (name: string): void => {
    const user = Object.values(this.users).includes(name);

    if (user) {
      console.info("User alredy exist.");
      return;
    }
    const uid = v4();
    this.users[uid] = name;
  };
  Get = (uid: string) => {
    return Object.keys(this.users).find((uid) => this.users[uid] === uid); 
  };
  GetByName = (name: string) => {
    const user = Object.values(this.users).find(p=>p==name);
    return user;
  };
  getAll = () => {
    const users = Object.values(this.users);
    return users;
  };
}
