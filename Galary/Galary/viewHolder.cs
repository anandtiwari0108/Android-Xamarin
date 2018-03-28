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
    public class viewHolder:RecyclerView.ViewHolder
    {
        public ImageView img { get; set; }
        public TextView txtCount { get; set; }
        public TextView textTitle { get; set; }
        
        public viewHolder(View itemview, Action<int> listener):base(itemview)
        {
            img = itemview.FindViewById<ImageView>(Resource.Id.galleryImage);
            txtCount = itemview.FindViewById<TextView>(Resource.Id.gallery_count);
            textTitle = itemview.FindViewById<TextView>(Resource.Id.gallery_title);
            itemview.Click += (sender, e) => listener(base.Position);
        }
    }
}