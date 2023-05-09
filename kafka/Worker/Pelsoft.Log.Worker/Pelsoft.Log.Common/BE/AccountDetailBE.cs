using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class AccountDetailList : List<AccountDetailBE>
    {

    }

    public class AccountDetailBE
    {
        #region [Private Members]

        private System.Int32 _AccountDetailId;

        private Guid _AccountDetailUnique;

        private System.String _AccountDetailDescrip;

        private System.Boolean _AccountDetailActiveFlag;

        private System.DateTime _AccountDetailCreated;

        private System.Int32 _ServiceChannelId;

        private System.Int32? _OwnerId;

        private System.Int32 _AccountDetailModifiedByUserId;

        private System.DateTime _AccountDetailModifiedDate;

        private System.Int32 _AccountId;

        private System.String _AccountName;

        private System.String _SCName;

        private System.Int32 _SCInternalCode;

        private System.String _UCName;

        private System.String _UserName;

        private System.Int32 _GroupAccountReleaseCase;

        private System.Int32 _SourceUserChannelId;

        private System.Int32 _AccountTypeId;
        
        #endregion

        #region [Properties]

		#region [AccountDetailId]
		public System.Int32 AccountDetailId
		{
			get { return _AccountDetailId; }
			set { _AccountDetailId = value;}
		}
		#endregion

		#region [AccountDetailUnique]
		public Guid AccountDetailUnique
		{
			get { return _AccountDetailUnique; }
			set { _AccountDetailUnique = value;}
		}
		#endregion

		#region [AccountDetailDescrip]
		public System.String AccountDetailDescrip
		{
			get { return _AccountDetailDescrip; }
			set { _AccountDetailDescrip = value;}
		}
		#endregion

		#region [AccountDetailActiveFlag]
		public System.Boolean AccountDetailActiveFlag
		{
			get { return _AccountDetailActiveFlag; }
			set { _AccountDetailActiveFlag = value;}
		}
		#endregion

		#region [AccountDetailCreated]
		public System.DateTime AccountDetailCreated
		{
			get { return _AccountDetailCreated; }
			set { _AccountDetailCreated = value;}
		}
		#endregion

		#region [ServiceChannelId]
		public System.Int32 ServiceChannelId
		{
			get { return _ServiceChannelId; }
			set { _ServiceChannelId = value;}
		}
		#endregion

		#region [OwnerId]
		public System.Int32? OwnerId
		{
			get { return _OwnerId; }
			set { _OwnerId = value;}
		}
		#endregion

		#region [AccountDetailModifiedByUserId]
		public System.Int32 AccountDetailModifiedByUserId
		{
			get { return _AccountDetailModifiedByUserId; }
			set { _AccountDetailModifiedByUserId = value;}
		}
		#endregion

		#region [AccountDetailModifiedDate]
		public System.DateTime AccountDetailModifiedDate
		{
			get { return _AccountDetailModifiedDate; }
			set { _AccountDetailModifiedDate = value;}
		}
		#endregion

        #region [AccountId]
        public System.Int32 AccountId
        {
            get { return _AccountId; }
            set { _AccountId = value; }
        }
        #endregion

        #region [AccountName]
        public System.String AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }
        #endregion

        #region [SCName]
        public System.String SCName
        {
            get { return _SCName; }
            set { _SCName = value; }
        }
        #endregion

        #region [SCInternalCode]
        public System.Int32 SCInternalCode
        {
            get { return _SCInternalCode; }
            set { _SCInternalCode = value; }
        }
        #endregion

        #region [UCName]
        public System.String UCName
        {
            get { return _UCName; }
            set { _UCName = value; }
        }
        #endregion

        #region [UserName]
        public System.String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        #endregion

        #region [GroupAccountReleaseCase]
        public System.Int32 GroupAccountReleaseCase
        {
            get { return _GroupAccountReleaseCase; }
            set { _GroupAccountReleaseCase = value; }
        }
        #endregion

        #region [SourceUserChannelId]
        public System.Int32 SourceUserChannelId
        {
            get { return _SourceUserChannelId; }
            set { _SourceUserChannelId = value; }
        }
        #endregion

        #region [AccountTypeId]
        public System.Int32 AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }
        #endregion

		#endregion
    }
}
