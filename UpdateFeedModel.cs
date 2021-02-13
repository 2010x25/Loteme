using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotameDownloads
{
    public partial class Temperatures
    {
        public Feed[] feeds { get; set; }
        public S3Creds s3creds { get; set; }
    }
    public partial class Feed
    {        
        public long Id { get; set; }      
        public string location { get; set; }       
        public string metaDataFile { get; set; }       
        public string[] files { get; set; }
    }
    public partial class S3Creds
    {         
        public string accessKeyId { get; set; }       
        public string secretAccessKey { get; set; }       
        public string sessionToken { get; set; }        
        public long expiration { get; set; }
    }
}
