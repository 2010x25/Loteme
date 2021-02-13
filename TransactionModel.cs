using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotameDownloads
{

    public class TransactionRootobject
    {
        public Id id { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public Event[] events { get; set; }
    }

    public class Id
    {
        public string val { get; set; }
        public string type { get; set; }
    }

    public class Event
    {
        public string tap { get; set; }
        public string src { get; set; }
        public string subSrc { get; set; }
        public int ts { get; set; }
        public int[] add { get; set; }
        public int[] remove { get; set; }
    }

}
