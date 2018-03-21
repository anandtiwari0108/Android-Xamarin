using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLiteDatabase.Resources.Model;
using System.Collections.Generic;

namespace SQLiteDatabase
{
    [Activity(Label = "SQLiteDatabase", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnAdd, btnView;
        EditText AddName;
        TextView data;
        string s = "";
        List<Person> userdata;
        Resources.DataHelper.DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnAdd = FindViewById<Button>(Resource.Id.buttonAdd);
            AddName = FindViewById<EditText>(Resource.Id.name);
            btnView = FindViewById<Button>(Resource.Id.buttonView);
            data = FindViewById<TextView>(Resource.Id.Dataview);
            db = new Resources.DataHelper.DataBase();
            db.CreateDataBase();
            db.InsertIntoTable(new Person("anand"));
            loadData();
            btnAdd.Click += delegate
            {
                db.InsertIntoTable(new Person(AddName.Text));
                loadData();
            };
            btnView.Click += delegate
            {
                loadData();
            };
        }

        private void loadData()
        {
            userdata = db.Tableuser();
            foreach (var user in userdata)
            {
                s += String.Format("{0}){1}\n", user.ID, user.Name);
            }
            data.Text = s;
            s = "";
        }
    }
}

