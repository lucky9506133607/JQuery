using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace InsertDataUsingJqueryAjax
{
    /// <summary>
    /// Summary description for InsertService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InsertService : System.Web.Services.WebService
    {

        [WebMethod]
        public string savedata(string name, string phone, string email, string address)
        {

            SqlConnection con = new SqlConnection("Data Source=TOSHIBA-PC\\SQLEXPRESS;Initial Catalog=SampleDb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("insert into student values ('" + name + "','" + phone + "','" + email + "','" + address + "')", con);
            con.Open();
            con.Close();
            return "Submit";
        }
        [WebMethod]
        public List<> showdata(string id)
        {
        
        }
    }
}
