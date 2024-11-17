
class Socios {
  constructor(nroSocio, name, adherentes) {
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
  constructor(nombre, edad) {
    this.nombre = nombre;
    this.edad = edad;
  }

  hacerSonido() {
    console.log(`${this.nombre} está haciendo un sonido.`);
  }
}

class Perro extends Animal {
  constructor(nombre, edad, raza) {
    super(nombre, edad); // Llama al constructor de la clase base
    this.raza = raza;
  }
  hacerSonido() {
    console.log(`${this.nombre} dice: ¡Guau!`);
  }
  moverCola() {
    console.log(`${this.nombre} está moviendo la cola.`);
  }
}

// 👇️ named export (same as previous code snippet)
export { Socios, Adherentes, Perro };