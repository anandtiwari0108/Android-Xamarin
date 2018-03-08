using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace Task1
{
    class dataModel
    {
       
        public int progress { get; set; }
        public String date
        {
            get;
            set;
        }
        public String Time
        {
            get; set;
        }
        public dataModel(String date, String Time, int progress)
        {
            this.progress = progress;
            this.date = date;
            this.Time = Time;
        }
    }
}