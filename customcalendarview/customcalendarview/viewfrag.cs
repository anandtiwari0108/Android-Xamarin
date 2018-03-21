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
    public class viewfrag:Android.Support.V4.App.Fragment
    {
        
        Button monthdate, weekdate,currentdate;
        public Calendar month;
        public CalenderAdapter adapter1;
        weekAdapter adapter2;
        Context ctx;
        GridView gridview;
        public static bool monthi=true;
        public viewfrag(Calendar month,Context ctx)
        {
            this.month = month;
            this.ctx = ctx;
            
        }
        
        
        public List<string> items;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
        View view = inflater.Inflate(Resource.Layout.layoutviewpager, container, false);

            gridview = view.FindViewById<GridView>(Resource.Id.gridview);
            monthdate = view.FindViewById<Button>(Resource.Id.monthdate1);
            currentdate = view.FindViewById<Button>(Resource.Id.currentdate);
            weekdate = view.FindViewById<Button>(Resource.Id.weekdate1);
            if(monthi)
            {
                adapter1 = new CalenderAdapter(ctx, month);
                gridview.Adapter = adapter1;
                
            }
            else
            {
                adapter2 = new weekAdapter(ctx, month, Calendar.Instance);
                gridview.Adapter = adapter2;
                
                
            }


            monthdate.Click += delegate
            {
                adapter1 = new CalenderAdapter(ctx, month);
                gridview.Adapter = adapter1;
                monthi = true;
                viewpageradapter.refreshview();
            };
            weekdate.Click += delegate
            {
                adapter2 = new weekAdapter(ctx, month, Calendar.Instance);
                gridview.Adapter = adapter2;
                monthi = false;
                viewpageradapter.refreshview();
            };

            currentdate.Click += delegate
                  {
                      adapter1 = new CalenderAdapter(ctx, month);
                      gridview.Adapter = adapter1;
                      monthi = true;
                      viewpageradapter.resettocurrent();
                  };
           
                
            
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
        public void refreshCalendar()
        {
           
                
                adapter1.refreshDays();
                adapter1.NotifyDataSetChanged();
          
            
                
           
            

           
        }
        public void refreshweek()
        {


            adapter2.refreshDays();
            adapter2.NotifyDataSetChanged();
            
        }
        public Calendar getmonth()
        {
            return this.month;
        }
        public void setmonth(Calendar month)
        {
            if (monthi)
            {
                this.month = month;
            }
            else
            {
                this.month = month;
            }
 }
        public void changeadapter()
        {
            if(monthi)
            {
                adapter1 = new CalenderAdapter(ctx, month);
                gridview.Adapter = adapter1;
            }
            else
            {
                adapter2 = new weekAdapter(ctx, month, Calendar.Instance);
                gridview.Adapter = adapter2;
            }

            







        }
        public void previous_click1()
        {
            if (monthi)
            {
                if (month.Get(CalendarField.Month) == month.GetActualMinimum(CalendarField.Month))
                {
                    month.Set((month.Get(CalendarField.Year) - 1), month.GetActualMaximum(CalendarField.Month), 1);
                }
                else
                {
                    month.Set(CalendarField.Month, month.Get(CalendarField.Month) - 1);
                }
                refreshCalendar();
            }
            else
            {
                month.Add(CalendarField.Date, -7);
                refreshweek();
            }
            
            
        }
        public void next_click1()
        {
            if (monthi)
            {

                if (month.Get(CalendarField.Month) == month.GetActualMaximum(CalendarField.Month))
                {
                    month.Set((month.Get(CalendarField.Year) + 1), month.GetActualMinimum(CalendarField.Month), 1);
                }
                else
                {
                    month.Set(CalendarField.Month, month.Get(CalendarField.Month) + 1);
                }
                refreshCalendar();
            }
            else
            {
                month.Add(CalendarField.Date, 7);
                refreshweek();
            }
           
        }
        
    }
}