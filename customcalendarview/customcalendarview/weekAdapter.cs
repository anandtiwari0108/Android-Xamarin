using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Widget;
using Android.Content;
using Java.Util;
using Android.Views;


namespace customcalendarview
{
    public class weekAdapter:BaseAdapter
    {
        int FIRST_DAY_OF_WEEK = 0; // Sunday = 0, Monday = 1
        int prev, lastDay;

        int prevchar, nextchar;
        bool nexti = true, previ = true;

        private Context mContext;
        int current;
        private Calendar month;
        private Calendar selectedDate;

        public System.String[] days;

        public weekAdapter(Context c, Calendar monthCalendar, Calendar ca)
        {

            month = monthCalendar;
            selectedDate = ca;
            mContext = c;


            refreshDays();
        }

        public void refreshDays()
        {





            int firstDay = month.Get(CalendarField.DayOfWeek);
            int week = month.Get(CalendarField.WeekOfMonth);
            //for the position in week
            int day = month.Get(CalendarField.DayOfWeek);
            prev = firstDay - FIRST_DAY_OF_WEEK - 1;
            // selecting the size of array

            days = new string[7];

            // enter the date from starting 
            int k = day - 1;//week day of selected date for prev
                            //int currentday,currentmonth,currentyear;
            var tempforcheck =(Calendar) month.Clone();
            var tempfornext=(Calendar)month.Clone();
            int l = day;//week day of selected date for after
            //for the prev date
            if (k >= 0)
            {
                for (; k >= 0;)
                {
                    days[k] = "" + tempforcheck.Get(CalendarField.Date);

                    /* currentyear = tempforcheck.Get(CalendarField.Year);
                     currentmonth = tempforcheck.Get(CalendarField.Month);
                     currentday = tempforcheck.Get(CalendarField.Date);
                     currentday--;

                     if (currentday > 0)
                     {
                         tempforcheck.Set(currentyear, currentmonth, currentday);
                     }
                     else
                     {
                         tempforcheck.Set(currentyear, currentmonth - 1, prevday);
                     }
                     k--;
                     */

                    tempforcheck.Add(CalendarField.Date, -1);
                    if (month.Get(CalendarField.Month)!=tempforcheck.Get(CalendarField.Month) && previ)
                    {
                        prevchar = k;
                        previ = false;

                    }
                   
                    k--;
                }

            }
            
            tempfornext.Add(CalendarField.Date, 1);
            //for the next date
            if (l < 7)
            {
                for (; l < 7;)
                {
                    days[l] = "" + tempfornext.Get(CalendarField.Date);
                    
                    if (month.Get(CalendarField.Month)!=tempfornext.Get(CalendarField.Month) && nexti)
                    {
                        nextchar = l;
                        nexti = false;

                    }
                    tempfornext.Add(CalendarField.Date, 1);
                    l++;
                }
            }



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

            if (!(position >= nextchar || position < prevchar))
            {
                dayView.Focusable = true;
                dayView.Clickable = true;
                view.SetBackgroundResource(Resource.Color.lightcolor);
            }

            else
            {
                var temp = month.Get(CalendarField.Year);
                var temp1 = month.Get(CalendarField.Month);

                if (month.Get(CalendarField.Year) == selectedDate.Get(CalendarField.Year) && month.Get(CalendarField.Month) == selectedDate.Get(CalendarField.Month) && days[position].Equals("" + selectedDate.Get(CalendarField.Date)))
                {
                    current = position;
                    view.SetBackgroundColor(Android.Graphics.Color.Blue);
                }
                else
                {

                    view.SetBackgroundResource(Resource.Color.holocolor);
                }
            }




            dayView.Text = days[position];
            System.String date = days[position];


            return view;
        }

    }
}