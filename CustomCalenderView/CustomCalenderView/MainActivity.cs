using Android.App;

using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;

namespace CustomCalenderView
{
    [Activity(Label = "CustomCalenderView", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, GestureDetector.IOnGestureListener
    {
        private static int SWIPE_THRESHOLD = 100;
        private static int SWIPE_VELOCITY_THRESHOLD = 100;
        
        GestureDetector detector;
        public Calendar month;  
        public CalenderAdapter adapter;
        public List<string> items;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
            collapsingToolbar.Title = "Calender";
            month = Calendar.Instance;

            
            System.String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
        
            char[] splitchar = { '-' };
            System.String[] dateArr = date.Split(splitchar);



           
            month.Set(Int16.Parse(dateArr[0]), Int16.Parse(dateArr[1])-1, Int16.Parse(dateArr[2]));

            items = new List<string>();
            adapter = new CalenderAdapter(this, month);

            detector = new GestureDetector(this);

            GridView gridview = (GridView)FindViewById(Resource.Id.gridview);
            gridview.Adapter = adapter;
            gridview.ItemClick += grid_click;
           
            TextView title = (TextView)FindViewById(Resource.Id.title);
            title.Text = Android.Text.Format.DateFormat.Format("MMMM yyyy", month);

            TextView previous = (TextView)FindViewById(Resource.Id.previous);
            previous.Click += previous_click;

            TextView next = (TextView)FindViewById(Resource.Id.next);
            next.Click += next_click;

            
        }
        public void previous_click(object sender, EventArgs e)
        {
            previous_click1();
        }

        public void next_click(object sender, EventArgs e)
        {
            next_click1();
        }
        public void refreshCalendar()
        {
            TextView title = (TextView)FindViewById(Resource.Id.title);

            adapter.refreshDays();
            adapter.NotifyDataSetChanged();
           
            title.Text = Android.Text.Format.DateFormat.Format("MMMM yyyy", month);
        }

        public void grid_click(object sender, AdapterView.ItemClickEventArgs e)
        {
            
            TextView date = (TextView)e.View.FindViewById(Resource.Id.date);
            System.String day = date.Text.ToString();
            if(day.Equals(""))
            {
                return;
            }
            
            Toast.MakeText(this, "Date --->" + Android.Text.Format.DateFormat.Format("yyyy-MM", month) + "-" + day, ToastLength.Short).Show();
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {

            float diffY = e2.GetY() - e1.GetY();
            float diffX = e2.GetX() - e1.GetX();

            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
                {
                    if (diffX > 0)
                    {
                        previous_click1();
                     }
                    else
                    {
                        next_click1();
                    }
                }
            }
            return true;

        }

        private void previous_click1()
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

        private void next_click1()
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
        public override bool OnTouchEvent(MotionEvent e)
        {
            detector.OnTouchEvent(e);
            return false;
        }
        public void OnLongPress(MotionEvent e)
        {
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return false;
        }

        public void OnShowPress(MotionEvent e)
        {
            
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }
    }
}

