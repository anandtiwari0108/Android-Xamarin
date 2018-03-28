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
using Com.Bumptech.Glide;
using Java.IO;
using Java.Lang;

namespace Galary
{
    class AlbumAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Dictionary<string, string>> data;
        public AlbumAdapter(Activity a, List<Dictionary<string, string>> d)
        {
            activity = a;
            data = d;
        }
        public override int Count => data.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            View view = convertView;
            ImageView galleryImage;
            TextView gallerycount, gallerytitle;
            if (view == null)
            {

                view = LayoutInflater.From(activity).Inflate(
                        Resource.Layout.album, null);
            }
                galleryImage = view.FindViewById<ImageView>(Resource.Id.galaryImage1);
                gallerycount = view.FindViewById<TextView>(Resource.Id.gallery_count1);
               gallerytitle = view.FindViewById<TextView>(Resource.Id.gallery_title1);
                

           
            
            Dictionary<string, string> imgdata = new Dictionary<string, string>();
            imgdata = data[position];
            
               Glide.With(activity).Load(new File(imgdata.GetValueOrDefault(Function.KEY_PATH))).Into(galleryImage);
                gallerycount.Text = imgdata.GetValueOrDefault(Function.KEY_COUNT);
                gallerytitle.Text = imgdata.GetValueOrDefault(Function.KEY_ALBUM);

            
            return view;
        }
    }
}