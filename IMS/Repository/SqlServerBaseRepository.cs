using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace IMS.Repository
{
    public class SqlServerBaseRepository
    {
        public static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\IMSDatabase.sqlite"; }
        }

        public static SqlConnection SimpleDbConnection()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}