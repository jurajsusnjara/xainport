pragma solidity >=0.4.0 <0.7.0;
pragma experimental ABIEncoderV2;

contract CitizenAttestations {
    address owner;

    struct attestationSignature {
        string attestationId;
        address attestationIssuer;
        string issuerSignature;
    }

    // citizen id -> signature
    mapping(address => attestationSignature[]) public citizenAttestationSignatures;

    modifier isOwner() {
        require(msg.sender == owner, "Not an owner.");
        _;
    }

    constructor() public {
        owner = msg.sender;
    }

    function addAttestationSignature(
        address citizenId,
        string memory attestationId,
        address attestationIssuer,
        string memory signature) public isOwner() {
        citizenAttestationSignatures[citizenId].push(attestationSignature(attestationId, attestationIssuer, signature));
    }

    function getCitizenSignatures(address citizenId) public view returns(attestationSignature[] memory) {
        return citizenAttestationSignatures[citizenId];
    }
}