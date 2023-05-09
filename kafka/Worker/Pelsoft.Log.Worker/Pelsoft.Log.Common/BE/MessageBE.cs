

namespace Pelsoft.Log.Common
{

	public class MessageList :List<MessageBE>
	{}
	
	public class MessageBE
	{
		#region [Private Members]

		private System.Int32 _MessageId;

		private System.Int32 _FromUserId;

		private System.DateTime _MessageCreatedDate;

		private System.String _MessageText;

		private System.DateTime _MessageCreateRow;

		private System.Int32 _MConfigId;

		private Guid _MessageGUID;

		private System.DateTime _MessageProcessStart;

		private System.DateTime _MessageProcessEnd;

		private System.Int32 _ProcessDetailIdConversionToCase;

		private Guid _AccountDetailUnique;

		private System.DateTime _MessageCreatedDateOriginal;

        private System.Int32 _UserId;

        private System.String _UserName;

        private Guid _AccountDetailUniqueUser;

        private System.String _UserFirstName;

        private System.String _UserLastName;
         
		#endregion
		
		#region [Properties]

		#region [MessageId]
		public System.Int32 MessageId
		{
			get { return _MessageId; }
			set { _MessageId = value;}
		}
		#endregion


		#region [FromUserId]
		public System.Int32 FromUserId
		{
			get { return _FromUserId; }
			set { _FromUserId = value;}
		}
		#endregion


		#region [MessageCreatedDate]
		public System.DateTime MessageCreatedDate
		{
			get { return _MessageCreatedDate; }
			set { _MessageCreatedDate = value;}
		}
		#endregion


		#region [MessageText]
		public System.String MessageText
		{
			get { return _MessageText; }
			set { _MessageText = value;}
		}
		#endregion


		#region [MessageCreateRow]
		public System.DateTime MessageCreateRow
		{
			get { return _MessageCreateRow; }
			set { _MessageCreateRow = value;}
		}
		#endregion


		#region [MConfigId]
		public System.Int32 MConfigId
		{
			get { return _MConfigId; }
			set { _MConfigId = value;}
		}
		#endregion


		#region [MessageGUID]
		public Guid MessageGUID
		{
			get { return _MessageGUID; }
			set { _MessageGUID = value;}
		}
		#endregion


		#region [MessageProcessStart]
		public System.DateTime MessageProcessStart
		{
			get { return _MessageProcessStart; }
			set { _MessageProcessStart = value;}
		}
		#endregion


		#region [MessageProcessEnd]
		public System.DateTime MessageProcessEnd
		{
			get { return _MessageProcessEnd; }
			set { _MessageProcessEnd = value;}
		}
		#endregion


		#region [ProcessDetailIdConversionToCase]
		public System.Int32 ProcessDetailIdConversionToCase
		{
			get { return _ProcessDetailIdConversionToCase; }
			set { _ProcessDetailIdConversionToCase = value;}
		}
		#endregion


		#region [AccountDetailUnique]
		public Guid AccountDetailUnique
		{
			get { return _AccountDetailUnique; }
			set { _AccountDetailUnique = value;}
		}
		#endregion


		#region [MessageCreatedDateOriginal]
		public System.DateTime MessageCreatedDateOriginal
		{
			get { return _MessageCreatedDateOriginal; }
			set { _MessageCreatedDateOriginal = value;}
		}
		#endregion

        #region [AccountDetailUniqueUser]
        public Guid AccountDetailUniqueUser
        {
            get { return _AccountDetailUniqueUser; }
            set { _AccountDetailUniqueUser = value; }
        }
        #endregion

        #region [UserId]
		public System.Int32 UserId
		{
			get { return _UserId; }
			set { _UserId = value;}
		}
		#endregion

        #region [UserName]
		public System.String UserName
		{
			get { return _UserName; }
			set { _UserName = value;}
		}
		#endregion

        #region [UserFirstName]
        public System.String UserFirstName
        {
            get { return _UserFirstName; }
            set { _UserFirstName = value; }
        }
        #endregion

        #region [UserLastName]
        public System.String UserLastName
        {
            get { return _UserLastName; }
            set { _UserLastName = value; }
        }
        #endregion

        #endregion
	}
}