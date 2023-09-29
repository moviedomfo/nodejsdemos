export default interface IAnmadAdapter {
    confirmarTransaccion(fOperacion: string, pIdsTransac: number): Promise<any>;
  }

  