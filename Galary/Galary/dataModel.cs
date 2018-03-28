using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace Galary
{
   public class dataModel
    {
        public string path { get; set; }
        public string size { get; set; }
        public string displayname { get; set; }
        public DateTime datecreated { get; set; }
        public string date { get; set; }
        public dataModel()
        {

        }
        public dataModel(string path, string size, string displayname, string dates)
        {
            this.path = path;
            long datetime = Convert.ToInt64(dates);
            DateTime date1 = new DateTime(datetime);
            Date date = new Date(datetime);
            datecreated = date1;
            DateFormat formatter = new SimpleDateFormat("dd/MM/YY");
            this.date = formatter.Format(date);
            this.size = size;
            this.displayname = displayname;
        }
        public dataModel(string dates)
        {

            this.date = dates;
            this.path = "";
        }

    }
}