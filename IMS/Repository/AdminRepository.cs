using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using IMS.Models;

namespace IMS.Repository
{
    public class AdminRepository : SqlServerBaseRepository, IAdminRepository
    {

        public Models.Admin GetAdmin(string username,string password)
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<Admin>("select * from admin where  UserName=@UserName and Password=@Password and disabled=0", new { UserName=username,Password=password }).FirstOrDefault();
                return result;
            }
        }
    }
}