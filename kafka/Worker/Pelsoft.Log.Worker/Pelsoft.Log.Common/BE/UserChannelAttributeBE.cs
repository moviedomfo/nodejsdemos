using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class UserChannelAttributeList : List<UserChannelAttributeBE>
    {}
        
    public class UserChannelAttributeBE
    {
        #region [Private Members]

        private System.Int32 _UserChannelAttributeId;
        private System.Int32 _UserChannelId;
        private System.String _Attribute;
        private System.String _Value;
          
        #endregion

        #region [Properties]

        #region [UserChannelAttributeId]
        public System.Int32 UserChannelAttributeId
        {
            get { return _UserChannelAttributeId; }
            set { _UserChannelAttributeId = value; }
        }
        #endregion

        #region [UserChannelId]
        public System.Int32 UserChannelId
        {
            get { return _UserChannelId; }
            set { _UserChannelId = value; }
        }
        #endregion

        #region [Attribute]
        public System.String Attribute
        {
            get { return _Attribute; }
            set { _Attribute = value; }
        }
        #endregion

        #region [Value]
        public System.String Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion

        #endregion
    }
}

