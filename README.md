# Access Loteme API in C#. 
All of the code found in this repo rely on the C# which may not be written in a elegant way to work with the Lotame API but solve the purpose. Example in this repo is helpful to you to get an idea to access how to access Lotame API in C# and download data. 

# Dependencies
Restore  AWSSDK.Core &  AWSSDK.S3 packages from Nuget.

```csharp
   httpClient.DefaultRequestHeaders.Add("x-lotame-token", "<<your lotame token>>");
   
   httpClient.DefaultRequestHeaders.Add("x-lotame-access", "<<your lotame access key>>");
   
   var response = await httpClient.GetAsync
                    ("2/firehose/updates?client_id=<<your client Id>>&include_latest=false");

```

x-lotame-token - Get token value from loteme admin portal.

x-lotame-access - Get access key from loteme admin portal

client_id - Numeric identification number will be available on loteme portal or provided by loteme support team.
