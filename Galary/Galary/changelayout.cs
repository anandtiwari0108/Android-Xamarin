using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Galary
{
    class changelayout : GridLayoutManager.SpanSizeLookup
    {
        public override int GetSpanSize(int position)
        {
            if (listadapter.data1[position].path==null)
            {
               
                    return 2;
               
            }
               
            else
                return 1;
        }
    }
}