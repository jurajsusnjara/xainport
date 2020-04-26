pragma solidity >=0.4.0 <0.7.0;

contract Covid19UserData {
    address owner;
    mapping(uint => string) public testChecksums;

    modifier isOwner() {
        require(msg.sender == owner, "Not an owner.");
        _;
    }

    constructor() public {
        owner = msg.sender;
    }

    function addDataChecksum(uint testId, string memory checksum) public isOwner() {
        testChecksums[testId] = checksum;
    }

    function read(uint testId) public view returns(string memory) {
        return testChecksums[testId];
    }
}