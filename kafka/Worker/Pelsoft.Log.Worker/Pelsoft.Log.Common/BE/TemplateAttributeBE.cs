using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Pelsoft.Log.Common
{


    public class TemplateAttributeList : List<TemplateAttributeBE>
	{}

    
	public class TemplateAttributeBE
	{
		#region [Private Members]

		private System.String _TemplateAttributeName;

        private System.String _TemplateAttributeNameToShow;
        
        private System.Int32 _TemplateAttributeOrder;
        
		#endregion
		
		#region [Properties]

        #region [TemplateAttributeName]
        public System.String TemplateAttributeName
		{
            get { return _TemplateAttributeName; }
            set { _TemplateAttributeName = value; }
		}
		#endregion

        #region [_TemplateAttributeNameToShow]
        public System.String TemplateAttributeNameToShow
        {
            get { return _TemplateAttributeNameToShow; }
            set { _TemplateAttributeNameToShow = value; }
        }
        #endregion

        #region [_TemplateAttributeOrder]
        public System.Int32 TemplateAttributeOrder
        {
            get { return _TemplateAttributeOrder; }
            set { _TemplateAttributeOrder = value; }
        }
        #endregion

		#endregion
	}
}
