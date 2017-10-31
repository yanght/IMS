using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace IMS.Repository
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\IMSDatabase.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
}