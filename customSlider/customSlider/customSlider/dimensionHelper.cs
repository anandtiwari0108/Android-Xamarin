using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace customSlider
{
    class dimensionHelper
    {
       

       
        public static int DpToPx(Context context, int dp)
        {
            int px = (int)Math.Round(dp * GetPixelScaleFactor(context));
            return px;
        }

        

        private static float GetPixelScaleFactor(Context context)
        {
            DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
            return displayMetrics.Xdpi / (int)DisplayMetricsDensity.Default;
        }
    }
}