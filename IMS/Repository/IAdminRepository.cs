using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Repository
{
    public interface IAdminRepository
    {
        Admin GetAdmin(string username, string password);
    }
}