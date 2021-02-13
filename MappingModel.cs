using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotameDownloads
{
    public partial class MappingRoot
    {
        public long behavior_id { get; set; }
        public HierarchyNode[] hierarchy_nodes { get; set; }
    }

    public partial class HierarchyNode
    {
        public long id { get; set; }
        public string path { get; set; }
    }
}
