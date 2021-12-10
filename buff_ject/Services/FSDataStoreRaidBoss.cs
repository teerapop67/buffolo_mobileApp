using buff_ject.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buff_ject.Services
{
    public class FSDataStoreRaidBoss : IDataStoreRaidBoss<RaidBoss>
    {
        FirebaseClient firebase =
    new FirebaseClient("https://realtimedatabasedemo-8e759-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public async Task<bool> AddItemAsyncCollec(RaidBoss boss)
        {
            Console.WriteLine("Created");
            await firebase
                .Child("Boss")
                .PostAsync(boss);
            return true;
        }

  //      var toDeletePerson = (await firebase
  //.Child("CollectionItem") // ลบ item ออกจากฐานข้อมูล ตามไอดีที่กำหนด
  //.OnceAsync<Collection>()).Where(a => a.Object.Username == username).FirstOrDefault();
  //      await firebase.Child("CollectionItem").Child(toDeletePerson.Key).DeleteAsync();
  //          return true;

        public async Task<bool> DeleteItemAsyncCollec(string nameboss)
        {
            var deleteBoss = (await firebase
                .Child("Boss")
                .OnceAsync<RaidBoss>()).Where(a => a.Object.NameBoss == nameboss).FirstOrDefault();
            await firebase.Child("Boss").Child(deleteBoss.Key).DeleteAsync();
            return true;
        
        }

        public async Task<RaidBoss> GetItemAsyncCollec(string nameboss)
        {
            var data = await GetItemsAsyncCollec();
            return data.Where(a => a.NameBoss == nameboss).FirstOrDefault();

        }

        public async Task<IEnumerable<RaidBoss>> GetItemsAsyncCollec(bool forceRefresh = false)
        {
            return (await firebase
                .Child("Boss")
                .OnceAsync<RaidBoss>()).Select(data => new RaidBoss
                {
                    NameBoss = data.Object.NameBoss,
                    HpBoss = data.Object.HpBoss,
                    ImageBoss = data.Object.ImageBoss
                }).ToList();
        }

        public async Task<bool> UpdateItemAsyncCollec(RaidBoss boss)
        {
            var updateBoss = (await firebase
                .Child("Boss")
                .OnceAsync<RaidBoss>())
                .Where(data => data.Object.NameBoss == boss.NameBoss)
                .FirstOrDefault();
            await firebase
                .Child("Boss")
                .Child(updateBoss.Key)
                .PutAsync(boss);
            return true;
        }
    }
}
