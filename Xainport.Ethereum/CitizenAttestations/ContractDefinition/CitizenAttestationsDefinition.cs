using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Xainport.Ethereum.CitizenAttestations.ContractDefinition
{


    public partial class CitizenAttestationsDeployment : CitizenAttestationsDeploymentBase
    {
        public CitizenAttestationsDeployment() : base(BYTECODE) { }
        public CitizenAttestationsDeployment(string byteCode) : base(byteCode) { }
    }

    public class CitizenAttestationsDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50600080546001600160a01b031916331790556109c5806100326000396000f3fe608060405234801561001057600080fd5b50600436106100415760003560e01c806339f7be3f1461004657806390b3cd5d146101e4578063cf53e380146103a5575b600080fd5b61016f6004803603604081101561005c57600080fd5b810190602081018135600160201b81111561007657600080fd5b82018360208201111561008857600080fd5b803590602001918460018302840111600160201b831117156100a957600080fd5b91908080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152509295949360208101935035915050600160201b8111156100fb57600080fd5b82018360208201111561010d57600080fd5b803590602001918460018302840111600160201b8311171561012e57600080fd5b91908080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525092955061055f945050505050565b6040805160208082528351818301528351919283929083019185019080838360005b838110156101a9578181015183820152602001610191565b50505050905090810190601f1680156101d65780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6103a3600480360360808110156101fa57600080fd5b810190602081018135600160201b81111561021457600080fd5b82018360208201111561022657600080fd5b803590602001918460018302840111600160201b8311171561024757600080fd5b91908080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152509295949360208101935035915050600160201b81111561029957600080fd5b8201836020820111156102ab57600080fd5b803590602001918460018302840111600160201b831117156102cc57600080fd5b91908080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525092956001600160a01b03853516959094909350604081019250602001359050600160201b81111561032f57600080fd5b82018360208201111561034157600080fd5b803590602001918460018302840111600160201b8311171561036257600080fd5b91908080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152509295506106b4945050505050565b005b6104ce600480360360408110156103bb57600080fd5b810190602081018135600160201b8111156103d557600080fd5b8201836020820111156103e757600080fd5b803590602001918460018302840111600160201b8311171561040857600080fd5b91908080601f0160208091040260200160405190810160405280939291908181526020018383808284376000920191909152509295949360208101935035915050600160201b81111561045a57600080fd5b82018360208201111561046c57600080fd5b803590602001918460018302840111600160201b8311171561048d57600080fd5b91908080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525092955061081d945050505050565b60405180836001600160a01b03166001600160a01b0316815260200180602001828103825283818151815260200191508051906020019080838360005b8381101561052357818101518382015260200161050b565b50505050905090810190601f1680156105505780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b60606002836040518082805190602001908083835b602083106105935780518252601f199092019160209182019101610574565b51815160209384036101000a6000190180199092169116179052920194855250604051938490038101842086519094879450925082918401908083835b602083106105ef5780518252601f1990920191602091820191016105d0565b518151600019602094850361010090810a8201928316921993909316919091179092529490920196875260408051978890038201882060019081018054601f600293821615909802909501909416049485018290048202880182019052838752909450919250508301828280156106a75780601f1061067c576101008083540402835291602001916106a7565b820191906000526020600020905b81548152906001019060200180831161068a57829003601f168201915b5050505050905092915050565b6000546001600160a01b03163314610703576040805162461bcd60e51b815260206004820152600d60248201526c2737ba1030b71037bbb732b91760991b604482015290519081900360640190fd5b60006002856040518082805190602001908083835b602083106107375780518252601f199092019160209182019101610718565b51815160209384036101000a600019018019909216911617905292019485525060408051948590038201852085820182526001600160a01b038916865285830188905290518951919650869450899350918291908401908083835b602083106107b15780518252601f199092019160209182019101610792565b51815160209384036101000a6000190180199092169116179052920194855250604051938490038101909320845181546001600160a01b0319166001600160a01b039091161781558484015180519194610813945060018601935001906108f4565b5050505050505050565b815180830160209081018051600280835293830195830195909520949052825180840182018051958152908201938201939093209390925282546001808501805460408051601f600019958416156101000295909501909216959095049283018690048602810186019094528184526001600160a01b0390921694938301828280156108ea5780601f106108bf576101008083540402835291602001916108ea565b820191906000526020600020905b8154815290600101906020018083116108cd57829003601f168201915b5050505050905082565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061093557805160ff1916838001178555610962565b82800160010185558215610962579182015b82811115610962578251825591602001919060010190610947565b5061096e929150610972565b5090565b61098c91905b8082111561096e5760008155600101610978565b9056fea264697066735822122039f37abb433bc72218449911aecc7015411f4c67a118cdbec388455e070aa22864736f6c63430006040033";
        public CitizenAttestationsDeploymentBase() : base(BYTECODE) { }
        public CitizenAttestationsDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddAttestationSignatureFunction : AddAttestationSignatureFunctionBase { }

    [Function("addAttestationSignature")]
    public class AddAttestationSignatureFunctionBase : FunctionMessage
    {
        [Parameter("string", "citizenId", 1)]
        public virtual string CitizenId { get; set; }
        [Parameter("string", "attestationId", 2)]
        public virtual string AttestationId { get; set; }
        [Parameter("address", "attestationIssuer", 3)]
        public virtual string AttestationIssuer { get; set; }
        [Parameter("string", "signature", 4)]
        public virtual string Signature { get; set; }
    }

    public partial class CitizenAttestationSignaturesFunction : CitizenAttestationSignaturesFunctionBase { }

    [Function("citizenAttestationSignatures", typeof(CitizenAttestationSignaturesOutputDTO))]
    public class CitizenAttestationSignaturesFunctionBase : FunctionMessage
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("string", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class GetAttestationSignatureFunction : GetAttestationSignatureFunctionBase { }

    [Function("getAttestationSignature", "string")]
    public class GetAttestationSignatureFunctionBase : FunctionMessage
    {
        [Parameter("string", "citizenId", 1)]
        public virtual string CitizenId { get; set; }
        [Parameter("string", "attestationId", 2)]
        public virtual string AttestationId { get; set; }
    }



    public partial class CitizenAttestationSignaturesOutputDTO : CitizenAttestationSignaturesOutputDTOBase { }

    [FunctionOutput]
    public class CitizenAttestationSignaturesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "attestationIssuer", 1)]
        public virtual string AttestationIssuer { get; set; }
        [Parameter("string", "issuerSignature", 2)]
        public virtual string IssuerSignature { get; set; }
    }

    public partial class GetAttestationSignatureOutputDTO : GetAttestationSignatureOutputDTOBase { }

    [FunctionOutput]
    public class GetAttestationSignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
