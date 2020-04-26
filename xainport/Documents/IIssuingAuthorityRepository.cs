using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xainport.Models;

namespace Xainport.Documents
{
    public interface IIssuingAuthorityRepository
    {
        IssuingAuthority AddIssuingAuthority(IssuingAuthority issuingAuthority);

        Task<IssuingAuthority> GetIssuingAuthority(string publicAddress);
    }
}
