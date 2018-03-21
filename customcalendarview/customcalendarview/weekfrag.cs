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
using Java.Util;

namespace customcalendarview
{
    class weekfrag: Android.Support.V4.App.Fragment
    {
        public Calendar month;
        public weekAdapter adapter1;
       
        public weekfrag(Calendar month)
        {
            this.month = month;
        }


        public List<string> items;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layoutweek, container, false);

            GridView gridview = view.FindViewById<GridView>(Resource.Id.gridviewweek);


            adapter1 = new weekAdapter(view.Context, month,Calendar.Instance);

            gridview.Adapter = adapter1;

            /* else
             {
                 weekdate.Enabled = false;
                 gridview.Adapter = adapter2;
             }
             */

            items = new List<string>();

            //monthdate.Click += delegate
            //  {
            //      monthdate.Enabled = false;
            //      weekdate.Enabled = true;
            //      adapter1 = new CalenderAdapter(view.Context, month);
            //      gridview.Adapter = adapter1;
            //      monthi = true;

            //  };
            //weekdate.Click += delegate
            //      {
            //          monthdate.Enabled = true;
            //          weekdate.Enabled = false;
            //          adapter2 = new weekAdapter(view.Context, month, Calendar.Instance);
            //          gridview.Adapter = adapter2;
            //          monthi = false;

            //      };
            gridview.ItemClick += grid_click;
            return view;
        }
        public void grid_click(object sender, AdapterView.ItemClickEventArgs e)
        {

            TextView date = (TextView)e.View.FindViewById(Resource.Id.date);
            System.String day = date.Text.ToString();
            if (day.Equals(""))
            {
                return;
            }

            Toast.MakeText(View.Context, "Date --->" + Android.Text.Format.DateFormat.Format("yyyy-MM", month) + "-" + day, ToastLength.Short).Show();
        }
       
        public void refreshweek()
        {
            adapter1.refreshDays();
            adapter1.NotifyDataSetChanged();
        }
        
        public void next_week()
        {
            month.Add(CalendarField.Date, 7);
            refreshweek();
        }
        public void prev_week()
        {
            month.Add(CalendarField.Date, -7);
            refreshweek();
        }
    }
}