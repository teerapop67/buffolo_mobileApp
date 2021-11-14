using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buff_ject.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string username);
        Task<T> GetItemAsync(string username);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
