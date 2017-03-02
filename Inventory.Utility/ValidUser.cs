using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Inventory.Utility
{
    public class ValidUser
    {
        public static void SetAuthCookie(string userResponse, string userId)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                userId,
                DateTime.Now,
                DateTime.Now.AddMinutes(60),
                false, //pass here true, if  want to implement remember me functionality
                userResponse);     // the path for the cookie
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
