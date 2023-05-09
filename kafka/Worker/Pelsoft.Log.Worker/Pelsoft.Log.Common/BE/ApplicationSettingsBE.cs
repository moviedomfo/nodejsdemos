using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class ApplicationSettingsBEList : List<ApplicationSettingsBE>
    {

    }

    public class ApplicationSettingsBE
    {
        #region [Private Members]
               
        System.Int32 _SettingId;
        System.String _Description;
        System.String _Value;
        System.Int32 _PCApplicationSettings;

        #endregion

        #region [Constructor]

        public ApplicationSettingsBE()
        {

        }

        #endregion

        #region [Properties]

        #region [SettingId]
        public System.Int32 SettingId
        {
            get { return _SettingId; }
            set { _SettingId = value; }
        }
        #endregion

        #region [Description]
        public System.String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion

        #region [Value]
        public System.String Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion

        #region [PCApplicationSettings]
        public System.Int32 PCApplicationSettings
        {
            get { return _PCApplicationSettings; }
            set { _PCApplicationSettings = value; }
        }
        #endregion

        #endregion


    }
}


