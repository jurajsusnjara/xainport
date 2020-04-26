using System.Threading.Tasks;
using Xainport.Models;

namespace Xainport.Documents
{
    public interface ICitizenAccountRepository
    {
        public Task<string> GetPublicAddressForUser(string name);

        public Task<string> GetCitizenAttestationsContractAddressForPublicAddress(string publicAddress);

        public Task<CitizenAccount> GetCitizenAccountForPublicAddress(string publicAddress);

        public CitizenAccount AddAccountInformation(CitizenAccount accountInformation);

        public Task UpdateAccountInformation(CitizenAccount accountInformation);
    }
}
