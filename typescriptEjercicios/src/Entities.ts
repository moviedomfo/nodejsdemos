export interface Animal {
  NombreEspecie: string;
  NombreCientifico: string;
  Familia: string;
  Clasificacion: "carnívoro" | "herbívoro" | "omnívoro";
  Oviparo: boolean;
  Invertebrado: boolean;
  Omnivoro: boolean;
}
export const animales: Animal[] = [
  {
    NombreEspecie: "León",
    NombreCientifico: "Panthera leo",
    Familia: "Felidae",
    Clasificacion: "carnívoro",
    Oviparo: false,
    Invertebrado: false,
    Omnivoro: false
  },
  {
    NombreEspecie: "Oso",
    NombreCientifico: "Ursidae",
    Familia: "Ursidae",
    Clasificacion: "omnívoro",
    Oviparo: false,
    Invertebrado: false,
    Omnivoro: true
  },
  {
    NombreEspecie: "Gallina",
    NombreCientifico: "Gallus gallus domesticus",
    Familia: "Phasianidae",
    Clasificacion: "omnívoro",
    Oviparo: true,
    Invertebrado: false,
    Omnivoro: true
  },
  // Puedes añadir más objetos aquí según sea necesario
];

export class Customer {
  id: number;
  name: string;
  address: Address;
  constructor(id: number, name: string, address: Address) {
    this.id = id;
    this.name = name;
    this.address = address;
  }
  clone(): Customer {
    const clonedAddress = this.address.clone();
    return new Customer(this.id, this.name, clonedAddress);
  }
}
export class Address {
  constructor(street: string, city: string, zipCode: string) {
    this.street = street;
    this.city = city;
    this.zipCode = zipCode;
  }
  street: string;
  city: string;
  zipCode: string;
  // Método para clonar la dirección de manera profunda
  clone(): Address {
    return new Address(this.street, this.city, this.zipCode);
  }
}

export class Person {
  id: number;
  name: string;
  lastName: string;
  address: Address[];


}