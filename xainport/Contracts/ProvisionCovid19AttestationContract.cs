using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xainport.Models;

namespace Xainport.Contracts
{
    public class ProvisionCovid19AttestationContract
    {
        [JsonProperty("citizenUsername")]
        public string CitizenUsername { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("attestation")]
        public Covid19Data.AttestationData Attestation { get; set; }
    }
}
