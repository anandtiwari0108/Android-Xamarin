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

namespace Task1
{
    class viewHolder:Java.Lang.Object
    {
        public TextView textViewDate { get; set; }
        public TextView textViewDay { get; set; }
        public TextView textViewprogress { get; set; }
        public SeekBar seekBarProgress { get; set; }
    }
}