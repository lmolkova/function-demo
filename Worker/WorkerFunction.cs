using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    public static class WorkerFunction
    {
        private static readonly HttpClient httpClient = new HttpClient();
        [FunctionName("Worker")]
        public static async Task Run(
            [ServiceBusTrigger("workitems", Connection = "SB")]string myQueueItem,
            [Blob("workitems/done", FileAccess.Write, Connection = "OutStorageAccount")] Stream blob,
            ILogger log)
        {
            var  response = await httpClient.GetByteArrayAsync(myQueueItem);
            await blob.WriteAsync(response);
        }
    }
}
