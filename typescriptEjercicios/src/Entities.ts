export interface Animal {
  NombreEspecie: string;
  NombreCientifico: string;
  Familia: string;
  Clasificacion: "carnívoro" | "herbívoro" | "omnívoro";
  Oviparo: boolean;
  Invertebrado: boolean;
  Omnívoro: boolean;
}

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
