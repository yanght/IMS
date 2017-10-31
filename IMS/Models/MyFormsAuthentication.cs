
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace IMS.Models
{
    public class MyFormsAuthentication
    {
        public static IIdentity identity { get; private set; }
        //Cookie保存是时间
        private const int CookieSaveDays = 14;

        //用户登录成功时设置Cookie
        public static void SetAuthCookie(string username, Admin userData, bool rememberMe)
        {
            if (userData == null)
                throw new ArgumentNullException("userData");

            var data = (new JavaScriptSerializer()).Serialize(new Admin()
            {
                UserName=userData.UserName
            });

            //创建ticket
            var ticket = new FormsAuthenticationTicket(
                2, username, DateTime.Now, DateTime.Now.AddDays(CookieSaveDays), rememberMe, data);
            identity = new FormsIdentity(ticket);
            //加密ticket
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            //创建Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
            };
            if (rememberMe)
                cookie.Expires = DateTime.Now.AddDays(CookieSaveDays);

          
            //写入Cookie
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        //从Request中解析出Cookie
        public static Admin GetAuthCookie()
        {
            // 1. 读登录Cookie
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;
            try
            {
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !string.IsNullOrEmpty(ticket.UserData))
                {
                    var userData = (new JavaScriptSerializer()).Deserialize<Admin>(ticket.UserData);
                    if (userData != null)
                    {
                        return userData;
                    }
                }
                return null;
            }
            catch
            {
                /* 有异常也不要抛出，防止攻击者试探。 */
                return null;
            }
        }
    }
}