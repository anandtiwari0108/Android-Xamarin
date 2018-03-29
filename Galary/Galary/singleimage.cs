using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Galary
{
    [Activity(Label = "singleimage", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class singleimage : AppCompatActivity
    {
        string path = null;
        string album = null;
        string timestamp = null;
        string name=null;
        string date = null;
        Android.Net.Uri uriExternal = Android.Provider.MediaStore.Images.Media.ExternalContentUri;
        Android.Net.Uri uriInternal = Android.Provider.MediaStore.Images.Media.InternalContentUri;
        List<Dictionary<string, string>> albumList = new List<Dictionary<string, string>>();
        List<dataModel> albumList1 = new List<dataModel>();
        string album_name = "";
        int position;
        Android.Support.V7.App.ActionBar tool;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            tool=SupportActionBar;
            // Create your application here
            SetContentView(Resource.Layout.singleview);
            album_name = Intent.GetStringExtra("name");
            position = Intent.GetIntExtra("position",4);
            ViewPager pager = FindViewById<ViewPager>(Resource.Id.pager);
            pager.OffscreenPageLimit=3;
             string[] projection = { MediaStore.MediaColumns.Data,
                    MediaStore.Images.Media.InterfaceConsts.BucketDisplayName, MediaStore.MediaColumns.Size, MediaStore.Images.Media.InterfaceConsts.DateTaken,MediaStore.MediaColumns.DisplayName };
            ICursor cursorExternal = ContentResolver.Query(uriExternal, projection, "bucket_display_name = \"" + album_name + "\"",
                   null, null);

            ICursor cursorInternal = ContentResolver.Query(uriInternal, projection, "bucket_display_name = \"" + album_name + "\"",
                    null, null);
            ICursor cursor = new MergeCursor(new ICursor[] { cursorExternal, cursorInternal });

            while (cursor.MoveToNext())
            {

                path = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.Data));
                album = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.BucketDisplayName));
                timestamp = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.Size));
                name= cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.DisplayName));
                date = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.DateTaken));
                albumList.Add(Function.mappingInbox(album, path, timestamp, date,name));
            }
            cursor.Close();
            foreach (Dictionary<string, string> s in albumList)
            {
                albumList1.Add(new dataModel(s.GetValueOrDefault(Function.KEY_PATH), 
                    s.GetValueOrDefault(Function.KEY_TIMESTAMP),
                   s.GetValueOrDefault(Function.KEY_COUNT), s.GetValueOrDefault(Function.KEY_TIME)));
            }
            //albumList1.Sort((x, y) => y.date.CompareTo(x.date));
            // albumList1.Sort((x, y) => x.date.CompareTo(y.date));


            //albumList1.OrderBy(x=>x.datecreated);
            //albumList1.GroupBy(x => x.datecreated).ToList();
            albumList1.Sort((x, y) => -1 * x.datecreated.CompareTo(y.datecreated));


            //albumList1.OrderBy(x=>x.datecreated);
            var temp = albumList1.GroupBy(x => x.date).ToList();
            viewpageradapter adapter = new viewpageradapter(this, albumList1, tool);
            //viewpageradapter adapter = new viewpageradapter(this, albumList,tool);
            pager.Adapter = adapter;
            pager.SetCurrentItem(position, false);
            
        }
        

       
    }
}