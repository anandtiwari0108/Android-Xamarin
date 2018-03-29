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
    class listadapter:RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private Activity activity;
        List<IGrouping<string, dataModel>> temp;
        static public int sect=0,moment=0,countview=1;
        public static Boolean mindicate=true;
        public static  List<dataModel> data,data1;
        
        RecyclerView recycler;
        public listadapter(Activity a, List<dataModel> d,List<IGrouping<string,dataModel>> temp, RecyclerView recycler, List<dataModel> data1) 
        {
            
            activity = a;
            data = d;
            this.temp = temp;
            this.recycler = recycler;
            listadapter.data1 = data1;
        }
        /*private List<Dictionary<string, string>> data;
        public listadapter(Activity a, List<Dictionary<string, string>> d)
        {
            activity = a;
            data = d;
        }
        */
       
        public override int ItemCount => data.Count+temp.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder.ItemViewType==1)
            {

                
                
                headingviewholder vh = holder as headingviewholder;
                vh.textTitle.Text = temp[position%temp.Count].Key;

                
            }
            else
            {
                    viewHolder vh = holder as viewHolder;
                    Glide.With(activity).Load(new File(data1[position].path)).Into(vh.img);
                    var size = data1[position].size;
                    //Glide.With(activity).Load(new File(data[position].GetValueOrDefault(Function.KEY_PATH))).Into(vh.img);
                    //var size= data[position].GetValueOrDefault(Function.KEY_TIMESTAMP);
                    int i;
                    if (size.Length == 6)
                    {
                        int.TryParse(size, out i);
                        i = i / 1024;
                        vh.txtCount.Text = i + "kb";
                    }
                    if (size.Length == 7)
                    {
                        int.TryParse(size, out i);
                        i = i / 1024;
                        i = i / 1024;
                        vh.txtCount.Text = i + "mb";
                    }
                    vh.txtCount.Text = data1[position].date;
                    //vh.textTitle.Text= data[position].GetValueOrDefault(Function.KEY_COUNT);
                    vh.textTitle.Text = data1[position].displayname;

               
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
           var manager= recycler.GetLayoutManager();
            
            if (viewType==1)
            {
                View itemView = LayoutInflater.From(activity).Inflate(
               Resource.Layout.heading, null);
                headingviewholder vh = new headingviewholder(itemView);
                mindicate = true;
                return vh;
            }
            else
            {
                View itemView = LayoutInflater.From(activity).Inflate(
               Resource.Layout.GRIDLAYOUTIMG, null);
                viewHolder vh = new viewHolder(itemView, OnClick);
                mindicate = false;

                return vh;
            }
        }
        public override int GetItemViewType(int position)
        {
            if (data1[position].date == null)
                return 1;
            
            else
                return 0;
        }
        private void OnClick(int obj)
        {
          
            if (ItemClick != null)
                ItemClick(this, obj);
        }
        
    }
    
}