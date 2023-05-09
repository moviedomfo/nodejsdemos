using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class UserChannelList : List<UserChannelBE>
    {}
        
    public class UserChannelBE
    {
        #region [Private Members]

        private System.Int32 _UserChannelId;
        private System.String _UCName;
        private System.Int32 _ClientId;
        private System.String _UCPublicationTo;
        
        #endregion

        #region [Properties]

        #region [UserChannelId]
        public System.Int32 UserChannelId
        {
            get { return _UserChannelId; }
            set { _UserChannelId = value; }
        }
        #endregion

        #region [UCName]
        public System.String UCName
        {
            get { return _UCName; }
            set { _UCName = value; }
        }
        #endregion

        //#region [ClientId]
        //public System.Int32 ClientId
        //{
        //    get { return _ClientId; }
        //    set { _ClientId = value; }
        //}
        //#endregion

        public int ClientId { get; set; }

        #region [UCPublicationTo]
        public System.String UCPublicationTo
        {
            get { return _UCPublicationTo; }
            set { _UCPublicationTo = value; }
        }
        #endregion

        #endregion
    }
}
