
export class Person {
    public Id: string;
    public FirstName: string;
    public Lastname: string;
    public City:string;
    public Phone :  string;
    public DocNumber :  string;
    
    public GeneratedDate :Date;
    public   GetFullName ():string {

      return `${this.Id }  ${this.Lastname } , ${this.FirstName} from ${this.City}`   ;
    }
  }
  
  
  export class Product {
    public Id: string;
    public Name: string;
    
    public Cost:string;
    public Count:number;
    public Material :  string;
    public Unit :  string;
    public Lab :  string;
    
    public GeneratedDate :Date;
    public Description: string;
    public Department: string;
    public   GetFullName ():string {
  
      return `${this.Id }  ${this.Name } , ${this.Material}`   ;
    }
  }