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
    class viewHolderList:RecyclerView.ViewHolder
    {
        public ImageView img { get; set; }
        public TextView txtCount { get; set; }
        public TextView textTitle { get; set; }

        public viewHolderList(View itemview,Action<int> listener1) : base(itemview)
        {
            img = itemview.FindViewById<ImageView>(Resource.Id.galaryImage2);
            txtCount = itemview.FindViewById<TextView>(Resource.Id.gallery_count2);
            textTitle = itemview.FindViewById<TextView>(Resource.Id.gallery_title2);
            itemview.Click += (sender, e) => listener1(base.Position);
        }
    }
}