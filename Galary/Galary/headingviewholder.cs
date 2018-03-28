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
    class headingviewholder : RecyclerView.ViewHolder
    {
        
        
        public TextView textTitle { get; set; }

        public headingviewholder(View itemview) : base(itemview)
        {
            
            textTitle = itemview.FindViewById<TextView>(Resource.Id.gallery_title_part);
            
        }
    }
}