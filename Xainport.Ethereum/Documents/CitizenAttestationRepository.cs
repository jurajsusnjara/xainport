using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xainport.Ethereum.CitizenAttestations;
using Xainport.Ethereum.CitizenAttestations.ContractDefinition;

namespace Xainport.Ethereum.Documents
{
    public class CitizenAttestationRepository : ICitizenAttestationRepository
    {
        private readonly CitizenAttestationsService citizenAttestationsService;
        private readonly string contractAddress;

        private CitizenAttestationRepository(
            CitizenAttestationsService citizenAttestationsService, 
            string contractAddress)
        {
            this.citizenAttestationsService = citizenAttestationsService;
            this.contractAddress = contractAddress;
        }

        public async Task AddAttestationSignature(
            string citizenAccountAddress,
            string attestationId, 
            string attestationIssuerAccountAddress, 
            string signature)
        {
            var receipt = await citizenAttestationsService.AddAttestationSignatureRequestAndWaitForReceiptAsync(citizenAccountAddress, attestationId, attestationIssuerAccountAddress, signature);
        }

        public async Task<string> GetAttestationSignature(string citizenId, string attestationId)
        {
            return await citizenAttestationsService.GetAttestationSignatureQueryAsync(citizenId, attestationId);
        }

        public string GetContractAddress()
        {
            return contractAddress;
        }

        public static async Task<ICitizenAttestationRepository> ConstructCitizenAttestationRepositoryWithNewContract(
            string url, string contractOwnerPrivateKey)
        {
            Account account = new Account(contractOwnerPrivateKey);
            Web3 web3 = new Web3(account, url);

            var deployment = new CitizenAttestationsDeployment();
            var receipt = await CitizenAttestationsService.DeployContractAndWaitForReceiptAsync(web3, deployment);
            CitizenAttestationsService citizenAttestationsService = new CitizenAttestationsService(web3, receipt.ContractAddress);

            return new CitizenAttestationRepository(citizenAttestationsService, receipt.ContractAddress);
        }

        public static ICitizenAttestationRepository ConstructCitizenAttestationRepositoryWithExistingContract(
            string url, string contractOwnerPrivateKey, string contractAddress)
        {
            Account account = new Account(contractOwnerPrivateKey);
            Web3 web3 = new Web3(account, url);

            CitizenAttestationsService citizenAttestationsService = new CitizenAttestationsService(web3, contractAddress);

            return new CitizenAttestationRepository(citizenAttestationsService, contractAddress);
        }
    }
}
