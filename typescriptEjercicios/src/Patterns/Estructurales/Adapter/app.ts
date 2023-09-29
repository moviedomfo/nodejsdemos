// Adapter: Convierte
// Un adaptador envuelve uno de los objetos (que debe utilizar tu app -Servicio-) para esconder la complejidad de la conversión que tiene nececita
// tu app. Se trata de un objeto especial que convierte la interfaz de un objetoX, de forma que otro objeto_A pueda comprenderla.
//Esta implementación utiliza el principio de composición de objetos:
//el adaptador implementa la interfaz de un objeto y envuelve el otro.

// La clase Cliente contiene la lógica de negocio existente del programa.

// La Interfaz con el Cliente describe un protocolo que otras clases deben seguir para poder colaborar con el código cliente.

// Servicio es alguna clase útil (normalmente de una tercera parte o heredada). El cliente no puede utilizar directamente esta clase porque tiene una interfaz incompatible.

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
