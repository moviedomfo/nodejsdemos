
 class Socios {
    constructor(nroSocio, name,adherentes) {
      this.nroSocio = nroSocio;
      this.name = name;
      this.adherentes = adherentes;
    }


  }

   class Adherentes {
    constructor(orden, name) {
      this.orden = orden;
      this.name = name;
      
    }


  }

  class Animal {
    constructor(nombre) {
      this.nombre = nombre;
    }
  
    hablar() {
      console.log(this.nombre + ' hace un ruido.');
    }
  }
  
  class Perro extends Animal {
    hablar() {
      console.log(this.nombre + ' ladra.');
    }
  }

// 👇️ named export (same as previous code snippet)
export {Socios,Adherentes};