using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class PublishHelper
    {
        /// <summary>
        /// Obtiene el id del error.
        /// </summary>
        public static PublishErrorBE ManageError(AccountsInstancesServicesBE pAccountsInstancesServicesBE, Pelsoft.Log.Common.PelsoftEnums.Channeltype pServiceChannelType, Exception pEx, PublishBE pPublishBE, string pApplicationSettingsId, string pWebException = "")
        {
            Int32 wIdError = 0;
            
            //Verifico si el error ya se encuentra registrado.
            PublishErrorBE wPublishErrorBE = PublishDAC.GetPublishError(pAccountsInstancesServicesBE.AsiConnectionStringDest, pServiceChannelType, pEx.Message + " " + pWebException);

            if (wPublishErrorBE == null)
            {
                wIdError = PublishDAC.InsertPublishError(pAccountsInstancesServicesBE.AsiConnectionStringDest, pServiceChannelType, pEx, pWebException);

                //Valores por defecto.
                wPublishErrorBE = new PublishErrorBE();
                wPublishErrorBE.PEAlertedByService = true;
                wPublishErrorBE.PEContinueToNextContact = true;
                wPublishErrorBE.PEServiceCycleCancel = false;
                wPublishErrorBE.PublicationErrorId = wIdError;
            }

            //Registra el mensaje de error obtenido y actualiza la cantidad de reintentos en uno.
            PublishDAC.UpdatePublishError(pAccountsInstancesServicesBE.AsiConnectionStringDest, pPublishBE.PublicationId, wPublishErrorBE.PublicationErrorId, Convert.ToInt32(pApplicationSettingsId));

            return wPublishErrorBE;
        }
    }
}
