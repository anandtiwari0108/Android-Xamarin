using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using GoogleGson;

namespace Galary
{
    [Activity(Label = "listitemalbum", Theme = "@style/MyTheme")]
    public class listitemalbum : AppCompatActivity
    {
        string path = null;
        string album = null;
        string timestamp = null;
        string name;
        string date;
        Android.Net.Uri uriExternal = Android.Provider.MediaStore.Images.Media.ExternalContentUri;
        Android.Net.Uri uriInternal = Android.Provider.MediaStore.Images.Media.InternalContentUri;
        List<Dictionary<string, string>> albumList = new List<Dictionary<string, string>>();
        List<dataModel> albumList1 = new List<dataModel>();
        List<dataModel> albumList2 = new List<dataModel>();
        string album_name = "";
        RecyclerView gridView;
        List<dataModel> albumListmain = new List<dataModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
             
            album_name = Intent.GetStringExtra("name");
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title=album_name;
            gridView = FindViewById<RecyclerView>(Resource.Id.grid_view);
            int iDisplayWidth = Resources.DisplayMetrics.WidthPixels;
            Resources resources = ApplicationContext.Resources;
            DisplayMetrics metrics = resources.DisplayMetrics;
            RecyclerView.LayoutManager mLayoutManager = new GridLayoutManager(this,2);
           var manager= new GridLayoutManager(this, 2);
            manager.SetSpanSizeLookup(new changelayout());
            gridView.SetLayoutManager(manager);
            gridView.AddItemDecoration(new itemdecorator(2, itemdecorator.dpToPx(this,10), true));
            gridView.SetItemAnimator(new DefaultItemAnimator());
            
            //gridView.SetItemViewCacheSize(200);
            //RecyclerView.LayoutManager mLayoutManager = new GridLayoutManager(this, 2);
            //gridView.SetLayoutManager(mLayoutManager);
            //gridView.SetItemAnimator(new DefaultItemAnimator());

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
                name = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.DisplayName));
                date = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.DateTaken));
                albumList.Add(Function.mappingInbox(album, path, timestamp,date ,name));
            }
            cursor.Close();
            foreach(Dictionary<string,string> s in albumList)
            {
                albumList1.Add(new dataModel(s.GetValueOrDefault(Function.KEY_PATH), s.GetValueOrDefault(Function.KEY_TIMESTAMP),
                    s.GetValueOrDefault(Function.KEY_COUNT), s.GetValueOrDefault(Function.KEY_TIME)));
            }
            albumList1.Sort((x, y) =>-1* x.datecreated.CompareTo(y.datecreated));

            albumList2 = albumList1.ToList();
            //albumList1.OrderBy(x=>x.datecreated);
            var temp=albumList1.GroupBy(x => x.date).ToList();

            List<dataModel> asd=new List<dataModel>();
            
            
            for (int i=0;i<temp.Count;i++)
            {
               
                albumListmain.Add(new dataModel());
                var tem = albumListmain.Concat(temp[i]).ToList();
                
                albumListmain.Clear();
                albumListmain = tem;

            }
            //datewiseadapter adapter = new datewiseadapter(this, albumList1, temp,album_name);
            listadapter adapter = new listadapter(this, albumList1,temp,gridView, albumListmain);
            //listadapter adapter = new listadapter(this, albumList);
            adapter.ItemClick += itemclicked;
            gridView.SetAdapter(adapter);
           // gridView.SetRecycledViewPool().set;
            


        }

        private void itemclicked(object sender, int e)
        {
           Intent i = new Intent(this, typeof(singleimage));
            var temp = albumListmain[e];
            var temp1 = albumList2.FindIndex(x => x.path.Equals(temp.path));
           i.PutExtra("position",temp1 );
           i.PutExtra("name", album_name);
           StartActivity(i);
       }
       
    }
}