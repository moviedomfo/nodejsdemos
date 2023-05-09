using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class ElementAttentionQueueBE
    {
        #region [Private Members]

        private System.Int32 _CaseId;
        private System.Int32 _AttentionQueueId;
        private System.Int32 _AccountDetailId;
        private System.String _AsiConnectionStringDest;

        #endregion

        #region [CaseId]
        public System.Int32 CaseId
        {
            get { return _CaseId; }
            set { _CaseId = value; }
        }
        #endregion

        #region [AttentionQueueId]
        public System.Int32 AttentionQueueId
        {
            get { return _AttentionQueueId; }
            set { _AttentionQueueId = value; }
        }
        #endregion

        #region [AccountDetailId]
        public System.Int32 AccountDetailId
        {
            get { return _AccountDetailId; }
            set { _AccountDetailId = value; }
        }
        #endregion

        #region [AsiConnectionStringDest]
        public System.String AsiConnectionStringDest
        {
            get { return _AsiConnectionStringDest; }
            set { _AsiConnectionStringDest = value; }
        }
        #endregion


    }
}
