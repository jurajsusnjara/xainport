using Nethereum.Signer;
using Nethereum.Web3;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xainport.Ethereum.Utility
{
    public static class Utility
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

        public static async Task<decimal> GetAccountBalance(string url, string publicKey)
        {
            var web3 = new Nethereum.Web3.Web3(url);
            //var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(publicKey);
            var balance = await web3.Eth.GetBalance.SendRequestAsync(publicKey);
            decimal etherAmount = Web3.Convert.FromWei(balance.Value);

            return etherAmount;
        }
    }
}
