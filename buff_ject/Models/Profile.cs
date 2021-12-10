using System;
using Xamarin.Forms;

namespace buff_ject.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int BuffCoin { get; set; }
        public string CharactorURL { get; set; }
        public string NameCharactor { get; set; }
        public int StrUser { get; set; }
        public int VitUser { get; set; }
        public int AgiUser { get; set; }
        public int TotalPower { get; set; }
        public string ProfileURL { get; set; }
        public int drawTime { get; set; }
        public int turnTime { get; set; }
    }
}