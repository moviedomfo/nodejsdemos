
export class Person {
    public Id: string;
    public FirstName: number;
    public Lastname: string;
    public CreatedDate: Date;
    
    public   GetFullName ():string {

      return this.Lastname + ` ,` + this.FirstName;
    }
  }
  