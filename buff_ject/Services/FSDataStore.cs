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
    public class FSDataStore : IDataStore<Profile>
    {
        FirebaseClient firebase =
            new FirebaseClient("https://realtimedatabasedemo-8e759-default-rtdb.asia-southeast1.firebasedatabase.app/");

        //เพิ่มข้อมูลไปที่ Firebase Realtime DB
        public async Task<bool> AddItemAsync(Profile item)
        {
            await firebase
              .Child("Items") //สร้าง root ที่ชื่อว่า item เก็บ property
              .PostAsync(item);

            return true;
        }



        public async Task<bool> DeleteItemAsync(string username)
        {
            var toDeletePerson = (await firebase
              .Child("Items") // ลบ item ออกจากฐานข้อมูล ตามไอดีที่กำหนด
              .OnceAsync<Profile>()).Where(a => a.Object.Username == username).FirstOrDefault();
            await firebase.Child("Items").Child(toDeletePerson.Key).DeleteAsync();
            return true;
        }

        public async Task<Profile> GetItemAsync(string username)
        {
            var item = await GetItemsAsync(); //ดึงข้อมูลทั้งหมดมา
            return item.Where(a => a.Username == username).FirstOrDefault(); // ส่งค่ากลับเฉพาะไอดีที่ตรงกัน
        }

        public async Task<IEnumerable<Profile>> GetItemsAsync(bool forceRefresh = false)
        {
            return (await firebase
              .Child("Items") //ดึงข้อมูลทั้งหมดจาก Firebase มา
              .OnceAsync<Profile>()).Select(item => new Profile //สร้างรายการ Object
              {
                  Username = item.Object.Username,
                  Password = item.Object.Password,
                  Email = item.Object.Email,
                  BuffCoin = item.Object.BuffCoin,
                  CharactorURL = item.Object.CharactorURL,
                  NameCharactor = item.Object.NameCharactor,
                  StrUser = item.Object.StrUser,
                  AgiUser = item.Object.AgiUser,
                  VitUser = item.Object.VitUser,
                  TotalPower = item.Object.TotalPower

              }).ToList();// คืนค่าแบบกลุ่มออกไป
        }

        public async Task<bool> UpdateItemAsync(Profile item)
        {
            var toUpdateProfile = (await firebase
              .Child("Items")
              .OnceAsync<Profile>())
              .Where(a => a.Object.Username == item.Username)
              .FirstOrDefault();

            await firebase
              .Child("Items")
              .Child(toUpdateProfile.Key)
              .PutAsync(item);

            return true;
        }
    }
}
