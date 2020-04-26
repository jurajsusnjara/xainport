using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Xainport.Models
{
    public class Covid19Data
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "createdTime")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty(PropertyName = "citizenAccountAddress")]
        public string CitizenAccountAddress { get; set; }

        [JsonProperty(PropertyName = "issuerAccountAddress")]
        public string IssuerAccountAddress { get; set; }

        [JsonProperty(PropertyName = "contractAddress")]
        public string ContractAddress { get; set; }

        [JsonProperty(PropertyName = "covid19Attestation")]
        public AttestationData Covid19Attestation { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public class AttestationData
        {
            [JsonProperty(PropertyName = "result")]
            public string Result { get; set; }

            [JsonProperty(PropertyName = "testedTime")]
            public DateTime TestedTime { get; set; }
        }
    }
}
