# Xainport Services
Xainport Services are the set of services that provide APIs to fetch, publish and create digital signatures on Ethereum blockchain.
Xainport Services can be used to create accounts that can issue digital certificates for various attestations. Xainport keeps track of public identites of those issuers and keeps references to blockchain smart contracts where their digital signatures are stored.

# Current implemenation
Current implementation contains of: 
- [Smart contract](Xainport.Ethereum/Contracts/CitizenAttestations.sol) that was deploy on [Rinkeby](https://rinkeby.etherscan.io/) Ethereum test network.
   - Smart contract address: [0xa1da03d100be75c4bb0df927915cf43c77a77cfd](https://rinkeby.etherscan.io/address/0xa1da03d100be75c4bb0df927915cf43c77a77cfd)
   - Account that owns smart contract: [0x1CCd0b7Ee8e3462F0A7e6951Ee520B35E17c1c47](https://rinkeby.etherscan.io/address/0x1CCd0b7Ee8e3462F0A7e6951Ee520B35E17c1c47) (Account transaction history can be seen on previous link)
- Web API that can be used to interact with smart contract. API is hosted at https://xainportapi.azurewebsites.net
   - GET request: /api/attestations/getaccountbalance - get balance of the account that owns smart contract
   - GET request: /api/attestations/getdigitalsignatures/{accountaddress} - get all signatures of attestations of {account address}
   - POST request: /api/attestations/publishdigitalsignature - publish new digital signature to contract and returns contract address
      - Example of POST request data:
  ```
      {
        "id": "1234",
        "citizenPublicAddress": "0x4ea01d9793cab9c9Cc0F14acDc317F157Df1617d",
        "issuerAccountAddress": "0x6Bd701A0D24b7c83cCe83989f6c8021e84bb60Ca",
        "signature": "0xb7ce8483cCe83e84989f6c8021e84bb60Ca89f6c8021e8e84bb60Ca89f6c8021exb7ce8483cCc8021"
      }
  ```
All blockchain transactions made through this API are executed with account that owns smart contract. Every POST transaction costs small amount of Ether so balance of this account will drop over time, check it at https://xainportapi.azurewebsites.net/api/attestations/getaccountbalance or browse Rinkeby for more info.
This is test network so no real money is spent.
   

# Subjects in Xainport system:
- **Attestation** - private document that needs to be signed by legitimate authority
- **Issuing authority** - account that provides digital signature for citizen's attestations
- **Citizen** - account for which attestations and attestations' signatures are issued
- **3rd party** - account which consumes citizens' attestations and verify its signatures

# Technologies used
Xainport Services technical implementation focuses around .NET Core framework and Azure resources which provide simple, fast, reliable and secure way of developing applications.

- **.NET Core** framework for building web application and REST API
- **Azure Cosmos** DB for storing data necessary for services to operate
- **Azure key vault** for storing private Xainport data
- **Azure Active Directory** for authentication and authorization of users and actions in Xainport system
- **Truffle** framework which offers development tools for Ethereum blockchain
- **Nethereum** .NET integration library for blockchain

# Solution infrastructure

Image below represents key components in Xainport system along with their relations.

![Xainport infastructure](/img/xainport_infrastructure.PNG)

# Data stored in Xainport system

- Public blockchain addresses of Issuing authorities
- Other public information about Issuing authorities such as name, country etc.
- Public blockchain addresses of Citizens
- Other public information about Citizens
- Addresses of smart contracts where digital signatures are stored
- Xainport's public and private keys used to manage smart contracts
