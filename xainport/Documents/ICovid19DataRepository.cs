using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Xainport.Models;

namespace Xainport.Documents
{
    public interface ICovid19DataRepository
    {
        public Task<IEnumerable<Covid19Data>> GetDataForCitizen(string citizenAccountAddress);

        public Task<Covid19Data> GetLatestTestedCovid19DataForCitizen(string citizenAccountAddress);

        public string CreateCovid19Data(
            string citizenAccountAddress,
            string dataIssuerAccountAddress,
            string contractAddress,
            Covid19Data.AttestationData attestationData);
    }
}
