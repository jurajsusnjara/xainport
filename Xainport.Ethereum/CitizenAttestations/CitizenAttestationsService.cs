using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Xainport.Ethereum.CitizenAttestations.ContractDefinition;

namespace Xainport.Ethereum.CitizenAttestations
{
    public partial class CitizenAttestationsService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CitizenAttestationsDeployment citizenAttestationsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CitizenAttestationsDeployment>().SendRequestAndWaitForReceiptAsync(citizenAttestationsDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CitizenAttestationsDeployment citizenAttestationsDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CitizenAttestationsDeployment>().SendRequestAsync(citizenAttestationsDeployment);
        }

        public static async Task<CitizenAttestationsService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CitizenAttestationsDeployment citizenAttestationsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, citizenAttestationsDeployment, cancellationTokenSource);
            return new CitizenAttestationsService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CitizenAttestationsService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddAttestationSignatureRequestAsync(AddAttestationSignatureFunction addAttestationSignatureFunction)
        {
             return ContractHandler.SendRequestAsync(addAttestationSignatureFunction);
        }

        public Task<TransactionReceipt> AddAttestationSignatureRequestAndWaitForReceiptAsync(AddAttestationSignatureFunction addAttestationSignatureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addAttestationSignatureFunction, cancellationToken);
        }

        public Task<string> AddAttestationSignatureRequestAsync(string citizenId, string attestationId, string attestationIssuer, string signature)
        {
            var addAttestationSignatureFunction = new AddAttestationSignatureFunction();
                addAttestationSignatureFunction.CitizenId = citizenId;
                addAttestationSignatureFunction.AttestationId = attestationId;
                addAttestationSignatureFunction.AttestationIssuer = attestationIssuer;
                addAttestationSignatureFunction.Signature = signature;
            
             return ContractHandler.SendRequestAsync(addAttestationSignatureFunction);
        }

        public Task<TransactionReceipt> AddAttestationSignatureRequestAndWaitForReceiptAsync(string citizenId, string attestationId, string attestationIssuer, string signature, CancellationTokenSource cancellationToken = null)
        {
            var addAttestationSignatureFunction = new AddAttestationSignatureFunction();
                addAttestationSignatureFunction.CitizenId = citizenId;
                addAttestationSignatureFunction.AttestationId = attestationId;
                addAttestationSignatureFunction.AttestationIssuer = attestationIssuer;
                addAttestationSignatureFunction.Signature = signature;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addAttestationSignatureFunction, cancellationToken);
        }

        public Task<CitizenAttestationSignaturesOutputDTO> CitizenAttestationSignaturesQueryAsync(CitizenAttestationSignaturesFunction citizenAttestationSignaturesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CitizenAttestationSignaturesFunction, CitizenAttestationSignaturesOutputDTO>(citizenAttestationSignaturesFunction, blockParameter);
        }

        public Task<CitizenAttestationSignaturesOutputDTO> CitizenAttestationSignaturesQueryAsync(string returnValue1, string returnValue2, BlockParameter blockParameter = null)
        {
            var citizenAttestationSignaturesFunction = new CitizenAttestationSignaturesFunction();
                citizenAttestationSignaturesFunction.ReturnValue1 = returnValue1;
                citizenAttestationSignaturesFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CitizenAttestationSignaturesFunction, CitizenAttestationSignaturesOutputDTO>(citizenAttestationSignaturesFunction, blockParameter);
        }

        public Task<string> GetAttestationSignatureQueryAsync(GetAttestationSignatureFunction getAttestationSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAttestationSignatureFunction, string>(getAttestationSignatureFunction, blockParameter);
        }

        
        public Task<string> GetAttestationSignatureQueryAsync(string citizenId, string attestationId, BlockParameter blockParameter = null)
        {
            var getAttestationSignatureFunction = new GetAttestationSignatureFunction();
                getAttestationSignatureFunction.CitizenId = citizenId;
                getAttestationSignatureFunction.AttestationId = attestationId;
            
            return ContractHandler.QueryAsync<GetAttestationSignatureFunction, string>(getAttestationSignatureFunction, blockParameter);
        }
    }
}
