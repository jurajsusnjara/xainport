using System;
using System.Threading.Tasks;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Xainport.Ethereum.CitizenAttestations;
using Xainport.Ethereum.CitizenAttestations.ContractDefinition;
using Xainport.Ethereum.Documents;

namespace Xainport.Ethereum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo().Wait();
            //InitContract().Wait();
        }

        static async Task InitContract()
        {
            var url = "https://rinkeby.infura.io/v3/e4c1c7027bb24016bc2ddd6ebfbf01fa";
            var privateKey = "1E6BB00C8E929E700F1B4F24FA9C1942C6C333F7B845DD2813B30874F4E5E04C";

            Console.WriteLine("Deploying contract");

            ICitizenAttestationRepository repo = await CitizenAttestationRepository.
                ConstructCitizenAttestationRepositoryWithNewContract(url, privateKey);

            Console.WriteLine(repo.GetContractAddress());
            Console.ReadLine();
        }

        static async Task Demo()
        {
            var url = "http://localhost:7545";
            var privateKey = "7c567d23f12d9e68d4a9783f90b417272c803e8eb4dc5d8387bca3904868734d";
            var citizenPublicAddress = "0x1100B250349B71F2629249bB6414790AE2bEe5F7";
            var account = new Account(privateKey);
            var web3 = new Web3(account, url);

            var deployment = new CitizenAttestationsDeployment();
            var receipt = await CitizenAttestationsService.DeployContractAndWaitForReceiptAsync(web3, deployment);
            var service = new CitizenAttestationsService(web3, receipt.ContractAddress);

            Console.WriteLine($"Contract Deployment Tx Status: {receipt.Status.Value}");
            Console.WriteLine($"Contract Address: {service.ContractHandler.ContractAddress}");
            Console.WriteLine("");

            // signing message
            string message = "COVID19";
            var signer = new MessageSigner();
            var signature = signer.HashAndSign(message, privateKey);

            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Signature: {signature}");
            Console.WriteLine("");

            // Write signed mesage
            string attestationId = Guid.NewGuid().ToString();
            var attestationIssuer = "0x77C5e60b0b7b7d6bD9bb06ED89d77723C06eEcaa";
            //var addReceipt = await service.AddAttestationSignatureRequestAndWaitForReceiptAsync(attestationId, attestationIssuer, signature);

            //Console.WriteLine($"Contract Deployment Tx Status: {addReceipt.Status.Value}");
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
