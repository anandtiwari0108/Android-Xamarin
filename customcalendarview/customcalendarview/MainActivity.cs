using Android.App;
using Android.Widget;
using System.Threading.Tasks;
using Firebase.Iid;
using Android.Util;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Java.Util;
using System.Collections.Generic;
using System;
using Android.Views;
using Java.Lang;

namespace customcalendarview
{
    [Activity(Label = "customcalendarview", MainLauncher = true,Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {

        List<Calendar> cal;
        List<Calendar> weekcal;
        List<viewfrag> frag;
       
       public static PagerTabStrip pagerTabStrip;
        ViewPager viewPager;
        static viewpageradapter adapter;
       Calendar currentcal,prevcal,nextcal,nextcal1,prevcal1,currentweek,prevweek,prevweek1,nextweek,nextweek1;
       
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Task.Run(() => {
                var instanceid = FirebaseInstanceId.Instance;
                
                Log.Debug("FCM application", "{0} {1}", instanceid.Token, instanceid.GetToken(this.GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
            });
            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            pagerTabStrip = FindViewById<PagerTabStrip>(Resource.Id.pagerstrip);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
           
            frag = new List<viewfrag>();
           
            cal = new List<Calendar>();
            weekcal = new List<Calendar>();
            currentcal = Calendar.Instance;
            prevcal = Calendar.Instance;
            nextcal = Calendar.Instance;
            prevcal1= Calendar.Instance;
            nextcal1= Calendar.Instance;
            // title.Add(Android.Text.Format.DateFormat.Format("MMMM yyyy", currentcal));
            currentweek = Calendar.Instance;
            prevweek = Calendar.Instance;
            prevweek1 = Calendar.Instance;
            nextweek = Calendar.Instance;
            nextweek1 = Calendar.Instance;
            prevweek.Add(CalendarField.Date, -7);
            prevweek1.Add(CalendarField.Date, -14);
            nextweek.Add(CalendarField.Date,7);
            nextweek1.Add(CalendarField.Date, 14);
           
            var collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
           
            System.String date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            char[] splitchar = { '-' };
            System.String[] dateArr = date.Split(splitchar);
            currentcal.Set(Int16.Parse(dateArr[0]), Int16.Parse(dateArr[1]) - 1, Int16.Parse(dateArr[2]));
            nextcal = (Calendar)currentcal.Clone();
            nextcal.Add(CalendarField.Month, 1);
            nextcal1 = (Calendar)nextcal.Clone();
            nextcal1.Add(CalendarField.Month, 1);
            prevcal = (Calendar)currentcal.Clone();
            prevcal.Add(CalendarField.Month, -1);
            prevcal1 = (Calendar)prevcal.Clone();
            prevcal1.Add(CalendarField.Month, -1);
            



            

            
            cal.Add(prevcal1);
            cal.Add(prevcal);
            cal.Add(currentcal);
            cal.Add(nextcal);
            cal.Add(nextcal1);
            frag.Add(new viewfrag(prevcal1,this));
            frag.Add(new viewfrag(prevcal, this));
            frag.Add(new viewfrag(currentcal, this));
           
            frag.Add(new viewfrag(nextcal, this));
           
            frag.Add(new viewfrag(nextcal1, this));
            viewPager.OffscreenPageLimit = 2;
            adapter = new viewpageradapter(this.SupportFragmentManager, frag,cal,viewPager);
            viewPager.Adapter = adapter;
            
            viewPager.SetCurrentItem(2,false);

            



        }
        public static void notiada()
        {
            adapter.NotifyDataSetChanged();
        }

      /*  private void updatecalendar()
        {
            
            updateprev();
            cal.Add(month);
            month = Calendar.Instance;
            title.Add(Android.Text.Format.DateFormat.Format("MMMM yyyy", month));
            cal.Add(month);
            updatenext();
            cal.Add(month);
        }

        public void updateprev()
        {
            if (month.Get(CalendarField.Month) == month.GetActualMinimum(CalendarField.Month))
            {
                month.Set((month.Get(CalendarField.Year) - 1), month.GetActualMaximum(CalendarField.Month), 1);
            }
            else
            {
                month.Set(CalendarField.Month, month.Get(CalendarField.Month) - 1);
            }
            title.Add(Android.Text.Format.DateFormat.Format("MMMM yyyy", month));
        }
        public void updatenext()
        {
            if (month.Get(CalendarField.Month) == month.GetActualMinimum(CalendarField.Month))
            {
                month.Set((month.Get(CalendarField.Year) + 1), month.GetActualMaximum(CalendarField.Month), 1);
            }
            else
            {
                month.Set(CalendarField.Month, month.Get(CalendarField.Month) + 1);
            }
            title.Add(Android.Text.Format.DateFormat.Format("MMMM yyyy", month));
        }
        */
       
    }
}

