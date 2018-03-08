using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Task1
{
    class CustomAdapter : BaseAdapter<dataModel>
    {
        List<dataModel> data;
       
        public CustomAdapter(List<dataModel> data)
        {
            this.data = data;
            
        }
        public override int Count => data.Count;

        public override dataModel this[int position] => data[position];

       

        public override long GetItemId(int position)
        {
            return data.Count;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            /*View view = convertView;
            if(view==null)
            {
                view= LayoutInflater.From(parent.Context).Inflate(Resource.Layout.customListView, parent, false);
                TextView date, time, progress;
                SeekBar seekBarProgress = view.FindViewById<SeekBar>(Resource.Id.seekBarProgress);
                date = view.FindViewById<TextView>(Resource.Id.textViewDate);
                time = view.FindViewById<TextView>(Resource.Id.textViewTime);
                progress = view.FindViewById<TextView>(Resource.Id.textViewProgress);
                view.Tag = new viewHolder { textViewDate = date, textViewDay = time, textViewprogress = progress,seekBarProgress=seekBarProgress };


            }
            var holder = (viewHolder)view.Tag;
            holder.textViewDate =data[position].
                
           

            return view;
            */
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.layout1, parent, false);
                TextView date, time, progress;
                SeekBar seekBarProgress = view.FindViewById<SeekBar>(Resource.Id.seekBarProgress);
                date = view.FindViewById<TextView>(Resource.Id.textViewDateEntry);
                time = view.FindViewById<TextView>(Resource.Id.textViewTimeEntry);
                progress = view.FindViewById<TextView>(Resource.Id.textViewProgressEntry);
                view.Tag = new viewHolder { textViewDate = date, textViewDay = time, textViewprogress = progress, seekBarProgress = seekBarProgress };
                seekBarProgress.Enabled = false;
               


            }
            var holder = (viewHolder)view.Tag;
            holder.textViewDate.Text = data[position].date;
            holder.textViewDay.Text = data[position].Time;
            holder.textViewprogress.Text = data[position].progress+"%";
           
           
            holder.seekBarProgress.Progress=data[position].progress;

            double f = (float)data[position].progress / 20;
            double i = System.Math.Ceiling(f);
            switch(i)
            {
                case 1:
                   
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:

                    break;


            }



            return view;

            
        }
    }
}