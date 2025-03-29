import ANMADService from "./AnmadService";
import IAnmadAdapter from "./ILaboratorioAdapter";

export class LaboratorioAdapter implements IAnmadAdapter {
  private anmadService: ANMADService;

  constructor() {
    this.anmadService = new ANMADService();
  }

  // Implementación del método de la interfaz IAnmadAdapter
  // que utiliza el servicio ANMADService para confirmar la transacción:    
  async confirmarTransaccion(fOperacion: string, pIdsTransac: number): Promise<any> {
    return this.anmadService.SendConfirmaTransac(fOperacion, pIdsTransac);
  }
}
