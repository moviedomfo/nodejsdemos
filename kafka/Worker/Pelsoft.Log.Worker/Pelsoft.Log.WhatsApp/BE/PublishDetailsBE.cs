using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class PublishDetailsBEList : List<PublishDetailsBE>
    {

    }

    public class PublishDetailsBE
    {
        #region [Private Members]

        System.String _PDCommentAux1;
        System.String _PDCommentAux2;
        System.Int32 _PDCommentAux3;
        System.DateTime _PDCommentAux4;
        System.String _PDCommentAux5;


        #endregion

        #region [Constructor]

        public PublishDetailsBE()
        {

        }

        #endregion

        #region [Properties]

        #region [PDCommentAux1]
        public System.String PDCommentAux1
        {
            get { return _PDCommentAux1; }
            set { _PDCommentAux1 = value; }
        }
        #endregion

        #region [PDCommentAux1]
        public System.String PDCommentAux2
        {
            get { return _PDCommentAux2; }
            set { _PDCommentAux2 = value; }
        }
        #endregion

        #region [PDCommentAux3]
        public System.Int32 PDCommentAux3
        {
            get { return _PDCommentAux3; }
            set { _PDCommentAux3 = value; }
        }
        #endregion

        #region [PDCommentAux4]
        public System.DateTime PDCommentAux4
        {
            get { return _PDCommentAux4; }
            set { _PDCommentAux4 = value; }
        }
        #endregion

        #region PDCommentAux5
        public System.String PDCommentAux5
        {
            get { return _PDCommentAux5; }
            set { _PDCommentAux5 = value; }
        }
        #endregion

        #region PDJsonPublication
        public System.String PDJsonPublication
        {
            get; set;
        }
        #endregion

        #endregion


    }
}
