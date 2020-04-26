using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xainport.Contracts;
using Xainport.Documents;
using Xainport.Ethereum.CitizenAttestations.ContractDefinition;
using Xainport.Ethereum.Documents;
using Xainport.Ethereum.Utility;
using Xainport.Models;

namespace Xainport.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttestationsController : ControllerBase
    {
        private readonly ICitizenAccountRepository citizenAccountRepository;
        private readonly IIssuingAuthorityRepository issuingAuthorityRepository;
        private readonly EthereumNetworkConnectionOptions ethereumNetworkConnection;

        public AttestationsController(
            ICitizenAccountRepository citizenAccountRepository,
            IIssuingAuthorityRepository issuingAuthorityRepository,
            IOptions<EthereumNetworkConnectionOptions> ethereumNetworkConnectionOptions)
        {
            this.citizenAccountRepository = citizenAccountRepository;
            this.issuingAuthorityRepository = issuingAuthorityRepository;
            ethereumNetworkConnection = ethereumNetworkConnectionOptions.Value;
        }

        [HttpGet("getdigitalsignatures/{citizenAccountAddress}")]
        public async Task<List<AttestationSignature>> GetDigitalSignature(string citizenAccountAddress)
        {
            ICitizenAttestationRepository ethereumCitizenAttestationRepository = CitizenAttestationRepository.
                ConstructCitizenAttestationRepositoryWithExistingContract(
                ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, ethereumNetworkConnection.ContractAddress);

            List<AttestationSignature> attestationSignatures = await ethereumCitizenAttestationRepository.GetCitizenSignatures(citizenAccountAddress);

            return attestationSignatures;
        }

        // returns smart contract address where information is stored
        [HttpPost("publishdigitalsignature")]
        public async Task<string> PublishDigitalSignature([FromBody] AttestationSignatureContract attestationSignatureContract)
        {
            ICitizenAttestationRepository ethereumCitizenAttestationRepository = CitizenAttestationRepository.
                    ConstructCitizenAttestationRepositoryWithExistingContract(
                    ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, ethereumNetworkConnection.ContractAddress);

            await ethereumCitizenAttestationRepository.AddAttestationSignature(
                attestationSignatureContract.CitizenPublicAddress, attestationSignatureContract.Id, attestationSignatureContract.IssuerAccountAddress, attestationSignatureContract.Signature);

            return ethereumNetworkConnection.ContractAddress;
        }
    }
}