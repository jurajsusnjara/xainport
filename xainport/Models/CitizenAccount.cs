using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xainport.Models
{
    public class CitizenAccount
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "publicAddress")]
        public string PublicAddress { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "citizenAttestationsContractAddress")]
        public string CitizenAttestationsContractAddress { get; set; }

    }
}
