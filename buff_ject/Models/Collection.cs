using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace buff_ject.Models
{
    public class Collection
    {
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public ImageSource ItemImage { get; set; }
        public int Str { get; set; }
        public int Vit { get; set; }
        public int Agi { get; set; }
    }
}
