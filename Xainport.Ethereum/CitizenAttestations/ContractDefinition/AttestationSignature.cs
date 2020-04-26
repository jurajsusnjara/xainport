using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Xainport.Ethereum.CitizenAttestations.ContractDefinition
{
    public partial class AttestationSignature : AttestationSignatureBase { }

    public class AttestationSignatureBase 
    {
        [Parameter("string", "attestationId", 1)]
        public virtual string AttestationId { get; set; }
        [Parameter("address", "attestationIssuer", 2)]
        public virtual string AttestationIssuer { get; set; }
        [Parameter("string", "issuerSignature", 3)]
        public virtual string IssuerSignature { get; set; }
    }
}
