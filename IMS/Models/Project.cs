using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Project
    {
        public long Id { get; set; }
        /// <summary>
        /// 村庄名称
        /// </summary>
        public string VillageName { get; set; }
        /// <summary>
        /// 签约名次
        /// </summary>
        public int SignSort { get; set; }
        /// <summary>
        /// 户主姓名
        /// </summary>
        public string HouseHolderName { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string HouseNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
        public int Disabled { get; set; }
    }
}