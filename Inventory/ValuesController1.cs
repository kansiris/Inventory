using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Inventory.Service;
using Inventory.Models;
using System.Data.SqlClient;

namespace Inventory
{
    public class ValuesController1 : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string email,string password)
        {
            SqlDataReader value = LoginService.UserSelectRow(email,password);
            if (value.HasRows)
                return "success";
            else
                return "Fail";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}