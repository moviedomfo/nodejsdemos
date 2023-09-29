// Paso 1: Definir la interfaz del producto (ITransportMedia)
export default interface ISoftwareBuilder {
  plan(): void;
  design(): void;
  implement(): void;
  test(): void;
  deploy(): void;
}
// Paso 2: Implementar una clase concreta que implemente ISoftwareBuilder
export class WebAppBuilder implements ISoftwareBuilder {
  plan() {
    console.log("Paso 1: Planificar el proyecto de desarrollo de software web.");
  }

  design() {
    console.log("Paso 2: Diseñar la arquitectura y la interfaz del software web.");
  }

  implement() {
    console.log("Paso 3: Implementar el código del software web.");
  }

  test() {
    console.log("Paso 4: Realizar pruebas unitarias y de integración web.");
  }

  deploy() {
    console.log("Paso 5: Desplegar el software en el entorno de producción web.");
  }
}

export class DeskAppBuilder implements ISoftwareBuilder {
  plan() {
    console.log("Paso 1: Planificar el proyecto de desarrollo de  escritorio.");
  }

  design() {
    console.log("Paso 2: Diseñar la arquitectura y la interfaz de escritorio.");
  }

  implement() {
    console.log("Paso 3: Implementar el código de  escritorio.");
  }

  test() {
    console.log("Paso 4: Realizar pruebas unitarias y de integración.");
  }

  deploy() {
    console.log("Paso 5: Desplegar el software en el entorno de producción.");
  }
}
