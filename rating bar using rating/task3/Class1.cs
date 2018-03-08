using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace task3
{
     class Class1 :Drawable
    {
        Paint paint;
        Path path;
        /* Context context;

         Paint paint;
         Path path;
         public Class1(Context context) : base(context) {
             this.context = context;



         }
         public Class1(Context context, IAttributeSet attrs) : base(context,attrs)
         {
             this.context = context;



         }
         public Class1(Context context, IAttributeSet attrs, int defStyle) : base(context,attrs,defStyle)
         {
             this.context = context;



         }
         */
        public override int Opacity =>  1;

        public override void Draw(Canvas canvas)
        {
           
            paint = new Paint();
            paint.SetARGB(255, 0, 0, 255);
            paint.SetStyle(Paint.Style.Stroke);

            paint.StrokeWidth = 4;
            path = new Path();
            path.AddPath(Star());
            canvas.DrawPath(path, paint);
            canvas.DrawColor(Color.Transparent);
        }

        public override void SetAlpha(int alpha)
        {
            throw new NotImplementedException();
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
            throw new NotImplementedException();
        }

        private Path Star()
        {

            Path path = new Path();
            float midX =60/ 2;
            
            float midY = 30 / 2;
            
            path.MoveTo(midX + 190, midY + 300);

            //for draw line
            
            path.LineTo(midX, midY + 210);
            path.LineTo(midX - 190, midY + 300);
            path.LineTo(midX - 160, midY + 90);
            path.LineTo(midX - 300, midY - 70);
            path.LineTo(midX - 100, midY - 110);
            path.LineTo(midX, midY - 300);
            path.LineTo(midX + 100, midY - 110);
            path.LineTo(midX + 300, midY - 70);
            path.LineTo(midX + 160, midY + 90);
            path.LineTo(midX + 190, midY + 300);

            return path;

        }
    }
   
}