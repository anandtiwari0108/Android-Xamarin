using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SQLiteDatabase.Resources.Model
{
    [Table("Person")]
    class Person
    {
        [PrimaryKey,AutoIncrement]
        public int ID { set; get; }
        public string Name { set; get; }
        public Person()
        {

        }
        public Person( string v2)
        {
            
            this.Name = v2;
        }
    }



    

        
    
}