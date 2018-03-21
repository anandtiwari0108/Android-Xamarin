using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteDatabase.Resources.Model;

namespace SQLiteDatabase.Resources.DataHelper
{

    class DataBase
    {
        String folder, path;
        SQLiteConnection con;
       public DataBase()
        {
             folder = System.Environment.GetFolderPath((System.Environment.SpecialFolder.MyDocuments));
            path = Path.Combine(folder, "Person.db");
             con = new SQLiteConnection(path);
        }
        
        public bool CreateDataBase()
        {
            try
            {
               
                
               
               

                using (con)
                {
                    
                    con.CreateTable<Person>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQL EXCEPTION:", ex.Message);
                return false;
            }


        }


        public bool InsertIntoTable(Person person)
        {
            try
            {
                using (con)
                {
                    folder = System.Environment.GetFolderPath((System.Environment.SpecialFolder.MyDocuments));
                    path = Path.Combine(folder, "Person.db");
                    con = new SQLiteConnection(path);
                   
                    
                  con.Insert(person);
                }
                return true;

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQL EXCEPTION:", ex.Message);
                return false;
            }
        }


        public List<Person> Tableuser()
        {
            try
            {
                folder = System.Environment.GetFolderPath((System.Environment.SpecialFolder.MyDocuments));
                path = Path.Combine(folder, "Person.db");
                con = new SQLiteConnection(path);

                using (con)
                {

                    return con.Table<Person>().ToList<Person>();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQL EXCEPTION:", ex.Message);
                return null;
            }


        }


        public bool UpdateTable(Person person)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder)))
                {
                    con.Query<Person>("UPDATE Person set Name=? where ID=? ", person.Name, person.ID);
                    return true;
                }


            }
            catch (SQLiteException ex)
            {
                Log.Info("SQL EXCEPTION:", ex.Message);
                return false;
            }
        }



        public bool DeleteTable(Person person)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder)))
                {
                    con.Delete(person);
                    return true;
                }


            }
            catch (SQLiteException ex)
            {
                Log.Info("SQL EXCEPTION:", ex.Message);
                return false;
            }
        }


        public bool selectTable(int id)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder)))
                {
                    con.Query<Person>("SELECT * from Person where ID=? ", id);
                    return true;
                }


            }
            catch (SQLiteException ex)
            {
                Log.Info("SQL EXCEPTION:", ex.Message);
                return false;
            }
        }
    }
}