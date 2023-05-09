
class Person {
     Id;
     FirstName;
     Lastname;
    
    GetFullName () {

      return this.Lastname + ` ,` + this.FirstName;
    }
  }
  
  module.exports = {Person: Person};