using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Database;
using Java.Util;
using Java.Text;
using Android.Content.Res;
using Android.Util;

namespace Galary
{
    public class Function
    {
        public static string KEY_ALBUM = "album_name";
        public static string KEY_PATH = "path";
        public static string KEY_TIMESTAMP = "timestamp";
        public static string KEY_TIME = "time";
        public static string KEY_COUNT = "date";
        
        public static Dictionary<string, string> mappingInbox(string album, string path, string timestamp, string time, string count)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add(KEY_ALBUM, album);
            map.Add(KEY_PATH, path);
            map.Add(KEY_TIMESTAMP, timestamp);
            map.Add(KEY_TIME, time);
            map.Add(KEY_COUNT, count);
            return map;
        }
        
        public static string getCount(Context c, string album_name)
        {
            Android.Net.Uri uriExternal = Android.Provider.MediaStore.Images.Media.ExternalContentUri;
            Android.Net.Uri uriInternal = Android.Provider.MediaStore.Images.Media.InternalContentUri;

            string[] projection = { MediaStore.MediaColumns.Data,
                MediaStore.Images.Media.InterfaceConsts.BucketDisplayName, MediaStore.MediaColumns.DateModified };
            ICursor cursorExternal = c.ContentResolver.Query(uriExternal, projection, "bucket_display_name = \"" + album_name + "\"", null, null);
            ICursor cursorInternal = c.ContentResolver.Query(uriInternal, projection, "bucket_display_name = \"" + album_name + "\"", null, null);
            ICursor cursor = new MergeCursor(new ICursor[] { cursorExternal, cursorInternal });


            return cursor.Count + " Photos";
        }

        public static string converToTime(string timestamp)
        {
            
            long datetime = Convert.ToInt64(timestamp);
            Date date = new Date(datetime);
            DateFormat formatter = new SimpleDateFormat("dd/MM HH:mm");
            return formatter.Format(date);
        }
        public static float convertDpToPixel(float dp, Context context)
        {
            Resources resources = context.Resources;
            DisplayMetrics metrics = resources.DisplayMetrics;
            float px = dp * (metrics.Density / 160f);
            return px;
        }
    }
}