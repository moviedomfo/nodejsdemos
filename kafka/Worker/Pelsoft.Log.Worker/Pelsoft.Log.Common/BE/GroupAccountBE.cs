using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{

    public class GroupAccountList : List<GroupAccountBE>
    { }

    public class GroupAccountBE
    {
        #region [Private Members]
        private System.Int32 _GroupAccountId;

        private System.String _GroupAccountName;

        private System.DateTime _GroupAccountStartDate;

        private System.DateTime? _GroupAccountEndDate;

        private System.DateTime _GroupAccountCreated;

        private System.Boolean _GroupAccountActiveFlag;

        private System.Boolean _GroupAccountMergeComments;

        private System.Boolean? _GroupAccountMergeCommentsDifferentParents;

        private System.Boolean? _GroupAccountMergeCommentsToOwner;

        private System.Boolean _GroupAccountMergePrivateComments;

        private System.Int32 _GroupAccountOpenCaseSearchTime;

        private System.Boolean _GroupAccountConvertMessageToCase;

        private System.Boolean _GroupAccountReOpenCase;

        private System.Int32 _GroupAccountClosedCaseSearchTime;

        private System.Boolean _GroupAccountReleaseCase;

        private System.Boolean _GroupAccountCreateOwnerUniqueCase;

        private System.Int32 _GroupAccountModifiedByUserId;

        private System.DateTime _GroupAccountModifiedDate;

        private Guid _AccountGroupUnique;

        private System.Boolean _GroupAccountFindLastOpenCase;

        private System.Boolean _GroupAccountFindLastClosedCase;

        private System.Int32 _GroupAccountReleaseCaseTime;
        
        #endregion

        #region [Properties]

        #region [GroupAccountId]
        public System.Int32 GroupAccountId
        {
            get { return _GroupAccountId; }
            set { _GroupAccountId = value; }
        }
        #endregion

        #region [GroupAccountName]
        public System.String GroupAccountName
        {
            get { return _GroupAccountName; }
            set { _GroupAccountName = value; }
        }
        #endregion

        #region [GroupAccountStartDate]
        public System.DateTime GroupAccountStartDate
        {
            get { return _GroupAccountStartDate; }
            set { _GroupAccountStartDate = value; }
        }
        #endregion

        #region [GroupAccountEndDate]
        public System.DateTime? GroupAccountEndDate
        {
            get { return _GroupAccountEndDate; }
            set { _GroupAccountEndDate = value; }
        }
        #endregion

        #region [GroupAccountCreated]
        public System.DateTime GroupAccountCreated
        {
            get { return _GroupAccountCreated; }
            set { _GroupAccountCreated = value; }
        }
        #endregion

        #region [GroupAccountActiveFlag]
        public System.Boolean GroupAccountActiveFlag
        {
            get { return _GroupAccountActiveFlag; }
            set { _GroupAccountActiveFlag = value; }
        }
        #endregion

        #region [GroupAccountMergeComments]
        public System.Boolean GroupAccountMergeComments
        {
            get { return _GroupAccountMergeComments; }
            set { _GroupAccountMergeComments = value; }
        }
        #endregion

        #region [GroupAccountMergeCommentsDifferentParents]
        public System.Boolean? GroupAccountMergeCommentsDifferentParents
        {
            get { return _GroupAccountMergeCommentsDifferentParents; }
            set { _GroupAccountMergeCommentsDifferentParents = value; }
        }
        #endregion

        #region [GroupAccountMergeCommentsToOwner]
        public System.Boolean? GroupAccountMergeCommentsToOwner
        {
            get { return _GroupAccountMergeCommentsToOwner; }
            set { _GroupAccountMergeCommentsToOwner = value; }
        }
        #endregion

        #region [GroupAccountMergePrivateComments]
        public System.Boolean GroupAccountMergePrivateComments
        {
            get { return _GroupAccountMergePrivateComments; }
            set { _GroupAccountMergePrivateComments = value; }
        }
        #endregion

        #region [GroupAccountOpenCaseSearchTime]
        public System.Int32 GroupAccountOpenCaseSearchTime
        {
            get { return _GroupAccountOpenCaseSearchTime; }
            set { _GroupAccountOpenCaseSearchTime = value; }
        }
        #endregion

        #region [GroupAccountConvertMessageToCase]
        public System.Boolean GroupAccountConvertMessageToCase
        {
            get { return _GroupAccountConvertMessageToCase; }
            set { _GroupAccountConvertMessageToCase = value; }
        }
        #endregion

        #region [GroupAccountReOpenCase]
        public System.Boolean GroupAccountReOpenCase
        {
            get { return _GroupAccountReOpenCase; }
            set { _GroupAccountReOpenCase = value; }
        }
        #endregion

        #region [GroupAccountClosedCaseSearchTime]
        public System.Int32 GroupAccountClosedCaseSearchTime
        {
            get { return _GroupAccountClosedCaseSearchTime; }
            set { _GroupAccountClosedCaseSearchTime = value; }
        }
        #endregion

        #region [GroupAccountReleaseCase]
        public System.Boolean GroupAccountReleaseCase
        {
            get { return _GroupAccountReleaseCase; }
            set { _GroupAccountReleaseCase = value; }
        }
        #endregion

        #region [GroupAccountCreateOwnerUniqueCase]
        public System.Boolean GroupAccountCreateOwnerUniqueCase
        {
            get { return _GroupAccountCreateOwnerUniqueCase; }
            set { _GroupAccountCreateOwnerUniqueCase = value; }
        }
        #endregion

        #region [GroupAccountModifiedByUserId]
        public System.Int32 GroupAccountModifiedByUserId
        {
            get { return _GroupAccountModifiedByUserId; }
            set { _GroupAccountModifiedByUserId = value; }
        }
        #endregion

        #region [GroupAccountModifiedDate]
        public System.DateTime GroupAccountModifiedDate
        {
            get { return _GroupAccountModifiedDate; }
            set { _GroupAccountModifiedDate = value; }
        }
        #endregion

        #region [AccountGroupUnique]
        public Guid AccountGroupUnique
        {
            get { return _AccountGroupUnique; }
            set { _AccountGroupUnique = value; }
        }
        #endregion

        #region [GroupAccountFindLastOpenCase]
        public System.Boolean GroupAccountFindLastOpenCase
        {
            get { return _GroupAccountFindLastOpenCase; }
            set { _GroupAccountFindLastOpenCase = value; }
        }
        #endregion

        #region [GroupAccountFindLastClosedCase]
        public System.Boolean GroupAccountFindLastClosedCase
        {
            get { return _GroupAccountFindLastClosedCase; }
            set { _GroupAccountFindLastClosedCase = value; }
        }
        #endregion

        #region [GroupAccountReleaseCaseTime]
        public System.Int32 GroupAccountReleaseCaseTime
        {
            get { return _GroupAccountReleaseCaseTime; }
            set { _GroupAccountReleaseCaseTime = value; }
        }
        #endregion

        #endregion
    }
}

