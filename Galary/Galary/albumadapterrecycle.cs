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
using Com.Bumptech.Glide;
using Java.IO;

namespace Galary
{
    class albumadapterrecycle : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick1;
        private Activity activity;
        private List<Dictionary<string, string>> data;
        public albumadapterrecycle(Activity a, List<Dictionary<string, string>> d)
        {
            activity = a;
            data = d;
        }
        public override int ItemCount => data.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            viewHolderList vh = holder as viewHolderList;
            Dictionary<string, string> imgdata = new Dictionary<string, string>();
            imgdata = data[position];

            Glide.With(activity).Load(new File(imgdata.GetValueOrDefault(Function.KEY_PATH))).Into(vh.img);
            vh.txtCount.Text = imgdata.GetValueOrDefault(Function.KEY_COUNT);
            vh.textTitle.Text = imgdata.GetValueOrDefault(Function.KEY_ALBUM);


           
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(activity).Inflate(
              Resource.Layout.album, null);
            viewHolderList vh = new viewHolderList(itemView,OnClick);
            
            return vh;
        }
        private void OnClick(int obj)
        {

            if (ItemClick1 != null)
                ItemClick1(this, obj);
        }
    }
}