using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class ElementConvertList : List<ElementConvertBE>
    {

    }
        
    public class ElementConvertBE
    {
        #region [Private Members]

        private System.Guid _ElementGUID;
        private System.DateTime _CreatedDate;
        private System.Guid _AccountDetailUnique;
        private System.Int32 _ServiceChannelId;
        private System.String _SCName;
        private System.Int32 _ProcessDetailIdConversionToCase;
        private System.Int32 _SCInternalCode;
        private System.Int32 _UserChannelId;
        private System.Int32 _ElementType;
        private System.Boolean _ElementTypePublic;
        private System.Int32 _ApplicationSettingsUserId;
        private System.DateTime _CreationDate;
        private System.DateTime _CreatedRowOrigen;
        private System.DateTime _CreationDateOriginal;
        private PelsoftEnums.ProcessAssembly _ProccesAssembly;


        #endregion

        #region [Properties]

        #region [ElementGUID]
        public System.Guid ElementGUID
        {
            get { return _ElementGUID; }
            set { _ElementGUID = value; }
        }
        #endregion

        #region [CreatedDate]
        public System.DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        #endregion

        #region [AccountDetailUnique]
        public Guid AccountDetailUnique
        {
            get { return _AccountDetailUnique; }
            set { _AccountDetailUnique = value; }
        }
        #endregion

        #region [ServiceChannelId]
        public System.Int32 ServiceChannelId
        {
            get { return _ServiceChannelId; }
            set { _ServiceChannelId = value; }
        }
        #endregion

        #region [SCName]
        public System.String SCName
        {
            get { return _SCName; }
            set { _SCName = value; }
        }
        #endregion

        #region [ProcessDetailIdConversionToCase]
        public System.Int32 ProcessDetailIdConversionToCase
        {
            get { return _ProcessDetailIdConversionToCase; }
            set { _ProcessDetailIdConversionToCase = value; }
        }
        #endregion

        #region [SCInternalCode]
        public System.Int32 SCInternalCode
        {
            get { return _SCInternalCode; }
            set { _SCInternalCode = value; }
        }
        #endregion

        //#region [UserChannelId]
        //public System.Int32 UserChannelId
        //{
        //    get { return _UserChannelId; }
        //    set { _UserChannelId = value; }
        //}
        //#endregion

        #region [ElementType]
        public System.Int32 ElementType
        {
            get { return _ElementType; }
            set { _ElementType = value; }
        }
        #endregion

        #region [ElementTypePublic]
        public System.Boolean ElementTypePublic
        {
            get { return _ElementTypePublic; }
            set { _ElementTypePublic = value; }
        }
        #endregion

        #region [ApplicationSettingsUserId]
        public System.Int32 ApplicationSettingsUserId
        {
            get { return _ApplicationSettingsUserId; }
            set { _ApplicationSettingsUserId = value; }
        }
        #endregion

        #region [CreationDate]
        public System.DateTime CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }
        #endregion

        #region [CreatedRowOrigen]
        public System.DateTime CreatedRowOrigen
        {
            get { return _CreatedRowOrigen; }
            set { _CreatedRowOrigen = value; }
        }
        #endregion

        #region [CreationDateOriginal]
        public System.DateTime CreationDateOriginal
        {
            get { return _CreationDateOriginal; }
            set { _CreationDateOriginal = value; }
        }
        #endregion

        public PelsoftEnums.ProcessAssembly ProccesAssembly
        {
            get { return _ProccesAssembly; }
            set { _ProccesAssembly = value; }
        }

        #endregion
    }
}