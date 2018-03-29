using Android.App;
using Android.Widget;
using Android.OS;
//using Android.Support.V7.App;
using System.Collections.Generic;

using Android.Database;
using Android.Net;
using Android.Provider;
using Android.Content.Res;
using Android.Util;
using System;
using Android.Support.V7.App;
using Android.Content;
using Android.Support.V7.Widget;

namespace Galary
{
    [Activity(Label = "Galary", MainLauncher = true,Theme= "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        string path = null;
        string album = null;
        string timestamp = null;
        string countPhoto = null;
        Android.Net.Uri uriExternal = Android.Provider.MediaStore.Images.Media.ExternalContentUri;
        Android.Net.Uri uriInternal = Android.Provider.MediaStore.Images.Media.InternalContentUri;
        List<Dictionary<string, string>> albumList = new List<Dictionary<string, string>>();
        RecyclerView gridView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.mainlayout);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title="Gallery";
            //gridView = FindViewById<ListView>(Resource.Id.list);
            gridView = FindViewById<RecyclerView>(Resource.Id.list1);

            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this);
            
            gridView.AddItemDecoration(new itemdecorator(2, itemdecorator.dpToPx(this, 10), true));


            //gridView.AddItemDecoration(new itemdecorator(2, itemdecorator.dpToPx(this, 10), true));
            gridView.SetItemAnimator(new DefaultItemAnimator());
            gridView.SetLayoutManager(mLayoutManager);

            string[] projection = { MediaStore.MediaColumns.Data,
                    MediaStore.Images.Media.InterfaceConsts.BucketDisplayName, MediaStore.MediaColumns.DateModified };
            ICursor cursorExternal = ContentResolver.Query(uriExternal, projection, "_data IS NOT NULL) GROUP BY (bucket_display_name",
                    null, null);
            
            ICursor cursorInternal = ContentResolver.Query(uriInternal, projection, "_data IS NOT NULL) GROUP BY (bucket_display_name",
                    null, null);
            ICursor cursor = new MergeCursor(new ICursor[] { cursorExternal, cursorInternal });

            while (cursor.MoveToNext())
            {

                path = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.Data));
                album = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.BucketDisplayName));
                timestamp = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.DateModified));
                countPhoto = Function.getCount(ApplicationContext, album);

                albumList.Add(Function.mappingInbox(album, path, timestamp, Function.converToTime(timestamp), countPhoto));
            }
            cursor.Close();
            albumadapterrecycle adapter = new albumadapterrecycle(this, albumList);
            adapter.ItemClick1 += itemclicked; 
                gridView.SetAdapter(adapter);
            //gridView.ItemClick += delegate (object sender,  ItemEventArgs e)
            //{
            //    Intent i = new Intent(this, typeof(listitemalbum));
            //    i.PutExtra("name", albumList[e.Position].GetValueOrDefault(Function.KEY_ALBUM));
            //    StartActivity(i);
            //};


}

        private void itemclicked(object sender, int e)
        {
            
                Intent i = new Intent(this, typeof(listitemalbum));
                i.PutExtra("name", albumList[e].GetValueOrDefault(Function.KEY_ALBUM));
                StartActivity(i);
           
        }
    }
}

