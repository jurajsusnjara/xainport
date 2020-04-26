using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Xainport.Ethereum.Documents
{
    public interface ICitizenAttestationRepository
    {
        Task AddAttestationSignature(string citizenAccountAddress, string attestationId, string attestationIssuerAccountAddress, string signature);

        Task<string> GetAttestationSignature(string citizenId, string attestationId);

        string GetContractAddress();
    }
}
