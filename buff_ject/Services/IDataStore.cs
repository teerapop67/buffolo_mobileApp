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


    public interface IDataStoreCollection<T>
    {
        Task<bool> AddItemAsyncCollec(T itemCollect);
        Task<bool> UpdateItemAsyncCollec(T itemCollect);
        Task<bool> DeleteItemAsyncCollec(string username);
        Task<T> GetItemAsyncCollec(string username);
        Task<IEnumerable<T>> GetItemsAsyncCollec(bool forceRefresh = false);

    }
        
}
