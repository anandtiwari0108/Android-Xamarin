using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using System.Globalization;
using Java.Util;
using System.Collections.Generic;

namespace Task1
{
    [Activity(Label = "Task1", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {

        int progress;
        ListView view;
        TextView textViewProgress;
        Button buttonSave;
        SeekBar seekbarProgress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            System.Collections.Generic.List<dataModel> data = new System.Collections.Generic.List<dataModel>();
            
            //for finding all layout from layout
            findAllView();
            
            
            seekbarProgress.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                textViewProgress.Text = e.Progress + "%";
                progress = e.Progress;
                
            };
            CustomAdapter adapter = new CustomAdapter(data);
            view.Adapter = adapter;
            buttonSave.Click += delegate
            {
                
                var d =DateTime.Now;
                data.Reverse();
                
                var dtPart = d.ToString("MMM dd,yyyy");
                
                var tmPart = d.ToShortTimeString();
                dataModel dataall = new dataModel(dtPart, tmPart, progress);
                data.Add(dataall);
                
                data.Reverse();
                adapter.NotifyDataSetChanged();
                seekbarProgress.Progress = 0;
                
            };
            }

        

        private void findAllView()
        {
            textViewProgress = FindViewById<TextView>(Resource.Id.textViewProgress);
            buttonSave = FindViewById<Button>(Resource.Id.buttonSave);
            seekbarProgress = FindViewById<SeekBar>(Resource.Id.seekBarProgress);
            view = FindViewById<ListView>(Resource.Id.listViewProgress);
        }
       
    }
}

