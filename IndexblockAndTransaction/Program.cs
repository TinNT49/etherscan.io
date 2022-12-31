using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace IndexblockAndTransaction
{
    public class BlockNumerRange
    {
        public string startBlockNumber { get; set; }
        public string endBlockNumber { get; set; }
    }

    public class BlockNumberObject
    {
        public int BlockNumberData { get; set; }
        public string hash { get; set; }
        public string parentHash { get; set; }
        public string miner { get; set; }
        public decimal blockReward { get; set; }
        public decimal gasLimit { get; set; }
        public decimal gasUsed { get; set; }
    }

    class Program
    {
        public BlockNumerRange blockNumBerRanger = new BlockNumerRange();
        static HttpClient client = new HttpClient();

        private void InputBlockNumer()
        {
            Console.WriteLine("Please input start block Number: ");
            var inputStarBlockNumber = Console.ReadLine();
            Console.WriteLine("Please input end block Number: ");
            var inputEndBlockNumber = Console.ReadLine();
            blockNumBerRanger.startBlockNumber = inputStarBlockNumber.Trim();
            blockNumBerRanger.endBlockNumber = inputEndBlockNumber.Trim();
        }


        static async Task<BlockNumberObject> GetBlockNumberAsync(string path)
        {
            BlockNumberObject blockNumber = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                blockNumber = await response.Content.ReadAsAsync<BlockNumberObject>();
            }
            return blockNumber;
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://api.etherscan.io/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            BlockNumberObject blockNumber = new BlockNumberObject();
            int blockNumberInput = 12100001;
            string hex = blockNumberInput.ToString("X");
            String path = $"api?module=proxy&action=eth_getBlockByNumber&tag=0x10d4f&boolean=true&apikey=YourApiKeyToken";

            blockNumber = await GetBlockNumberAsync(path);

            Console.ReadLine();
        }

        static void Main(string[] args)
        {

            RunAsync().GetAwaiter().GetResult();

        }
    }
}
