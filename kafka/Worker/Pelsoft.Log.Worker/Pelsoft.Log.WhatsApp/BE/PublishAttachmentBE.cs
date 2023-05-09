using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class PublishAttachmentBEList : List<PublishAttachmentBE>
    {

    }

    public class PublishAttachmentBE
    {

        public String MediaCategory { get; set; }
        #region [Private Members]

        System.String _PAttachmentName;
        Byte[] _PAttachment;
        System.String _PATransferEncoding;
        System.Boolean _PIsAttachmentOnBody;
        System.String _PAttachmentOnBody_Content;
        System.String _PAttachmentMediaType;
        System.String _PAttachmentLink;

        #endregion

        #region [Constructor]

        public PublishAttachmentBE()
        {

        }

        #endregion

        #region [Properties]

        #region [PAttachmentName]
        public System.String PAttachmentName
        {
            get { return _PAttachmentName; }
            set { _PAttachmentName = value; }
        }
        #endregion

        #region [PAttachment]
        public Byte[] PAttachment
        {
            get { return _PAttachment; }
            set { _PAttachment = value; }
        }
        #endregion

        #region [PATransferEncoding]
        public System.String PATransferEncoding
        {
            get { return _PATransferEncoding; }
            set { _PATransferEncoding = value; }
        }
        #endregion

        #region [PIsAttachmentOnBody]
        public System.Boolean PIsAttachmentOnBody
        {
            get { return _PIsAttachmentOnBody; }
            set { _PIsAttachmentOnBody = value; }
        }
        #endregion

        #region [PAttachmentOnBody_Content]
        public System.String PAttachmentOnBody_Content
        {
            get { return _PAttachmentOnBody_Content; }
            set { _PAttachmentOnBody_Content = value; }
        }
        #endregion

        #region [PAttachmentMediaType]
        public System.String PAttachmentMediaType
        {
            get { return _PAttachmentMediaType; }
            set { _PAttachmentMediaType = value; }
        }
        #endregion

        #region [PAttachmentLink]
        public System.String PAttachmentLink
        {
            get { return _PAttachmentLink; }
            set { _PAttachmentLink = value; }
        }
        #endregion


        #endregion


    }
}

