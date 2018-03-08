using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Graphics;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;

namespace task3
{
   
    [Activity(Label = "task3", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        
        
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           




            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
           
            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBarProgress);
          
            TextView textViewProgress = FindViewById<TextView>(Resource.Id.textView3);
            RatingBar ratingBar = FindViewById<RatingBar>(Resource.Id.ratingBar1);
            
            seekBar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                float f =(float) e.Progress/20;
                f=(float)Math.Round(f,1);
                ratingBar.Rating = f;
                textViewProgress.Text = "Rating:"+f;



            };
            
        }
        
    }
}

