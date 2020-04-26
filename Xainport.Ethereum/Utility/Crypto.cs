using Nethereum.Signer;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xainport.Ethereum.Utility
{
    public static class Crypto
    {
        public static string CreateDigitalSignature(string message, string privateKey)
        {
            var signer = new MessageSigner();
            return signer.HashAndSign(message, privateKey);
        }

        public static bool VerifyDigitalSignature(string publicKey, string message, string signature)
        { 
            // TODO verify digital signature

            return true;
        }
    }
}
