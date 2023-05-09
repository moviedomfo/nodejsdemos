using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    public class UserChannelAccountBE
    {
        public int UCAId { get; set; }
        public int ClientId { get; set; }
        public DateTime UCACreated { get; set; }
        public DateTime UCAModifiedDate { get; set; }
        public int UCAModifiedByUserId { get; set; }
    }
}
