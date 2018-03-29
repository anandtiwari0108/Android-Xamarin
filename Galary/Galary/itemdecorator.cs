using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Galary
{
    
    class itemdecorator:RecyclerView.ItemDecoration
    {
        private int spanCount;
        private int spacing;
        private bool includeEdge;
       public itemdecorator(int spanCount, float spacing, bool includeEdge)
        {
            this.spanCount = spanCount;
            this.spacing = (int)spacing;
            this.includeEdge = includeEdge;
        }
        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            int position = parent.GetChildAdapterPosition(view); // item position
            int column = position % spanCount; // item column

            if (includeEdge)
            {
                outRect.Left = spacing - column * spacing / spanCount; // spacing - column * ((1f / spanCount) * spacing)
                outRect.Right = (column + 1) * spacing / spanCount; // (column + 1) * ((1f / spanCount) * spacing)

                if (position < spanCount)
                { // top edge
                    outRect.Top = spacing;
                }
                outRect.Bottom = spacing; // item bottom
            }
            else
            {
                outRect.Left = column * spacing / spanCount; // column * ((1f / spanCount) * spacing)
                outRect.Right = spacing - (column + 1) * spacing / spanCount; // spacing - (column + 1) * ((1f /    spanCount) * spacing)
                if (position >= spanCount)
                {
                    outRect.Top = spacing; // item top
                }
            }
        }
        static public float dpToPx(Context context,float dp)
        {
            DisplayMetrics metrics =context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, metrics);
        }
    }
    
}