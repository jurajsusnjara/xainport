using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xainport.Contracts;
using Xainport.Documents;
using Xainport.Ethereum.Documents;
using Xainport.Models;
using Xainport.Services;

namespace Xainport.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class Covid19DataController : ControllerBase
    {
        private readonly ICovid19DataRepository covid19DataRepository;
        private readonly ICitizenAccountRepository accountInformationRepository;
        private readonly EthereumNetworkConnectionOptions ethereumNetworkConnection;

        public Covid19DataController(
            ICovid19DataRepository covid19DataRepository,
            ICitizenAccountRepository accountInformationRepository,
            IOptions<EthereumNetworkConnectionOptions> ethereumNetworkConnectionOptions)
        {
            this.covid19DataRepository = covid19DataRepository;
            this.accountInformationRepository = accountInformationRepository;
            this.ethereumNetworkConnection = ethereumNetworkConnectionOptions.Value;
        }

        // citizen reading own covid19 attestations
        [HttpGet("getpersonaldata/{citizenAccountAddress}")]
        public async Task<IEnumerable<Covid19Data>> GetPersonalCovid19Data(string citizenAccountAddress) 
        {
            // authorize the caller
            return await covid19DataRepository.GetDataForCitizen(citizenAccountAddress);
        }

        // 3rd party reading latest covid19 attestation
        [HttpGet("getlatestdata/{citizenAccountAddress}")]
        public async Task<Covid19Data> GetLatestCovid19Attestation(string citizenAccountAddress)
        {
            // authorize the caller
            // ask owner of the data to confirm access
            return await covid19DataRepository.GetLatestTestedCovid19DataForCitizen(citizenAccountAddress);
        }

        // attestation issuer provisioning new covid19 attestation
        //[HttpPost("provision")]
        //public async Task<string> ProvisionCovid19Attestation([FromBody] ProvisionCovid19AttestationContract contract)
        //{
        //    // authorize the caller
        //    // verify digital signature

        //    CitizenAccount accountInformation = await accountInformationRepository.GetAccountInformationForUsername(contract.CitizenUsername);
        //    if (accountInformation == null)
        //    {
        //        return "Account doesn't exist";
        //    }
            
        //    string citizenAccountAddress = accountInformation.PublicAddress;
        //    // get this from logged in user info
        //    string dataIssuerAccountAddress = "xxxdataIssuerAccountAddress";

        //    ICitizenAttestationRepository ethereumCitizenAttestationRepository;
        //    if (String.IsNullOrEmpty(accountInformation.CitizenAttestationsContractAddress))
        //    {
        //        ethereumCitizenAttestationRepository = await CitizenAttestationRepository.
        //            ConstructCitizenAttestationRepositoryWithNewContract(
        //            ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, citizenAccountAddress);
        //        accountInformation.CitizenAttestationsContractAddress = ethereumCitizenAttestationRepository.GetContractAddress();
        //        await accountInformationRepository.UpdateAccountInformation(accountInformation);
        //    }
        //    else
        //    {
        //        ethereumCitizenAttestationRepository = CitizenAttestationRepository.
        //            ConstructCitizenAttestationRepositoryWithExistingContract(
        //            ethereumNetworkConnection.ConnectionUrl, ethereumNetworkConnection.AccountPrivateKey, accountInformation.CitizenAttestationsContractAddress);
        //    }

        //    string id = covid19DataRepository.CreateCovid19Data(
        //        citizenAccountAddress,
        //        dataIssuerAccountAddress,
        //        accountInformation.CitizenAttestationsContractAddress,
        //        contract.Attestation);

        //    return id;

        //}
    }
}