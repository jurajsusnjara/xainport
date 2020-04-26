using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xainport.Services
{
    public interface ICosmosDbService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);

        Task<T> GetItemAsync(string id);

        T AddItemAsync(T document);

        Task UpdateItemAsync(string id, T document);

        Task DeleteItemAsync(string id);
    }
}
