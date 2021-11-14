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
    public class FSDataStoreCollection : IDataStore<Collection>
    {
        FirebaseClient firebase =
            new FirebaseClient("https://realtimedatabasedemo-8e759-default-rtdb.asia-southeast1.firebasedatabase.app/");

        //เพิ่มข้อมูลไปที่ Firebase Realtime DB
        public async Task<bool> AddItemAsync(Collection item)
        {
            await firebase
              .Child("CollectionItem") //สร้าง root ที่ชื่อว่า item เก็บ property
              .PostAsync(item);

            return true;
        }



        public async Task<bool> DeleteItemAsync(string username)
        {
            var toDeletePerson = (await firebase
              .Child("CollectionItem") // ลบ item ออกจากฐานข้อมูล ตามไอดีที่กำหนด
              .OnceAsync<Collection>()).Where(a => a.Object.Username == username).FirstOrDefault();
            await firebase.Child("CollectionItem").Child(toDeletePerson.Key).DeleteAsync();
            return true;
        }

        public async Task<Collection> GetItemAsync(string username)
        {
            var item = await GetItemsAsync(); //ดึงข้อมูลทั้งหมดมา
            return item.Where(a => a.Username == username).FirstOrDefault(); // ส่งค่ากลับเฉพาะไอดีที่ตรงกัน
        }

        public async Task<IEnumerable<Collection>> GetItemsAsync(bool forceRefresh = false)
        {
            return (await firebase
              .Child("Collection") //ดึงข้อมูลทั้งหมดจาก Firebase มา
              .OnceAsync<Collection>()).Select(item => new Collection //สร้างรายการ Object
              {
                  Username = item.Object.Username,
                  ItemName = item.Object.ItemName,
                  ItemImage = item.Object.ItemImage,

              }).ToList();// คืนค่าแบบกลุ่มออกไป
        }

        public async Task<bool> UpdateItemAsync(Collection item)
        {
            var toUpdateProfile = (await firebase
              .Child("CollectionItem")
              .OnceAsync<Collection>())
              .Where(a => a.Object.Username == item.Username)
              .FirstOrDefault();

            await firebase
              .Child("CollectionItem")
              .Child(toUpdateProfile.Key)
              .PutAsync(item);

            return true;
        }
    }
}
