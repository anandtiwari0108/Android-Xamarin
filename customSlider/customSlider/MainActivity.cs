using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace customSlider
{
    [Activity(Label = "customSlider", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            TextView minvalue, maxvalue;
            CustomSeekBar seekbar;
            minvalue = FindViewById<TextView>(Resource.Id.minvalue);
            maxvalue= FindViewById<TextView>(Resource.Id.maxvalue);
            seekbar = FindViewById<CustomSeekBar>(Resource.Id.slider);
            minvalue.Text = "min:" + seekbar.GetSelectedMinValue();
            maxvalue.Text = "max:" + seekbar.GetSelectedMaxValue();
            seekbar.LowerValueChanged += delegate
              {
                  minvalue.Text = "min:" + seekbar.GetSelectedMinValue();
              };
            seekbar.UpperValueChanged += delegate
              {
                  maxvalue.Text = "min:" + seekbar.GetSelectedMaxValue();
              };
            
        }
    }
}

