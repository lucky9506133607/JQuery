using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace Crud_Operation
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
   // [WebService(Namespace = "http://tempuri.org/")]
   // [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
   // [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod]
        public string savedata(string Fname, string Mname, string Lname, string Gender, string Email, string MaritulStatus, string Hobbies, string TelePhone, string Mobile, string Address, string PinCode, string State, string Nationality)
        {
            SqlConnection con = new SqlConnection("Data Source=TOSHIBA-PC\\SQLEXPRESS;Initial Catalog=SampleDb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into tblUser values ('" + Fname + "','" + Mname + "','" + Lname + "','" + Gender + "','" + Email + "','" + MaritulStatus + "','" + Hobbies + "','" + TelePhone + "','" + Mobile + "','" + Address + "','" + PinCode + "','" + State + "','" + Nationality + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            return "Submit";
        }
    }
}
