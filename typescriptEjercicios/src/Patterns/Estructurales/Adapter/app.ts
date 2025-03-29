// Adapter: Convierte la interfaz de una clase en otra interfaz que el cliente espera.
// Permite que clases con interfaces incompatibles trabajen juntas.

//Actúa como un puente entre dos interfaces, permitiendo que una clase use la funcionalidad de otra clase sin necesidad de modificar su código original.
// 
// En este ejemplo, el patrón se utiliza para adaptar la interfaz de un servicio externo (ANMADService) a la interfaz que espera el cliente (Laboratorio).

// Al utilizar un adaptador, se puede mantener la separación de responsabilidades y facilitar el mantenimiento del código a largo plazo.

// Un adaptador envuelve uno de los objetos (que debe utilizar tu app -Servicio-) para esconder la complejidad de la conversión que tiene nececita
// tu app. Se trata de un objeto especial que convierte la interfaz de un objetoX, de forma que otro objeto_A pueda comprenderla.
//Esta implementación utiliza el principio de composición de objetos:
//el adaptador implementa la interfaz de un objeto y envuelve el otro.

// La clase Cliente contiene la lógica de negocio existente del programa.

// La Interfaz con el Cliente describe un protocolo que otras clases deben seguir para poder colaborar con el código cliente.

// Servicio es alguna clase útil (normalmente de una tercera parte o heredada). 
// El cliente no puede utilizar directamente esta clase porque tiene una interfaz incompatible.

// La clase Adaptadora es capaz de trabajar tanto con la clase cliente como con la clase de servicio:
// implementa la interfaz con el cliente, mientras "envuelve" el objeto de la clase de servicio.
// La clase adaptadora recibe llamadas del cliente a través de la interfaz de cliente y las traduce en llamadas al objeto envuelto de la clase de servicio, pero en un formato que pueda comprender.

import ILaboratorioAdapter from "./ILaboratorioAdapter";

// utilizará el adaptador para interactuar con ANMADService:
export class Laboratorio {
  private adapter: ILaboratorioAdapter;

  constructor(adapter: ILaboratorioAdapter) {
    this.adapter = adapter;
  }

  async confirmarTransaccion(fOperacion: string, pIdsTransac: number) {
    try {
      const resultado = await this.adapter.confirmarTransaccion(fOperacion, pIdsTransac);
      console.log("Transacción exitosa:");
      console.log(resultado);
    } catch (error) {
      console.error("Error en la transacción:");
      console.error(error);
    }
  }
}
