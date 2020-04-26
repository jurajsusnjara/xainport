# XainportServices
Xainport Services are the set of services that provides APIs to fetch, publish and create digital signatures on Ethereum blockchain.
Xainport Services can be used to create accounts that can issue digital certificates for various attestations. Xainport keeps track of public identites of those issuers and keeps references to blockchain smart contracts where their digital signatures are stored.

Subjects in Xainport system:
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
