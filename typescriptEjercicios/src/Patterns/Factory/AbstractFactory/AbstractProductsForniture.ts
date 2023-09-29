//Interface para un conjunto relacionado de productos
// Definir la interfaz base del producto (IForniture)
export interface IForniture {
  HasLegs(): void;
}

//  Definir la interfaz del producto (IChair)
export interface IChair extends IForniture {
  SitOn(): void;
}

// Definir la interfaz del producto (ISofa)
export interface ISofa extends IForniture {
  Relax(): void;
}

export class VictorianChair implements IChair {
  HasLegs(): void {
    console.log("HasLegs  Victorian chair.");
  }
  SitOn(): void {
    console.log("SitOn  Victorian chair .");
  }
}
export class ModernChair implements IChair {
  HasLegs(): void {
    console.log("HasLegs  Modern Chair.");
  }
  SitOn(): void {
    console.log("SitOn   Modern Chair.");
  }
}

export class VictorianSofa implements ISofa {
  HasLegs(): void {
    console.log("HasLegs Victorian Sofa.");
  }
  Relax(): void {
    console.log("Relax Victorian  Sofa.");
  }
}
export class ModernSofa implements ISofa {
  HasLegs(): void {
    console.log("HasLegs Modern Sofa.");
  }
  Relax(): void {
    console.log("Relax  Modern Sofa.");
  }
}
