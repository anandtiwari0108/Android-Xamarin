using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Widget;
using Android.Content;
using Java.Util;
using Android.Views;
using Android.App;

using Android.OS;
using Android.Runtime;


using Java.Lang;
using Android.Graphics;

namespace CustomCalenderView
{
    public class CalenderAdapter : BaseAdapter
    {
        int FIRST_DAY_OF_WEEK = 0; // Sunday = 0, Monday = 1
        int prev;
        private Context mContext;
        int current;
        private Calendar month;
        private Calendar selectedDate;
        private List<string> items;
        public System.String[] days;

        public CalenderAdapter(Context c, Calendar monthCalendar)
        {
            
            month = monthCalendar;
            selectedDate = (Calendar)monthCalendar.Clone();
            mContext = c;
            month.Set(CalendarField.DayOfMonth, 1);
            this.items = new List<string>();
            refreshDays();
        }

        public void refreshDays()
        {
            items.Clear();
            
            Calendar prevmonth =  (Calendar)month.Clone();
            prevmonth.Set(CalendarField.Month, prevmonth.Get(CalendarField.Month) - 1);
            int prevday = prevmonth.GetActualMaximum(CalendarField.DayOfMonth);
            int lastDay = month.GetActualMaximum(CalendarField.DayOfMonth);
            int firstDay = month.Get(CalendarField.DayOfWeek);
            prev = firstDay - FIRST_DAY_OF_WEEK - 1;
            // selecting the size of array
            if (firstDay == 1)
            {
                days = new string[lastDay + (FIRST_DAY_OF_WEEK * 6)];
            }
            else
            {
                days = new System.String[lastDay + firstDay - (FIRST_DAY_OF_WEEK + 1)];
            }
            int j = FIRST_DAY_OF_WEEK;








            // enter the empty value for the empty date for day
            if (firstDay > 1)
            {
                var temp = firstDay - FIRST_DAY_OF_WEEK-1;
                for (j = 0; j < firstDay - FIRST_DAY_OF_WEEK; j++)
                {
                    temp --;
                    days[j] = (prevday-temp).ToString();
                }
            }
            else
            {
                for (j = 0; j < FIRST_DAY_OF_WEEK * 6; j++)
                {
                    
                    days[j] = "";
                }
                j = FIRST_DAY_OF_WEEK * 6 + 1; // sunday => 1, monday => 2
            }





            // enter the date from starting 
            int dayNumber = 1;
            for (int i = j - 1; i < days.Length; i++)
            {
                days[i] = "" + dayNumber;
                dayNumber++;
            }
        }
        public void setItems(List<string> items)
        {
            for (int i = 0; i != items.Count; i++)
            {
               
                items[i]= "0" + items[i];
                
            }
            this.items = items;
        }

        public override int Count
        {
            get
            {
                return days.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            TextView dayView;
            if (view == null) // no view to re-use, create new
                view = LayoutInflater.From(mContext).Inflate(Resource.Layout.calender_item, null);

            dayView = (TextView)view.FindViewById(Resource.Id.date);

             
            // disable empty days from the beginning
            if (days[position].Equals(""))
            {
                dayView.Clickable = false;
                dayView.Focusable = false;
            }
            else
            {
                
                
                if(prev>0 && prev>position)
                {
                    view.SetBackgroundResource(Resource.Color.lightcolor);
                }
                else
                {
                    if (month.Get(CalendarField.Year) == selectedDate.Get(CalendarField.Year) && month.Get(CalendarField.Month) == selectedDate.Get(CalendarField.Month) && days[position].Equals("" + selectedDate.Get(CalendarField.DayOfMonth)))
                    {
                        current = position;
                        view.SetBackgroundColor(Android.Graphics.Color.Blue);
                    }
                    else
                    {

                        view.SetBackgroundResource(Resource.Color.holocolor);
                    }
                }
               
               
            }

            dayView.Text = days[position];
            System.String date = days[position];

            
            return view;
        }
        

    }
}