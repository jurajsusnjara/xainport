pragma solidity >=0.4.0 <0.7.0;

contract CitizenAttestations {
    address owner;
    address citizenAddress;

    struct attestationSignature {
        address attestationIssuer;
        string issuerSignature;
    }

    // citizen id -> attestation id -> signature
    mapping(string => mapping(string => attestationSignature)) public citizenAttestationSignatures;

    modifier isOwner() {
        require(msg.sender == owner, "Not an owner.");
        _;
    }

    constructor() public {
        owner = msg.sender;
    }

    function addAttestationSignature(
        string memory citizenId,
        string memory attestationId,
        address attestationIssuer,
        string memory signature) public isOwner() {
        mapping(string => attestationSignature) storage attestationSignatures = citizenAttestationSignatures[citizenId];
        attestationSignatures[attestationId] = attestationSignature(attestationIssuer, signature);
    }

    function getAttestationSignature(string memory citizenId, string memory attestationId) public view returns(string memory) {
        return citizenAttestationSignatures[citizenId][attestationId].issuerSignature;
    }
}