﻿using buff_ject.Models;
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



        public async Task<bool> DeleteItemAsync(string id)
        {
            var toDeletePerson = (await firebase
              .Child("Items") // ลบ item ออกจากฐานข้อมูล ตามไอดีที่กำหนด
              .OnceAsync<Profile>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await firebase.Child("Items").Child(toDeletePerson.Key).DeleteAsync();
            return true;
        }

        public async Task<Profile> GetItemAsync(string username)
        {
            var item = await GetItemsAsync(); //ดึงข้อมูลทั้งหมดมา
            Console.WriteLine("CHECK", item);
            return item.Where(a => a.Username == username).FirstOrDefault(); // ส่งค่ากลับเฉพาะไอดีที่ตรงกัน
        }

        public async Task<IEnumerable<Profile>> GetItemsAsync(bool forceRefresh = false)
        {
            return (await firebase
              .Child("Items") //ดึงข้อมูลทั้งหมดจาก Firebase มา
              .OnceAsync<Profile>()).Select(item => new Profile //สร้างรายการ Object
              {
                  Username = item.Object.Username,
                  
              }).ToList();// คืนค่าแบบกลุ่มออกไป
        }

        public async Task<bool> UpdateItemAsync(Profile item)
        {
            var toUpdatePerson = (await firebase
              .Child("Items")
              .OnceAsync<Profile>())
              .Where(a => a.Object.Id == item.Id)
              .FirstOrDefault();

            await firebase
              .Child("Items")
              .Child(toUpdatePerson.Key)
              .PutAsync(item);

            return true;
        }
    }
}
