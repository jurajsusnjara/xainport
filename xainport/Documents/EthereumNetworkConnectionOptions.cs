using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xainport.Documents
{
    public class EthereumNetworkConnectionOptions
    {
        public string ConnectionUrl { get; set; }

        public string AccountPublicAddress { get; set; }

        public string AccountPrivateKey { get; set; }

        public string ContractAddress { get; set; }
    }
}
