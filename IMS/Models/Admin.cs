using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Admin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateTime { get; set; }
        public string CreateBy { get; set; }
        public string ModifyTime { get; set; }
        public string ModifyBy { get; set; }
        public int Disabled { get; set; }
    }
}