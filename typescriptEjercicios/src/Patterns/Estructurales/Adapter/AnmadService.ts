// que simula el servicio de terceros:
export default class ANMADService{

    // R
    SendConfirmaTransac(fOperacion: string, pIdsTransac: number): Promise<any> {
        // Simulación de respuesta exitosa
        if (fOperacion === 'OK') {
          const respuesta = {
            IdMedicamento: 123,
            NroGTIN: 'GTIN123',
            Nombre: 'Medicamento de ejemplo',
            LaboratorioOrigen: 'Laboratorio XYZ',
          };
          return Promise.resolve(respuesta);
        }
        
        // Simulación de respuesta de error
        const errorRespuesta = {
          mensajeError: 'Error en la transacción ANMAD',
        };
        return Promise.reject(errorRespuesta);
      }
}