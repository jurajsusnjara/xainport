using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xainport.Models;
using Xainport.Services;

namespace Xainport.Documents
{
    public class CitizenAccountRepository : ICitizenAccountRepository
    {
        private readonly ICosmosDbService<CitizenAccount> cosmosDbService;

        public CitizenAccountRepository(ICosmosDbService<CitizenAccount> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<string> GetPublicAddressForUser(string name)
        {
            string query = $"select * from c where c.name = '{name}'";

            // TODO should be only one record
            IEnumerable<CitizenAccount> accountInformation = await cosmosDbService.GetItemsAsync(query);
            return accountInformation.First().PublicAddress;
        }

        public async Task<string> GetCitizenAttestationsContractAddressForPublicAddress(string publicAddress)
        {
            string query = $"select * from c where c.publicAddress = '{publicAddress}'";

            // TODO should be only one record
            IEnumerable<CitizenAccount> accountInformation = await cosmosDbService.GetItemsAsync(query);
            return accountInformation.First().CitizenAttestationsContractAddress;
        }

        public async Task<CitizenAccount> GetCitizenAccountForPublicAddress(string publicAddress)
        {
            string query = $"select * from c where c.publicAddress = '{publicAddress}'";

            // TODO should be only one record
            IEnumerable<CitizenAccount> accountInformation = await cosmosDbService.GetItemsAsync(query);

            return accountInformation.Any() ? accountInformation.First() : default;
        }

        public CitizenAccount AddAccountInformation(CitizenAccount accountInformation)
        {
            return cosmosDbService.AddItemAsync(accountInformation);
        }

        public async Task UpdateAccountInformation(CitizenAccount citizenAccount)
        {
            await cosmosDbService.UpdateItemAsync(citizenAccount.Id, citizenAccount);
        }

    }
}
