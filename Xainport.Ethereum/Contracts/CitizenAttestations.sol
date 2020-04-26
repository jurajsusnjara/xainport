pragma solidity >=0.4.0 <0.7.0;
pragma experimental ABIEncoderV2;

contract CitizenAttestations {
    address owner;
    address citizenAddress;

    struct attestationSignature {
        string attestationId;
        address attestationIssuer;
        string issuerSignature;
    }

    // citizen id -> signature
    mapping(string => attestationSignature[]) public citizenAttestationSignatures;

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
        citizenAttestationSignatures[citizenId].push(attestationSignature(attestationId, attestationIssuer, signature));
    }

    function getCitizenSignatures(string memory citizenId) public view returns(attestationSignature[] memory) {
        return citizenAttestationSignatures[citizenId];
    }
}