using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xainport.Models;
using Xainport.Services;

namespace Xainport.Documents
{
    public class IssuingAuthorityRepository : IIssuingAuthorityRepository
    {
        private readonly ICosmosDbService<IssuingAuthority> cosmosDbService;

        public IssuingAuthorityRepository(ICosmosDbService<IssuingAuthority> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public IssuingAuthority AddIssuingAuthority(IssuingAuthority issuingAuthority)
        {
            return cosmosDbService.AddItemAsync(issuingAuthority);
        }

        public async Task<IssuingAuthority> GetIssuingAuthority(string publicAddress)
        {
            string query = $"select * from c where c.publicAddress = '{publicAddress}'";

            // TODO should be only one record
            IEnumerable<IssuingAuthority> issuingAuthority = await cosmosDbService.GetItemsAsync(query);
            return issuingAuthority.First();
        }
    }
}
