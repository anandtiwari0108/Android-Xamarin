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
using Java.Lang;
using Java.Util;

namespace customcalendarview
{
    class viewpageradapter : FragmentStatePagerAdapter, ViewPager.IOnPageChangeListener
    {
        List<Calendar> cal;
        static List<viewfrag> frag;
        static ViewPager pager;
        Android.Support.V4.App.FragmentManager fm;
        int pagecount;
        int current;

        public viewpageradapter(Android.Support.V4.App.FragmentManager fm, List<viewfrag> frag, List<Calendar> cal, ViewPager pager) : base(fm)
        {
            this.cal = cal;
            viewpageradapter.frag = frag;
            viewpageradapter.pager = pager;
            pagecount = frag.Count;
            viewpageradapter.pager.AddOnPageChangeListener(this);
            this.fm = fm;

        }



        public override int Count => frag.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {




            return frag[position];





        }
        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
        }
        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return PagerAdapter.PositionNone;
        }
        
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(Android.Text.Format.DateFormat.Format("MMMM yyyy", frag[position].getmonth()));
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {

        }

        public void OnPageScrollStateChanged(int state)
        {
           
            if (state == ViewPager.ScrollStateIdle)
            {
               if(current==0)
                {
                    frag[3].previous_click1();
                    frag[3].previous_click1();
                    frag[1].previous_click1();
                    frag[1].previous_click1();
                    frag[0].previous_click1();
                    frag[0].previous_click1();
                    frag[2].previous_click1();
                    frag[2].previous_click1();
                    frag[4].previous_click1();
                    frag[4].previous_click1();
                    NotifyDataSetChanged();
                    pager.SetCurrentItem(2, false);
                }
                if (current == 1)
                {
                    
                   
                    // move some stuff from the 
                    // center to the right here

                    // move stuff from the left to the center 

                    // retrieve new stuff and insert it to the left page

                }
                if (current == 2)
                {
                    if(viewfrag.monthi==true)
                    {

                    }
                    else
                    {

                        
                        
                    }
                    // move stuff from the center to the left page

                    // move stuff from the right to the center page

                    // retrieve stuff and insert it to the right page

                }
                if(current==3)
                {
                    
                   

                }
                if(current==4)
                {
                    frag[4].next_click1();
                    frag[4].next_click1();
                    frag[3].next_click1();
                    frag[3].next_click1();
                    frag[2].next_click1();
                    frag[2].next_click1();
                    frag[1].next_click1();
                    frag[0].next_click1();

                    frag[1].next_click1();
                    frag[0].next_click1();


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
            pager.CurrentItem=pager.CurrentItem + 1;
        }
        public static void changeprev()
        {
            pager.CurrentItem = pager.CurrentItem - 1;
        }
        public static void refreshview()
        {
            int i=pager.CurrentItem;
            var temp= frag[i].getmonth();
            Calendar currentcal=Calendar.Instance, prevcal= Calendar.Instance, nextcal= Calendar.Instance, nextcal1= Calendar.Instance, prevcal1= Calendar.Instance;
            currentcal = frag[i].getmonth();

            if (viewfrag.monthi == true)
            {

                if (currentcal.Get(CalendarField.Month) == currentcal.GetActualMaximum(CalendarField.Month))
                {
                    nextcal.Set((currentcal.Get(CalendarField.Year) + 1), currentcal.GetActualMinimum(CalendarField.Month), 1);
                }
                else
                {
                    nextcal.Set(CalendarField.Month, currentcal.Get(CalendarField.Month) + 1);
                }
                if (nextcal.Get(CalendarField.Month) == nextcal.GetActualMaximum(CalendarField.Month))
                {
                    nextcal1.Set((nextcal.Get(CalendarField.Year) + 1), nextcal.GetActualMinimum(CalendarField.Month), 1);
                }
                else
                {
                    nextcal1.Set(CalendarField.Month, nextcal.Get(CalendarField.Month) + 1);
                }
                if (currentcal.Get(CalendarField.Month) == currentcal.GetActualMinimum(CalendarField.Month))
                {
                    prevcal.Set((currentcal.Get(CalendarField.Year) - 1), currentcal.GetActualMaximum(CalendarField.Month), 1);
                }
                else
                {
                    prevcal.Set(CalendarField.Month, currentcal.Get(CalendarField.Month) - 1);
                }



                if (prevcal.Get(CalendarField.Month) == prevcal.GetActualMinimum(CalendarField.Month))
                {
                    prevcal1.Set((prevcal.Get(CalendarField.Year) - 1), prevcal.GetActualMaximum(CalendarField.Month), 1);
                }
                else
                {
                    prevcal1.Set(CalendarField.Month, prevcal.Get(CalendarField.Month) - 1);
                }
                

            }
            else
            {
               
                
                prevcal =(Calendar) currentcal.Clone();
                prevcal1 = (Calendar)currentcal.Clone();
                nextcal1 = (Calendar)currentcal.Clone();
                nextcal = (Calendar)currentcal.Clone();
                prevcal1.Add(CalendarField.Date, -14);
                prevcal.Add(CalendarField.Date, -7);
                nextcal.Add(CalendarField.Date, 7);
                nextcal1.Add(CalendarField.Date, 14);




            }
            frag[0].setmonth(prevcal1);
            frag[1].setmonth(prevcal);
            frag[2].setmonth(currentcal);
            frag[3].setmonth(nextcal);
            frag[4].setmonth(nextcal1);
            pager.SetCurrentItem(2, false);
            
            frag[3].changeadapter();
            
            frag[1].changeadapter();
            
            frag[0].changeadapter();
            frag[2].changeadapter();
            
            frag[4].changeadapter();
            MainActivity.notiada();
            
        }
       public static void resettocurrent()
        {
            Calendar currentcal = Calendar.Instance, prevcal = Calendar.Instance, nextcal = Calendar.Instance, nextcal1 = Calendar.Instance, prevcal1 = Calendar.Instance;
            prevcal.Add(CalendarField.Month, -1);
            prevcal1.Add(CalendarField.Month, -2);
            nextcal.Add(CalendarField.Month, 1);
            nextcal1.Add(CalendarField.Month, 2);
            frag[0].setmonth(prevcal1);
            frag[1].setmonth(prevcal);
            frag[2].setmonth(currentcal);
            frag[3].setmonth(nextcal);
            frag[4].setmonth(nextcal1);
            pager.SetCurrentItem(2, false);
           
            MainActivity.notiada();

        }
    }
}