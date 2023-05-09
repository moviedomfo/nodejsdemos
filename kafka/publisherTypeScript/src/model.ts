
export class Person {
    public Id: string;
    public FirstName: string;
    public Lastname: string;
    public City:string;
    public Phone :  string;
    public GeneratedDate :Date;
  DocNumber: string;
    public   GetFullName ():string {

      return `${this.GeneratedDate }  ${this.Lastname } , ${this.FirstName} from ${this.City}`   ;
    }
  }
  