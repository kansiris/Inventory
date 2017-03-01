using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Inventory.Content
{
    public class CustomPrinciple : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        
        public bool IsInRole(string role)
        {
            if (Roles.Any(role.Contains))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrinciple(string username) {
            this.Identity = new GenericIdentity(username);
        }
        public string[] Roles { get; set; }
        public string ID { get; set; }
        public string Emailid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DbName { get; set; } 
        public string UserSite { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public DateTime? Created_Date { get; set; }
        public int SubscriptionId { get; set; }
        public int UserTypeId { get; set; }
        public string User_Site { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public int IsActive { get; set; }
        public string activationcode { get; set; }
        public Byte[] Profile_Picture { get; set; }
        public string Date_Format { get; set; }
        public string Timezone { get; set; }
        public string Currency { get; set; }
    }
}