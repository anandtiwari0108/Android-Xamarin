using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace customSlider
{
    class ImageHelper
    {
        public static Bitmap DrawableToBitmap(Drawable drawable)
        {
            if (drawable is BitmapDrawable)
            {
                return ((BitmapDrawable)drawable).Bitmap;
            }
            
            var width = !drawable.Bounds.IsEmpty
                ? drawable.Bounds.Width()
                : drawable.IntrinsicWidth;

            var height = !drawable.Bounds.IsEmpty
                ? drawable.Bounds.Height()
                : drawable.IntrinsicHeight;

           
            var bitmap = Bitmap.CreateBitmap(width <= 0 ? 1 : width, height <= 0 ? 1 : height,
                Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);

            return bitmap;
        }
    }
}