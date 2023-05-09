using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class PublishBEList : List<PublishBE>
    {

    }

    public class PublishBE
    {
        #region [Private Members]

        System.Int32 _PublicationId;
        System.String _PComment;
        System.String _PublicationTo;
        System.Guid _AccountDetailUnique;
        System.DateTime _PublicationDate;
        System.Int32 _PChannelType;
        System.Int32 _PublicationErrorId;
        System.Int32 _PRetriesQuantity;
        System.DateTime _PModifiedDate;
        System.Int32 _PModifiedByUserId;
        System.Boolean _PAwaitingUserConfirmation;
        System.Int32 _ProcessDetailId;
        System.Boolean _PActiveFlag;
        System.String _PublicationCC;
        System.Guid _CaseCommentGuid;


        #endregion

        #region [Constructor]

        public PublishBE()
        {

        }

        #endregion

        #region [Properties]

        #region [PublicationId]
        public System.Int32 PublicationId
        {
            get { return _PublicationId; }
            set { _PublicationId = value; }
        }
        #endregion

        #region [PComment]
        public System.String PComment
        {
            get { return _PComment; }
            set { _PComment = value; }
        }
        #endregion

        #region [PublicationTo]
        public System.String PublicationTo
        {
            get { return _PublicationTo; }
            set { _PublicationTo = value; }
        }
        #endregion

        #region [AccountDetailUnique]
        public System.Guid AccountDetailUnique
        {
            get { return _AccountDetailUnique; }
            set { _AccountDetailUnique = value; }
        }
        #endregion

        #region [PublicationDate]
        public System.DateTime PublicationDate
        {
            get { return _PublicationDate; }
            set { _PublicationDate = value; }
        }
        #endregion

        #region [PChannelType]
        public System.Int32 PChannelType
        {
            get { return _PChannelType; }
            set { _PChannelType = value; }
        }
        #endregion

        #region [PublicationErrorId]
        public System.Int32 PublicationErrorId
        {
            get { return _PublicationErrorId; }
            set { _PublicationErrorId = value; }
        }
        #endregion

        #region [PRetriesQuantity]
        public System.Int32 PRetriesQuantity
        {
            get { return _PRetriesQuantity; }
            set { _PRetriesQuantity = value; }
        }
        #endregion

        #region [PModifiedDate]
        public System.DateTime PModifiedDate
        {
            get { return _PModifiedDate; }
            set { _PModifiedDate = value; }
        }
        #endregion

        #region [PModifiedByUserId]
        public System.Int32 PModifiedByUserId
        {
            get { return _PModifiedByUserId; }
            set { _PModifiedByUserId = value; }
        }
        #endregion

        #region [PAwaitingUserConfirmation]
        public System.Boolean PAwaitingUserConfirmation
        {
            get { return _PAwaitingUserConfirmation; }
            set { _PAwaitingUserConfirmation = value; }
        }
        #endregion

        #region [ProcessDetailId]
        public System.Int32 ProcessDetailId
        {
            get { return _ProcessDetailId; }
            set { _ProcessDetailId = value; }
        }
        #endregion

        #region [PActiveFlag]
        public System.Boolean PActiveFlag
        {
            get { return _PActiveFlag; }
            set { _PActiveFlag = value; }
        }
        #endregion

        #region PublicationCC
        public System.String PublicationCC
        {
            get { return _PublicationCC; }
            set { _PublicationCC = value; }
        }
        #endregion

        #region CaseCommentGuid
        public System.Guid CaseCommentGuid
        {
            get { return _CaseCommentGuid; }
            set { _CaseCommentGuid = value; }
        }
        #endregion

        #endregion
    }
}
