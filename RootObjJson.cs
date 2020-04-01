using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPayments
{
    public class RootJsonObject
    {
        [JsonProperty("transactions")]
        public IList<Dictionary<string, dynamic>> TransactionsItems { get; set; }

        [JsonProperty("removed")]
        public string[] Removed { set; get; }

        [JsonProperty("lastblock")]
        public string LastBlock { set; get; }
    }
}
