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

namespace customcalendarview
{
    public class CalenderAdapter:BaseAdapter
    {
        int FIRST_DAY_OF_WEEK = 0; // Sunday = 0, Monday = 1
        int prev;
        int lastDay;
        private Context mContext;
        int current=0;
        private Calendar month;
       
        private List<string> items;
        public System.String[] days;

        public CalenderAdapter(Context c, Calendar monthCalendar)
        {

            month = monthCalendar;
            
            mContext = c;
            month.Set(CalendarField.DayOfMonth, 1);
            this.items = new List<string>();
            refreshDays();
        }

        public void refreshDays()
        {
            items.Clear();

            Calendar prevmonth = (Calendar)month.Clone();
            prevmonth.Set(CalendarField.Month, prevmonth.Get(CalendarField.Month) - 1);
            int prevday = prevmonth.GetActualMaximum(CalendarField.DayOfMonth);
            lastDay = month.GetActualMaximum(CalendarField.DayOfMonth);
            int firstDay = month.Get(CalendarField.DayOfWeek);
            prev = firstDay - FIRST_DAY_OF_WEEK - 1;
            // selecting the size of array
            if ((prev + lastDay) <= 35)
            {
                //days = new string[lastDay + (FIRST_DAY_OF_WEEK * 6)];
                days = new string[35];

            }
            else
            {
                //days = new System.String[lastDay + firstDay - (FIRST_DAY_OF_WEEK + 1)];
                days = new System.String[42];
            }
            int j = FIRST_DAY_OF_WEEK;








            // enter the empty value for the empty date for day
            if (firstDay > 1)
            {
                var temp = firstDay - FIRST_DAY_OF_WEEK - 1;
                for (j = 0; j < firstDay - FIRST_DAY_OF_WEEK; j++)
                {
                    current++;
                    temp--;
                    days[j] = (prevday - temp).ToString();
                }
            }
            else
            {
                for (j = 0; j < FIRST_DAY_OF_WEEK * 6; j++)
                {
                    current++;
                    days[j] = "";
                }
                j = FIRST_DAY_OF_WEEK * 6 + 1; // sunday => 1, monday => 2
            }





            // enter the date from starting 
            int dayNumber = 1;
            int nextdaynum = 1;
            for (int i = j - 1; i < days.Length; i++)
            {
                if (dayNumber <= lastDay)
                {
                    days[i] = "" + dayNumber;
                    dayNumber++;
                }
                else
                {
                    days[i] = "" + nextdaynum;
                    nextdaynum++;
                }
            }
        }
        public void setItems(List<string> items)
        {
            for (int i = 0; i != items.Count; i++)
            {

                items[i] = "0" + items[i];

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
        
                if (prev > 0 && prev > position)
                {
                view.SetBackgroundResource(Resource.Color.lightcolor);
                view.Click += delegate
                  {
                      viewpageradapter.changeprev();
                  };
                }
                else if(position>(prev+lastDay-1))
                {
                view.SetBackgroundResource(Resource.Color.lightcolor);
                view.Click += delegate
                {
                    viewpageradapter.changenext();
                };
            }
                else
                {
                var datetime = DateTime.Now.Date.ToString("yyyy-MM-dd");

                char[] splitchar = { '-' };
                System.String[] dateArr = datetime.Split(splitchar);

                int temp2 = Integer.ParseInt(dateArr[0]);
                int temp3= Integer.ParseInt(dateArr[1]);
                var temp = month.Get(CalendarField.Year);
                var temp1 = month.Get(CalendarField.Month)+1;

                if (temp==temp2 && temp1==temp3 && days[position].Equals(dateArr[2]))
                    {

                       
                        view.SetBackgroundColor(Android.Graphics.Color.Blue);
                    }
                    else
                    {

                        view.SetBackgroundResource(Resource.Color.holocolor);
                    }
                    dayView.Clickable = false;
                    dayView.Focusable = false;
           
                 }

            dayView.Text = days[position];
            System.String date = days[position];
            

            return view;
        }


    }
}
