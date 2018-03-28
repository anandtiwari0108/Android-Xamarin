using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Java.IO;
using Java.Lang;

namespace Galary
{
    class viewpageradapter : PagerAdapter
    {
        Android.Support.V7.App.ActionBar tool;
        private Activity activity;
        private List<dataModel> data;
        LayoutInflater inflater;
        public viewpageradapter(Activity a, List<dataModel> d, Android.Support.V7.App.ActionBar tool)
        {
            activity = a;
            data = d;
            this.tool = tool;
        }
        public override int Count => data.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
           return view == ((RelativeLayout)@object);
        }
        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            
            ImageView img;
            inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.singleviewphoto, null);
           
            img = view.FindViewById<ImageView>(Resource.Id.imgDisplay);
            Glide.With(activity).Load(new File(data[position].path)).Into(img);
            tool.Title = data[position].displayname;
            //  Glide.With(activity).Load(new File(data[position].GetValueOrDefault(Function.KEY_PATH))).Into(img);
            // tool.Title = data[position].GetValueOrDefault(Function.KEY_COUNT);
            ((ViewPager)container).AddView(view);

            return view;

        }
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            ((ViewPager)container).RemoveView((RelativeLayout)@object);
        }
    }
}