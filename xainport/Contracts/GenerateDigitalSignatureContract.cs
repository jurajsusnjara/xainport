using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xainport.Contracts
{
    public class GenerateDigitalSignatureContract
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "citizenAccountAddress")]
        public string CitizenAccountAddress { get; set; }

        [JsonProperty(PropertyName = "issuerAccountAddress")]
        public string IssuerAccountAddress { get; set; }

        [JsonProperty(PropertyName = "createdTime")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty(PropertyName = "payload")]
        public string Payload { get; set; }

        [JsonProperty(PropertyName = "privateKey")]
        public string PrivateKey { get; set; }
    }
}
