using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xainport.Ethereum.CitizenAttestations.ContractDefinition;

namespace Xainport.Ethereum.Documents
{
    public interface ICitizenAttestationRepository
    {
        Task AddAttestationSignature(string citizenAccountAddress, string attestationId, string attestationIssuerAccountAddress, string signature);

        Task<List<AttestationSignature>> GetCitizenSignatures(string citizenId);

        string GetContractAddress();
    }
}
