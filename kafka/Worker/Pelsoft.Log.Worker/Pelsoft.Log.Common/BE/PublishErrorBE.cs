using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class PublishErrorBEList : List<PublishErrorBE>
    {

    }

    public class PublishErrorBE
    {
        #region [Private Members]
               
        System.Int32 _PublicationErrorId;
        System.Boolean _PEAlertedByService;
        System.Boolean _PEContinueToNextContact;
        System.Boolean _PEServiceCycleCancel;
 
        #endregion

        #region [Constructor]

        public PublishErrorBE()
        {

        }

        #endregion

        #region [Properties]

        #region [PublicationErrorId]
        public System.Int32 PublicationErrorId
        {
            get { return _PublicationErrorId; }
            set { _PublicationErrorId = value; }
        }
        #endregion

        #region [PEAlertedByService]
        public System.Boolean PEAlertedByService
        {
            get { return _PEAlertedByService; }
            set { _PEAlertedByService = value; }
        }
        #endregion

        #region [PEContinueToNextContact]
        public System.Boolean PEContinueToNextContact
        {
            get { return _PEContinueToNextContact; }
            set { _PEContinueToNextContact = value; }
        }
        #endregion

        #region [PEServiceCycleCancel]
        public System.Boolean PEServiceCycleCancel
        {
            get { return _PEServiceCycleCancel; }
            set { _PEServiceCycleCancel = value; }
        }
        #endregion


        #endregion


    }
}


