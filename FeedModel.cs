using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotameDownloads
{

    public class FeedRoot
    {
        public Feed[] feeds { get; set; }
    }
    public class FeedModel
    {
        public long id { get; set; }

        public string type { get; set; }

        public string profileType { get; set; }

        public string regionName { get; set; }

        public bool includeChildClients { get; set; }

        public string datasetId { get; set; }

        public long percentageOfIdsIncluded { get; set; }

        public string metaDataFile { get; set; }
    }
}


