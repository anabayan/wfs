
Source URL: https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator#running-on-docker

Running on Docker
The Azure Cosmos Emulator can be run on Docker for Windows. The emulator does not work on Docker for Oracle Linux.

Once you have Docker for Windows installed, switch to Windows containers by right-clicking the Docker icon on the toolbar and selecting Switch to Windows containers.

Next, pull the emulator image from Docker Hub by running the following command from your favorite shell.

bash


docker pull mcr.microsoft.com/cosmosdb/windows/azure-cosmos-emulator
To start the image, run the following commands.

From the command-line:

cmd



md %LOCALAPPDATA%\CosmosDBEmulator\bind-mount

docker run --name azure-cosmosdb-emulator --memory 2GB --mount "type=bind,source=%LOCALAPPDATA%\CosmosDBEmulator\bind-mount,destination=C:\CosmosDB.Emulator\bind-mount" --interactive --tty -p 8081:8081 -p 8900:8900 -p 8901:8901 -p 8902:8902 -p 10250:10250 -p 10251:10251 -p 10252:10252 -p 10253:10253 -p 10254:10254 -p 10255:10255 -p 10256:10256 -p 10350:10350 mcr.microsoft.com/cosmosdb/windows/azure-cosmos-emulator
 Note

If you see a port conflict error (specified port is already in use) when you run the docker run command, you can pass a custom port by altering the port numbers. For example, you can change the "-p 8081:8081" to "-p 443:8081"

From PowerShell:

PowerShell



md $env:LOCALAPPDATA\CosmosDBEmulator\bind-mount 2>null

docker run --name azure-cosmosdb-emulator --memory 2GB --mount "type=bind,source=$env:LOCALAPPDATA\CosmosDBEmulator\bind-mount,destination=C:\CosmosDB.Emulator\bind-mount" --interactive --tty -p 8081:8081 -p 8900:8900 -p 8901:8901 -p 8902:8902 -p 10250:10250 -p 10251:10251 -p 10252:10252 -p 10253:10253 -p 10254:10254 -p 10255:10255 -p 10256:10256 -p 10350:10350 mcr.microsoft.com/cosmosdb/windows/azure-cosmos-emulator

The response looks similar to the following:



Starting emulator
Emulator Endpoint: https://172.20.229.193:8081/
Master Key: C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
Exporting SSL Certificate
You can import the SSL certificate from an administrator command prompt on the host by running:
cd /d %LOCALAPPDATA%\CosmosDBEmulatorCert
powershell .\importcert.ps1
--------------------------------------------------------------------------------------------------
Starting interactive shell
Now use the endpoint and master key-in from the response in your client and import the SSL certificate into your host. To import the SSL certificate, do the following from an admin command prompt:

From the command-line:

cmd


cd  %LOCALAPPDATA%\CosmosDBEmulator\bind-mount
powershell .\importcert.ps1
From PowerShell:

PowerShell


cd $env:LOCALAPPDATA\CosmosDBEmulator\bind-mount
.\importcert.ps1
Closing the interactive shell once the emulator has been started will shut down the emulator’s container.

To open the Data Explorer, navigate to the following URL in your browser. The emulator endpoint is provided in the response message shown above.



https://<emulator endpoint provided in response>/_explorer/index.html
If you have a .NET client application running on a Linux docker container and if you are running Azure Cosmos emulator on a host machine, in this case you can’t connect to the Azure Cosmos account from the emulator. Because the app is not running on the host machine, the certificate registered on the Linux container that matches the emulator’s endpoint cannot be added.

As a workaround, you can disable the server’s SSL certificate validation from your client application by passing a HttpClientHandler instance as shown in the following .Net code sample. This workaround is only applicable if you are using the Microsoft.Azure.DocumentDB Nuget package, it isn't supported with the Microsoft.Azure.Cosmos Nuget package:

C#


var httpHandler = new HttpClientHandler()
{
   ServerCertificateCustomValidationCallback = (req,cert,chain,errors) => true
};

using (DocumentClient client = new DocumentClient(new Uri(strEndpoint), strKey, httpHandler))
{
   RunDatabaseDemo(client).GetAwaiter().GetResult();
}
In addition to disabling the SSL certificate validation, it is important that you start the emulator with the /allownetworkaccess option and the emulator’s endpoint is accessible from the host IP address rather than host.docker.internal DNS.