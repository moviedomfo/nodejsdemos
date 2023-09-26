// En este ejemplo, hemos creado una jerarquía de clases que sigue el patrón Factory Method.
// Logistic es la clase abstracta Creator con el método planDelivery, que utiliza el Factory Method createTransport para crear objetos ITransportMedia.
// Las clases concretas RoadLogistic y SeaLogistic implementan createTransport para crear objetos Trunck y Ships, respectivamente,
// que implementan la interfaz ITransportMedia.

// Cuando llamamos al método planDelivery en las instancias de RoadLogistic y SeaLogistic, se crea el objeto correspondiente y
// se llama al método deliver del producto resultante.
import {RoadLogistic, SeaLogistic} from "./CreatorLogistic";

/**
 * Client App  that use the pathern
 */
export class LogisticClientApp {
  public DoWork() {
    //Uso del patrón Factory Method
    const roadLogistic = new RoadLogistic();
    const seaLogistic = new SeaLogistic();
    roadLogistic.planDelivery();
    seaLogistic.planDelivery();
  }
}
