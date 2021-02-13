using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LotameDownloads
{
    class Program
    {

        private const string bucketName = "lotame-firehose-mena";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;

        static void Main(string[] args)
        {         
            Init();           
        }
        private static SessionAWSCredentials GetTemporaryCredentialsAsync(S3Creds cred)
        {
            return new SessionAWSCredentials
                       (cred.accessKeyId, cred.secretAccessKey, cred.sessionToken);
        }

        /// <summary>
        /// Download zip files
        /// </summary>
        /// <param name="feed"></param>
        /// <param name="creds"></param>
        /// <param name="baseDirectory"></param>
        static void DownloadFiles(Feed feed, S3Creds creds, string baseDirectory)
        {
            var s3Client = new AmazonS3Client(GetTemporaryCredentialsAsync(creds), bucketRegion);
            try
            {
                var arr = feed.location.Split('/');
                var bucketName = arr[2].Replace("s3://", "").Trim();
                var baseKey = feed.location.Replace("s3://" + arr[2], "").Trim().TrimStart('/');                
                foreach(var file in feed.files)
                {
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key =  baseKey + "/" + file
                    };

                    Log(string.Format("Downloading.. {0}" , request.Key));
                    using (GetObjectResponse response = s3Client.GetObjectAsync(request).Result)
                    {
                        response.WriteResponseStreamToFile
                            (string.Concat(baseDirectory , feed.Id ,  @"\" + file));
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                Log(string.Format("Error encountered ***. Message:'{0}' when writing an object", e.Message));
            }

            catch (Exception e)
            {
                Log(string.Format("Unknown encountered on server. Message:'{0}' when writing an object", e.Message));
            }
        }

        /// <summary>
        /// Download meta data file....
        /// </summary>
        /// <param name="feed"></param>
        /// <param name="creds"></param>
        /// <param name="baseDirectory"></param>
        static async Task DownloadMetaFiles(Feed feed, S3Creds creds, string baseDirectory)
        {
            var s3Client = new AmazonS3Client(GetTemporaryCredentialsAsync(creds), bucketRegion);
            try
            {
                var arr = feed.metaDataFile.Split('/');
                var bucketName = arr[2].Replace("s3://", "").Trim();
                var baseKey = feed.metaDataFile.Replace("s3://" + arr[2], "").Trim().TrimStart('/');
                Console.WriteLine(baseKey);
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = baseKey
                    };
                    using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
                    {
                        response.WriteResponseStreamToFile(string.Concat
                            (baseDirectory , feed.Id + @"\_" , Path.GetFileName(baseKey)));
                    }
               
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
        static async Task Init()
        {
            string baseDirectory = ConfigurationManager.AppSettings["DownloadPath"];

            // Get S3 access token from Lotame API start.....
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.lotame.com");
            httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("x-lotame-token", "<<your lotame token>>");
            httpClient.DefaultRequestHeaders.Add("x-lotame-access", "<<your lotame access key>>");
            var response = await httpClient.GetAsync
                    ("2/firehose/updates?client_id=<<yourclientId>>&include_latest=false");
            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Temperatures>(json);
            // Get S3 access token from Lotame API end.....

            foreach (var feed in model.feeds)
            {
                DownloadFiles(feed, model.s3creds, baseDirectory);                
            }
        }
        static void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
