using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Xainport.Models;
using Xainport.Services;

namespace Xainport.Documents
{
    public class Covid19DataRepository : ICovid19DataRepository
    {
        private readonly ICosmosDbService<Covid19Data> cosmosDbService;

        public Covid19DataRepository(ICosmosDbService<Covid19Data> cosmosDbService) 
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Covid19Data>> GetDataForCitizen(string citizenAccountAddress)
        {
            string query = $"select * from c where c.citizenAccountAddress = '{citizenAccountAddress}'";

            return await cosmosDbService.GetItemsAsync(query);
        }

        public async Task<Covid19Data> GetLatestTestedCovid19DataForCitizen(string citizenAccountAddress)
        {
            IEnumerable<Covid19Data> allData = await this.GetDataForCitizen(citizenAccountAddress);

            // TODO sort by attestationData dictionary and find most recent testedtimestamp
            return allData.Any() ? allData.First() : default;
        }

        public string CreateCovid19Data(
            string citizenAccountAddress,
            string dataIssuerAccountAddress,
            string contractAddress,
            Covid19Data.AttestationData attestationData)
        {
            Covid19Data covid19Data = new Covid19Data
            {
                Id = Guid.NewGuid().ToString(),
                CitizenAccountAddress = citizenAccountAddress,
                ContractAddress = contractAddress,
                Covid19Attestation = attestationData,
                CreatedTime = DateTime.Now,
                IssuerAccountAddress = dataIssuerAccountAddress
            };

            return cosmosDbService.AddItemAsync(covid19Data).Id;
        }
    }
}
