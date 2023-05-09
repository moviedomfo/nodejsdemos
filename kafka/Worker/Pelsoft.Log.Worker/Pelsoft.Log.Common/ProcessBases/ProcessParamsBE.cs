using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Pelsoft.Log.Common.ProcessBases
{
    public class ProcessParamsBE
    {
        private GroupAccountBE _GroupAccountBE;
        private AccountsInstancesServicesBE _AccountInfo;
        private int _currentProcessDetailsID;
        private ElementConvertBE _ElementConvertBE;
        private PelsoftEnums.ProcessAssembly _ProccesAssembly;

        public PelsoftEnums.ProcessAssembly ProccesAssembly
        {
            get { return _ProccesAssembly; }
            set { _ProccesAssembly = value; }
        }

        public ElementConvertBE ElementConvertBE
        {
            get { return _ElementConvertBE; }
            set { _ElementConvertBE = value; }
        }

        public int CurrentProcessDetailsID
        {
            get { return _currentProcessDetailsID; }
            set { _currentProcessDetailsID = value; }
        }

        public AccountsInstancesServicesBE AccountInfo
        {
            get { return _AccountInfo; }
            set { _AccountInfo = value; }
        }

        public GroupAccountBE GroupAccountBE
        {
            get { return _GroupAccountBE; }
            set { _GroupAccountBE = value; }
        }

    }
}
