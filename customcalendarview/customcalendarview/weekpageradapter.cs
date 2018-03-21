using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace customcalendarview
{
    class weekpageradapter: FragmentPagerAdapter, ViewPager.IOnPageChangeListener
    {
        List<Calendar> cal;
        List<weekfrag> frag;
        static ViewPager pager;

        int pagecount;
        int current;

        public weekpageradapter(Android.Support.V4.App.FragmentManager fm, List<weekfrag> frag, List<Calendar> cal, ViewPager pager) : base(fm)
        {
            this.cal = cal;
            this.frag = frag;
            weekpageradapter.pager = pager;
            pagecount = frag.Count;
            weekpageradapter.pager.AddOnPageChangeListener(this);

        }



        public override int Count => frag.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {



            return frag[position];







        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(Android.Text.Format.DateFormat.Format("MMMM yyyy", cal[position]));
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {

        }

        public void OnPageScrollStateChanged(int state)
        {

            if (state == ViewPager.ScrollStateIdle)
            {
                if (current == 0)
                {
                    frag[3].prev_week();
                    frag[1].prev_week();
                    frag[0].prev_week();

                    frag[2].prev_week();
                    frag[2].prev_week();
                    frag[4].prev_week();

                    NotifyDataSetChanged();
                    pager.SetCurrentItem(2, false);
                }
                if (current == 1)
                {
                    NotifyDataSetChanged();
                    // move some stuff from the 
                    // center to the right here

                    // move stuff from the left to the center 

                    // retrieve new stuff and insert it to the left page

                }
                if (current == 2)
                {
                    NotifyDataSetChanged();
                    // move stuff from the center to the left page

                    // move stuff from the right to the center page

                    // retrieve stuff and insert it to the right page

                }
                if (current == 4)
                {
                    frag[3].next_week();
                    frag[1].next_week();
                    frag[0].next_week();

                    frag[2].next_week();
                    frag[2].next_week();
                    frag[4].next_week();

                    NotifyDataSetChanged();
                    pager.SetCurrentItem(2, false);
                }

                // go back to the center allowing to scroll indefinitely

            }
        }
        public void OnPageSelected(int position)
        {
            current = position;
        }
        public static void changenext()
        {
            pager.CurrentItem = pager.CurrentItem - 1;
        }
        public static void changeprev()
        {
            pager.CurrentItem = pager.CurrentItem + 1;
        }
    }
}