using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xainport.Contracts
{
    public class AttestationSignatureContract
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "citizenPublicAddress")]
        public string CitizenPublicAddress { get; set; }

        [JsonProperty(PropertyName = "issuerAccountAddress")]
        public string IssuerAccountAddress { get; set; }

        [JsonProperty(PropertyName = "signature")]
        public string Signature { get; set; }
    }
}
