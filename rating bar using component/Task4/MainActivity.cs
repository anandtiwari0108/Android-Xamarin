using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using System.Drawing;
using Android.Graphics;

namespace Task4
{
    [Activity(Label = "Task4", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
      
    
        ImageView star1,star2,star3,star4,star5;
        protected override void OnCreate(Bundle savedInstanceState)
        { 
          
            double progress = 0;
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Paint p = new Paint
            {
                Color = Android.Graphics.Color.Blue
        };
            p.SetStyle(Paint.Style.Stroke);
            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBarProgress);
            TextView textViewProgress = FindViewById<TextView>(Resource.Id.textView3);
            star1 = FindViewById<ImageView>(Resource.Id.star1);
            star2 = FindViewById<ImageView>(Resource.Id.star2);
            star3 = FindViewById<ImageView>(Resource.Id.star3);
            star4 = FindViewById<ImageView>(Resource.Id.star4);
            star5 = FindViewById<ImageView>(Resource.Id.star5);
            seekBar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                double f = (float)e.Progress / 20;
                f = Math.Round(f, 1);
                progress = f;
                textViewProgress.Text = "Rating:" + f;
                checkedstar(progress);
            };
        }
        void checkedstar(double progress)
        {
            double d;
            //all set to empty
            if (progress == 0)
            {
                setim(star1, Resource.Drawable.star);
                setim(star2, Resource.Drawable.star);
                setim(star3, Resource.Drawable.star);
                setim(star4, Resource.Drawable.star);
                setim(star5, Resource.Drawable.star);

            }
            //1st star
            else if (progress > 0 && progress < 1)
            {
                d = progress;
                setimage(star1, d);
                setim(star2, Resource.Drawable.star);
            }
            //2nd star
            else if (progress > 1 && progress < 2)
            {
                setim(star1, Resource.Drawable.star10);
                d = progress - 1;
                setimage(star2, d);
                setim(star3, Resource.Drawable.star);

            }
            //3rd star
            else if (progress > 2 && progress < 3)
            {
                setim(star1, Resource.Drawable.star10);
                setim(star2, Resource.Drawable.star10);
                d = progress - 2;
                setim(star4, Resource.Drawable.star);
                setimage(star3, d);

            }
            //4th star
            else if (progress > 3 && progress < 4)
            {
                setim(star1, Resource.Drawable.star10);
                setim(star2, Resource.Drawable.star10);
                setim(star3, Resource.Drawable.star10);
                d = progress - 3;
                setimage(star4, d);
                setim(star5, Resource.Drawable.star);

            }
            //5th star
            else if (progress > 4 && progress < 5)
            {
                setim(star1, Resource.Drawable.star10);
                setim(star2, Resource.Drawable.star10);
                setim(star3, Resource.Drawable.star10);
                setim(star4, Resource.Drawable.star10);
                
                d = progress - 4;

                setimage(star5, d);
            }
            //All star
            else if (progress == 5)
            {
                setim(star1, Resource.Drawable.star10);
                setim(star2, Resource.Drawable.star10);
                setim(star3, Resource.Drawable.star10);
                setim(star4, Resource.Drawable.star10);
                setim(star5, Resource.Drawable.star10);
                
            }
        }
        void setim(ImageView im, int i)
        {
            im.SetBackgroundResource(i);
        }


        // setting an image for float value .1 to .9
        void setimage(ImageView im, double de)
        {
            int i = (int)(de * 10);
            switch (i)
            {
                case 1:
                    im.SetBackgroundResource(Resource.Drawable.star1);
                    break;
                case 2:
                    im.SetBackgroundResource(Resource.Drawable.star2);
                    break;
                case 3:
                    im.SetBackgroundResource(Resource.Drawable.star3);
                    break;
                case 4:
                    im.SetBackgroundResource(Resource.Drawable.star4);
                    break;
                case 5:
                    im.SetBackgroundResource(Resource.Drawable.star5);
                    break;
                case 6:
                    im.SetBackgroundResource(Resource.Drawable.star6);
                    break;
                case 7:
                    im.SetBackgroundResource(Resource.Drawable.star7);
                    break;
                case 8:
                    im.SetBackgroundResource(Resource.Drawable.star8);
                    break;
                case 9:
                    im.SetBackgroundResource(Resource.Drawable.star9);
                    break;
            }

        }
    }
}








































