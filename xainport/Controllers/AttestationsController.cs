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

        [HttpGet("issuingauthorityinfo/{publicAddress}")]
        public async Task<IssuingAuthority> GetIssuingAuthorityInfo(string publicAddress)
        {
            return await issuingAuthorityRepository.GetIssuingAuthority(publicAddress);
        }

        // returns smart contract address where information is stored
        [HttpPost("publishdigitalsignature")]
        public async Task<string> PublishDigitalSignature([FromBody] AttestationSignatureContract attestationSignatureContract)
        {
            CitizenAccount citizenAccount = await citizenAccountRepository.
                GetCitizenAccountForPublicAddress(attestationSignatureContract.CitizenPublicAddress);

            if (citizenAccount == null)
            {
                throw new InvalidOperationException("Citizen account doesn't exist");
            }

            ICitizenAttestationRepository ethereumCitizenAttestationRepository = CitizenAttestationRepository.
                    ConstructCitizenAttestationRepositoryWithExistingContract(
                    ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, citizenAccount.CitizenAttestationsContractAddress);
            
            if (String.IsNullOrEmpty(citizenAccount.CitizenAttestationsContractAddress))
            {
                citizenAccount.CitizenAttestationsContractAddress = ethereumCitizenAttestationRepository.GetContractAddress();
            }

            await ethereumCitizenAttestationRepository.AddAttestationSignature(
                citizenAccount.PublicAddress, attestationSignatureContract.Id, attestationSignatureContract.IssuerAccountAddress, attestationSignatureContract.Signature);

            return citizenAccount.CitizenAttestationsContractAddress;
        }

        [HttpGet("getdigitalsignature/{citizenAccountAddress}/{attestationId}")]
        public async Task<string> GetDigitalSignature(string citizenAccountAddress, string attestationId)
        {
            CitizenAccount citizenAccount = await citizenAccountRepository.GetCitizenAccountForPublicAddress(citizenAccountAddress);

            ICitizenAttestationRepository ethereumCitizenAttestationRepository = CitizenAttestationRepository.
                ConstructCitizenAttestationRepositoryWithExistingContract(
                ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, citizenAccount.CitizenAttestationsContractAddress);

            return await ethereumCitizenAttestationRepository.GetAttestationSignature(
                citizenAccountAddress, attestationId);
        }

        [HttpPost("createdigitalsignature")]
        public async Task<string> CreateDigitalSignature([FromBody] GenerateDigitalSignatureContract contract)
        {
            CitizenAccount citizenAccount = await citizenAccountRepository.GetCitizenAccountForPublicAddress(contract.CitizenAccountAddress);

            ICitizenAttestationRepository ethereumCitizenAttestationRepository = CitizenAttestationRepository.
                ConstructCitizenAttestationRepositoryWithExistingContract(
                ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, ethereumNetworkConnection.ContractAddress);

            Attestation attestation = new Attestation()
            {
                Id = contract.Id,
                CitizenAccountAddress = contract.CitizenAccountAddress,
                CreatedTime = contract.CreatedTime,
                IssuerAccountAddress = contract.IssuerAccountAddress,
                Payload = contract.Payload,
                SmartContractAddress = ethereumNetworkConnection.ContractAddress
            };

            string message = JsonConvert.SerializeObject(attestation);

            string signature = Crypto.CreateDigitalSignature(message, contract.PrivateKey);

            await ethereumCitizenAttestationRepository.AddAttestationSignature(
                citizenAccount.PublicAddress,
                attestation.Id,
                attestation.IssuerAccountAddress,
                signature);

            return signature;
        }
    }
}