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
    public class FSDataStoreCollection : IDataStoreCollection<Collection>
    {
        FirebaseClient firebase =
            new FirebaseClient("https://realtimedatabasedemo-8e759-default-rtdb.asia-southeast1.firebasedatabase.app/");

        //เพิ่มข้อมูลไปที่ Firebase Realtime DB
        public async Task<bool> AddItemAsyncCollec(Collection itemCollect)
        {
            Console.WriteLine("SADASDASD");
            await firebase
              .Child("CollectionItem") //สร้าง root ที่ชื่อว่า item เก็บ property
              .PostAsync(itemCollect);

            return true;
        }



        public async Task<bool> DeleteItemAsyncCollec(string username)
        {
            var toDeletePerson = (await firebase
              .Child("CollectionItem") // ลบ item ออกจากฐานข้อมูล ตามไอดีที่กำหนด
              .OnceAsync<Collection>()).Where(a => a.Object.Username == username).FirstOrDefault();
            await firebase.Child("CollectionItem").Child(toDeletePerson.Key).DeleteAsync();
            return true;
        }

        public async Task<Collection> GetItemAsyncCollec(string username)
        {
            var item = await GetItemsAsyncCollec(); //ดึงข้อมูลทั้งหมดมา
            return item.Where(a => a.Username == username).FirstOrDefault(); // ส่งค่ากลับเฉพาะไอดีที่ตรงกัน
        }

        public async Task<IEnumerable<Collection>> GetItemsAsyncCollec(bool forceRefresh = false)
        {
            return (await firebase
              .Child("CollectionItem") //ดึงข้อมูลทั้งหมดจาก Firebase มา
              .OnceAsync<Collection>()).Select(item => new Collection //สร้างรายการ Object
              {
                  Username = item.Object.Username,
                  ItemName = item.Object.ItemName,
                  ItemImage = item.Object.ItemImage,
                  ItemPrice = item.Object.ItemPrice,
                  Agi = item.Object.Agi,
                  Str = item.Object.Str,
                  Vit = item.Object.Vit

              }).ToList();// คืนค่าแบบกลุ่มออกไป
        }

        public async Task<bool> UpdateItemAsyncCollec(Collection itemCollect)
        {
            var toUpdateProfile = (await firebase
              .Child("CollectionItem")
              .OnceAsync<Collection>())
              .Where(a => a.Object.Username == itemCollect.Username)
              .FirstOrDefault();

            await firebase
              .Child("CollectionItem")
              .Child(toUpdateProfile.Key)
              .PutAsync(itemCollect);

            return true;
        }
    }
}
