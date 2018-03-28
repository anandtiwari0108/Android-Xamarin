using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Galary
{
    class mapcomparator :Comparer<Dictionary<string, string>>
    {
        private  string key;
    private string order;
        public mapcomparator(string key,string order)
        {
            this.key = key;
            this.order = order;
        }
        public override int Compare(Dictionary<string, string> x, Dictionary<string, string> y)
        {
            string firstvalue=x.GetValueOrDefault(key);
            string secondvalue=y.GetValueOrDefault(key);
            if (this.order.ToLower().SequenceEqual("asc"))
                {
                return firstvalue.CompareTo(secondvalue);
            }
            else
            {
                return secondvalue.CompareTo(firstvalue);
            }
        }
    }
}