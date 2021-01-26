jquery load callback function
In this video we will discuss the use of complete parameter of jquery load function.
Syntax
.load( url [, data ] [, complete ] )

complete parameter is a callback function that is executed when the request completes. The callback function has 3 parameters.

CallbackFunction(response, status, xhr)

In the following example, the anonymous function that is passed as the third parameter to the load function is executed when the AJAX request completes.

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var textBoxes = $('input[type="text"]');
            textBoxes.focus(function () {
                var helpDiv = $(this).attr('id');
                $('#' + helpDiv + 'HelpDiv').load('GetHelpText.aspx', { HelpTextKey: helpDiv },
                      function (response, status, xhr) {
                          var errorMessage = 'status : ' + xhr.status + '<br/>';
                          errorMessage += 'status text : ' + xhr.statusText + '<br/>';
                          errorMessage += 'response : ' + response;
                          $('#divError').html(errorMessage);
                      });
            });

            textBoxes.blur(function () {
                var helpDiv = $(this).attr('id') + 'HelpDiv';
                $('#' + helpDiv).html('');
            });
        });
    </script>
</head>
<body style="font-family:Arial">
    <table>
        <tr>
            <td>First Name</td>
            <td><input id="firstName" type="text" /></td>
            <td><div id="firstNameHelpDiv"></div></td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td><input id="lastName" type="text" /></td>
            <td><div id="lastNameHelpDiv"></div></td>
        </tr>
        <tr>
            <td>Email</td>
            <td><input id="email" type="text" /></td>
            <td><div id="emailHelpDiv"></div></td>
        </tr>
        <tr>
            <td>Income</td>
            <td><input id="income" type="text" /></td>
            <td><div id="incomeHelpDiv"></div></td>
        </tr>
    </table>
    <br />
    <div id="divError"></div>
</body>
</html>

If you want the code in the callback function to execute only if these is an error, you can do so using status parameter as shown below.
$('#' + helpDiv + 'HelpDiv').load('GetHelpText.aspx', { HelpTextKey: helpDiv },
    function (response, status, xhr) {
        if (status == 'error') {
            var errorMessage = 'status : ' + xhr.status + '<br/>';
            errorMessage += 'status text : ' + xhr.statusText + '<br/>';
            errorMessage += 'response : ' + response;
            $('#divError').html(errorMessage);
        }
    });

----------------------------------------------------------------------------------------------

jquery ajax get function
In this video we will discuss
1. How to make a GET request using jQuery AJAX get function
2. Difference between jQuery get and load functions
We discussed how to make AJAX requests using jQuery load() function in Part 53 and Part 54. Let us rewrite the example we worked with in Part 54, using jQuery AJAX get() function.

Please change the following jquery code in HtmlPage1.html

$('#' + helpDiv + 'HelpDiv').load('GetHelpText.aspx', { HelpTextKey: helpDiv });

AS SHOWN BELOW

$.get('GetHelpText.aspx', { HelpTextKey: helpDiv }, function (response) {
    $('#' + helpDiv + 'HelpDiv').html(response);

});

What is the difference between jQuery get and load functions
1. jQuery load function can be used to load only the HTML data from the server, where as jquery get function can be used to load any type of data (xml, json, script, or html).

2. jQuery load function may issue a get or post request depending on whether the data parameter is specified or not. POST method is used if data is provided, otherwise, GET is used. On the other hand jquery get method always issues a GET request.

3. With load function we specify a selector first. The HTML content retrieved from the server is automatically inserted into the DOM elements matched by the selector.
$('selector').load('url', 'data');

With get function we do not specify any selector first, instead we will make use of the callback function to insert the data received from the server into the DOM elements.

$.get('url', 'data', function (response) {
    $('selector').html(response);

});

In our next video, we will discuss how to load json data from the server using jquery get function.

--------------------------------------------------------------------------------------------------------

load json data using jquery ajax

In this video we will discuss how to load JSON data from the server using jQuery get function. This is continuation to Part 56. Please watch Part 56 before proceeding.
Syntax
$.get( url [, data ] [, success ] [, dataType ] )

dataType parameter specifies the type of data expected from the server. The dataType can be xml, json, script, or html. By default jQuery makes an intelligent guess.

The following steps modify the example we worked with in Part 56, so that HtmlPage1.html will be able to retrieve and display JSON data from the server.

Step 1 : Modify GetHelpText.aspx to return JSON data. Remove the following div element from GetHelpText.aspx.
<div id="divResult" runat="server"></div>

Step 2 : Modify the code in GetHelpText.aspx.cs as shown below.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace Demo
{
    public partial class GetHelpText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string JSONString = js.Serialize(GetHelpTextByKey(Request["HelpTextKey"]));

            Response.Write(JSONString);
        }

        private HelpText GetHelpTextByKey(string key)
        {
            HelpText helpText = new HelpText();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetHelpTextByKey", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@HelpTextKey", key);
                cmd.Parameters.Add(parameter);
                con.Open();
                helpText.Text = cmd.ExecuteScalar().ToString();
                helpText.Key = key;
            }

            return helpText;
        }
    }

    public class HelpText
    {
        public string Key { get; set; }
        public string Text { get; set; }
    }
}

Step 3 : Modify the jQuery code in HtmlPage1.html as shown below

$(document).ready(function () {
    var textBoxes = $('input[type="text"]');
    textBoxes.focus(function () {
        var helpDiv = $(this).attr('id');
        $.get('GetHelpText.aspx', { HelpTextKey: helpDiv }, function (response) {
            $('#' + helpDiv + 'HelpDiv').html(response.Text);
        }, 'json');
    });

    textBoxes.blur(function () {
        var helpDiv = $(this).attr('id') + 'HelpDiv';
        $('#' + helpDiv).html('');
    });
});

-----------------------------------------------------------------------------------

jquery ajax get xml data
In this video we will discuss how to load XML data from the server using jQuery get function. This is continuation to Part 57, please watch Part 57 before proceeding.

The following steps modify the example we worked with in Part 57, so that HtmlPage1.html will be able to retrieve and display XML data from the server.

Step 1 : Modify the code in GetHelpText.aspx.cs as shown below.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace Demo
{
    public partial class GetHelpText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HelpText));
            xmlSerializer.Serialize(Response.OutputStream,
                GetHelpTextByKey(Request["HelpTextKey"]));
        }

        private HelpText GetHelpTextByKey(string key)
        {
            HelpText helpText = new HelpText();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetHelpTextByKey", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@HelpTextKey", key);
                cmd.Parameters.Add(parameter);
                con.Open();
                helpText.Text = cmd.ExecuteScalar().ToString();
                helpText.Key = key;
            }

            return helpText;
        }
    }

    public class HelpText
    {
        public string Key { get; set; }
        public string Text { get; set; }
    }
}

Step 2 : Modify the jQuery code in HtmlPage1.html as shown below

$(document).ready(function () {
    var textBoxes = $('input[type="text"]');
    textBoxes.focus(function () {
        var helpDiv = $(this).attr('id');
        $.get('GetHelpText.aspx', { HelpTextKey: helpDiv }, function (response) {
            var jQueryXml = $(response);
            var textElement = jQueryXml.find("Text");

            $('#' + helpDiv + 'HelpDiv').html(textElement.text());
        }, 'xml');
    });

    textBoxes.blur(function () {
        var helpDiv = $(this).attr('id') + 'HelpDiv';
        $('#' + helpDiv).html('');
    });
});

Please Note :
1. Use fiddler to inspect the data that is sent to and received from GetHelpText.aspx
2. Type the following URL in the browser to see the generated XML
http://localhost:PortNumber/GetHelpText.aspx?HelpTextKey=firstName

--------------------------------------------------------------------------------------

jquery make a post request
In this video we will discuss 
1. How to make a post request using jquery. 
2. What is the difference between get and post

This is continuation to Part 58, please watch Part 58 before proceeding.

To make a get request we use jquery get function. jQuery load function can be used to make either a get or post request, depending on whether the data parameter is specified or not. POST method is used if data is provided, otherwise, GET is used.

Another method that is available in jQuery to make a post request is post method.

jQuery post function syntax
$.post( url [, data ] [, success ] [, dataType ] )

Please note that the syntax of post function is similar to get function

To modify the example we worked with in Part 58, to issue a post request simply replace get() function with post function.

CHANGE THE FOLLOWING LINE FROM
$.get('GetHelpText.aspx', { HelpTextKey: helpDiv }, function (response) {

TO
$.post('GetHelpText.aspx', { HelpTextKey: helpDiv }, function (response) {

At this point if you fire fiddler and inspect the request you will find that a POST request is issued.

What is the difference between get and post
1. GET is designed for getting data from the server. POST is designed for modifying data on the server.

2. Both GET and POST can be used to send data to the server. 

3. When you are sending data with a GET request, the data is appended to the URL as query strings. With POST request the data is included in the message body.

4. There is a limit on how much data can be sent using a GET request due to the limit on the length of the data that can be passed as part of the URL. The size limitations associated with GET are different depending on the client and server software. So, If you're sending large amounts of data, use POST over GET. If it's just a small amount of data then you may use GET.

5. In general, a GET request should have no side-effects, in the sense that it shouldn't modify data. Usually if we are using a GET request to send data to the server, it should be to retrieve an item with an id. For example, GetCustomer.aspx?id=1, will retrieve a customer with id=1.

A lot of people keep asking what is the difference between GET and POST in AJAX
Whether you make GET and POST request using AJAX or by some other means is irrelevant, the differences are still the same.

----------------------------------------------------------------------------------------------------------

jquery ajax method
In this video we will discuss how to make ajax requests using jquery ajax function.
The other methods that are available in jquery to make ajax requests are load(), get() and post(). We discussed these methods in detail in the previous sessions of jQuery tutorial video series. All these methods are wrapper methods and use ajax() method under the hood. In Visual Studio, if you right click on any of these methods and select "Go To Definition" from the context menu, you can see that these methods call ajax() method.

The wrapper methods are easier to use but they do not provide much flexibility. If you want to have complete control on configuring the ajax request use ajax() method.

Syntax of jquery ajax method
$.ajax( [ settings ] )

settings is a JavaScript object that we use to configure the Ajax request. For the list of all available options please check the jquery ajax method documentation
http://api.jquery.com/jquery.ajax/

Let us now modify the example we worked with in Part 59, to use ajax() method instead of post() method.

$(document).ready(function () {
    var textBoxes = $('input[type="text"]');
    textBoxes.focus(function () {
        var helpDiv = $(this).attr('id');

        $.ajax({
            url: 'GetHelpText.aspx',
            data: { HelpTextKey: helpDiv },
            success: function (response, status, xhr) {
                var jQueryXml = $(response);
                var textElement = jQueryXml.find("Text");
                $('#' + helpDiv + 'HelpDiv').html(textElement.text());
            },
            dataType: 'xml',
            method: 'post'
        });
    });

    textBoxes.blur(function () {
        var helpDiv = $(this).attr('id') + 'HelpDiv';
        $('#' + helpDiv).html('');
    });

});

In our next video, we will discuss how to call an asp.net web service using jQuery AJAX and consume the XML data returned by the web service.

-----------------------------------------------------------------------------------------------

Calling asp.net web services using jquery ajax

In this video we will discuss how to call asp.net webservice using jQuery AJAX.
We want to retrieve data from the following database table tblEmployee using asp.net web service and jQuery AJAX
The retrieved data should be displayed as shown below
Step 1 : Create SQL Server table and insert employee data

Create table tblEmployee
(
     Id int primary key identity,
     Name nvarchar(50),
     Gender nvarchar(10),
     Salary int
)
GO

Insert into tblEmployee values ('Mark', 'Male', 50000)
Insert into tblEmployee values ('Sara', 'Female', 60000)
Insert into tblEmployee values ('John', 'Male', 45000)
Insert into tblEmployee values ('Pam', 'Female', 45000)
GO

Step 2 : Create a stored procedure to retrieve employee data by ID

Create procedure spGetEmployeeById
@Id int
as
Begin
     Select ID, Name, Gender, Salary
     from tblEmployee
     where ID = @Id
End

Step 3 : Create new asp.net web application project. Name it Demo. 

Step 4 : Include a connection string in the web.config file to your database.
<add name="DBCS"
      connectionString="server=.;database=SampleDB;integrated security=SSPI" />

Step 5 : Add a class file to the project. Name it Employee.cs. Copy and paste the following code.

namespace Demo
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
    }
}

Step 6 : Add a new WebService (ASMX). Name it EmployeeService.asmx. Copy and paste the following code.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {
        [WebMethod]
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = new Employee();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = employeeId;

                cmd.Parameters.Add(parameter);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                }
            }

            return employee;
        }
    }
}

Step 7 : Add an HTML page to the ASP.NET project. Copy and paste the following HTML and jQuery code

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGetEmployee').click(function () {

                var empId = $('#txtId').val();

                $.ajax({
                    url: 'EmployeeService.asmx/GetEmployeeById',
                    data: { employeeId: empId },
                    method: 'post',
                    dataType: 'xml',
                    success: function (data) {
                        var jQueryXml = $(data);
                        $('#txtName').val(jQueryXml.find('Name').text());
                        $('#txtGender').val(jQueryXml.find('Gender').text());
                        $('#txtSalary').val(jQueryXml.find('Salary').text());
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });
        });
    </script>
</head>
<body style="font-family:Arial">
    ID : <input id="txtId" type="text" style="width:100px" />
    <input type="button" id="btnGetEmployee" value="Get Employee" />
    <br /><br />
    <table border="1" style="border-collapse:collapse">
        <tr>
            <td>Name</td>
            <td><input id="txtName" type="text" /></td>
        </tr>
        <tr>
            <td>Gender</td>
            <td><input id="txtGender" type="text" /></td>
        </tr>
        <tr>
            <td>Salary</td>
            <td><input id="txtSalary" type="text" /></td>
        </tr>
    </table>
</body>
</html>

In our next video we will discuss how to call an asp.net web service that returns JSON data using jQuery AJAX.


-------------------------------------------------------------------------------------------------

Handling json data returned from asp.net web services
In this video we will discuss
1. How to make an asp.net web service return JSON data
2. Consume JSON data returned by the web service using jQuery AJAX

This is continuation to Part 61, please watch Part 61 before proceeding.

There are 2 approaches to make an asp.net web service return JSON data and consume it using jQuery AJAX.

Approach 1 : With Approach 1, the web service code does not change in any way. 

We need to add/change the following options of the jquery request object.

1. Add contentType option and set it to application/json; charset=utf-8 to specify that we will be sending a JSON string.

2. Use JSON.stringify() method to convert the data you are sending to the server to a JSON string

3. Change the dataType to json, to specify that you are expecting JSON data from the server

4. Finally modify the success function to display Name, Gender and Salary property values from the JSON object.

Please note : By defaut, the JSON object returned by the web service has a property d. So to retrieve Name property value, use data.d.Name.

$(document).ready(function () {
    $('#btnGetEmployee').click(function () {

        var empId = $('#txtId').val();

        $.ajax({
            url: 'EmployeeService.asmx/GetEmployeeById',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ employeeId: empId }),
            dataType: "json",
            method: 'post',
            success: function (data) {
                $('#txtName').val(data.d.Name);
                $('#txtGender').val(data.d.Gender);
                $('#txtSalary').val(data.d.Salary);
            },
            error: function (err) {
                alert(err);
            }
        });
    });
});

Approach 2 : With Approach 2 both the web service code and the jQuery code need to change.

Modify the ASP.NET web service as shown below to return JSON data

1. Serialize the employee object to JSON string using JavaScriptSerializer and write it to the response stream.

2. Since the method is not returning anything set the return type of the method to void.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {
        [WebMethod]
        public void GetEmployeeById(int employeeId)
        {
            Employee employee = new Employee();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = employeeId;

                cmd.Parameters.Add(parameter);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(employee));
        }
    }
}

Modify the jQuery code in HtmlPage1.html as shown below.

1. contentType option is no longer required, so you may remove it.

2. You don't have to convert the data that you are sending to the server to JSON string, so you may remove JSON.stringify() method

3. The JSON object returned from the server will not have property d, so you don't have to use d on the data object. For example, you can now retrieve Name property simply by using data.Name.

$(document).ready(function () {
    $('#btnGetEmployee').click(function () {

        var empId = $('#txtId').val();

        $.ajax({
            url: 'EmployeeService.asmx/GetEmployeeById',
            data: { employeeId: empId },
            dataType: "json",
            method: 'post',
            success: function (data) {
                $('#txtName').val(data.Name);
                $('#txtGender').val(data.Gender);
                $('#txtSalary').val(data.Salary);
            },
            error: function (err) {
                alert(err);
            }
        });
    });
});

In our next video we will discuss how to call an asp.net web service that returns JSON arrays using jQuery AJAX.

---------------------------------------------------------------------------------------------

Handling json arrays returned from asp.net web services with jquery
In this video we will discuss how to handle JSON arrays returned from asp.net web service using jQuery.
When we click Get All Employees button, we want to retrieve all the Employees from the database table and display on the page using jQuery.
This is continuation to Part 62, please watch Part 62 before proceeding.

Step 1 : Copy and paste the following code in EmployeeService.asmx.cs

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {
        [WebMethod]
        public void GetAllEmployees()
        {
            List<Employee> listEmployees = new List<Employee>();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblEmployee", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    listEmployees.Add(employee);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listEmployees));
        }
    }
}

Step 2 : Copy and paste the following HTML and jQuery code in HtmlPage1.html

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGetAllEmployees').click(function () {
                $.ajax({
                    url: 'EmployeeService.asmx/GetAllEmployees',
                    dataType: "json",
                    method: 'post',
                    success: function (data) {
                        var employeeTable = $('#tblEmployee tbody');
                        employeeTable.empty();

                        $(data).each(function (index, emp) {
                            employeeTable.append('<tr><td>' + emp.ID + '</td><td>'
                                + emp.Name + '</td><td>' + emp.Gender
                                + '</td><td>' + emp.Salary + '</td></tr>');
                        });
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });
        });
    </script>
</head>
<body style="font-family:Arial">
    <input type="button" id="btnGetAllEmployees" value="Get All Employees" />
    <br /><br />
    <table id="tblEmployee" border="1" style="border-collapse:collapse">
        <thead>
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Gender</th>
                <th>Salary</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>
</body>
</html>

-----------------------------------------------------------------------------------

Save data using asp.net web services and jquery ajax
In this video we will discuss how to save data using asp.net web services and jquery ajax.
This is continuation to Part 63, please watch Part 63 before proceeding.

When Add Employee button is clicked we want to post form data to the asp.net web service and the web service should save the data to the database table.
Step 1 : Create Insert Stored Procedure

Create procedure spInsertEmployee
@Name nvarchar(50),
@Gender nvarchar(10),
@Salary int
as
Begin
     Insert into tblEmployee
     values (@Name, @Gender, @Salary)
End

Step 2 : Modify EmployeeService.asmx.cs as shown below

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {
        [WebMethod]
        public void AddEmployee(Employee emp)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spInsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Name",
                    Value = emp.Name
                });

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Gender",
                    Value = emp.Gender
                });

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Salary",
                    Value = emp.Salary
                });

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [WebMethod]
        public void GetAllEmployees()
        {
            List<Employee> listEmployees = new List<Employee>();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblEmployee", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    listEmployees.Add(employee);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listEmployees));
        }
    }
}

Step 3 : Modify HtmlPage1.html as shown below.

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAddEmployee').click(function () {
                var employee = {};
                employee.Name = $('#txtName').val();
                employee.Gender = $('#txtGender').val();
                employee.Salary = $('#txtSalary').val();

                $.ajax({
                    url: 'EmployeeService.asmx/AddEmployee',
                    method: 'post',
                    data: '{emp: ' + JSON.stringify(employee) + '}',
                    contentType: "application/json; charset=utf-8",
                    success: function () {
                        getAllEmployees();
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });

            function getAllEmployees() {
                $.ajax({
                    url: 'EmployeeService.asmx/GetAllEmployees',
                    dataType: "json",
                    method: 'post',
                    success: function (data) {
                        var employeeTable = $('#tblEmployee tbody');
                        employeeTable.empty();

                        $(data).each(function (index, emp) {
                            employeeTable.append('<tr><td>' + emp.ID + '</td><td>'
                                + emp.Name + '</td><td>' + emp.Gender
                                + '</td><td>' + emp.Salary + '</td></tr>');
                        });
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            }
        });
    </script>
</head>
<body style="font-family:Arial">
    <table border="1" style="border-collapse:collapse">
        <tr>
            <td>Name</td>
            <td><input id="txtName" type="text" /></td>
        </tr>
        <tr>
            <td>Gender</td>
            <td><input id="txtGender" type="text" /></td>
        </tr>
        <tr>
            <td>Salary</td>
            <td><input id="txtSalary" type="text" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="button" id="btnAddEmployee" value="Add Employee" />
            </td>
        </tr>
    </table>
    <br />
    <table id="tblEmployee" border="1" style="border-collapse:collapse">
        <thead>
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Gender</th>
                <th>Salary</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>
</body>
</html>

-----------------------------------------------------------------------------------

Check if username exists in database with ajax
In this video we will discuss how to check if username exists in database using asp.net web service and jQuery ajax.
As we start to type in the the username textbox and when the number of characters in the textbox is equal to or greater than 3, then we want to issue an ajax call to check in the database table if that username is already in use.

If the username is already taken, then we want to display a message stating username already in use.
If the username is not taken yet, then we want to display a message stating username available.
Step 1 : Create SQL Server table and insert test data

Create table tblRegistration
(
     ID int primary key identity,
     Username nvarchar(100)
)
Go

Insert into tblRegistration values ('Pameela')
Insert into tblRegistration values ('SaraBaker')
Go

Step 2 : Create a stored procedure to check if username is in use. This stored procedure returns 1 if the user name is already taken, else 0.

Create procedure spUserNameExists
@UserName nvarchar(100)
as
Begin
     Declare @Count int

     Select @Count = Count(UserName)
     from tblRegistration
     where UserName = @UserName

     If @Count > 0
          Select 1 as UserNameExists
     Else
          Select 0 as UserNameExists
End

Step 3 : Create new asp.net web application project. Name it Demo. 

Step 4 : Include a connection string in the web.config file to your database.
<add name="DBCS"
     connectionString="server=.;database=SampleDB;integrated security=SSPI" />

Step 5 : Add a class file to the project. Name it Registration.cs. Copy and paste the following code.

namespace Demo
{
    public class Registration
    {
        public string UserName { get; set; }
        public bool UserNameInUse { get; set; }
    }
}

Step 6 : Add a new WebService (ASMX). Name it RegistrationService.asmx. Copy and paste the following code.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class RegistrationService : System.Web.Services.WebService
    {
        [WebMethod]
        public void UserNameExists(string userName)
        {
            bool userNameInUse = false;

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUserNameExists", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@UserName",
                    Value = userName
                });

                con.Open();
                userNameInUse = Convert.ToBoolean(cmd.ExecuteScalar());
            }

            Registration regsitration = new Registration();
            regsitration.UserName = userName;
            regsitration.UserNameInUse = userNameInUse;

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(regsitration));
        }
    }
}

Step 7 : Add an HTML page to the ASP.NET project. Copy and paste the following HTML and jQuery code

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#txtUserName').keyup(function () {
                var userName = $(this).val();

                if (userName.length >= 3) {
                    $.ajax({
                        url: 'RegistrationService.asmx/UserNameExists',
                        method: 'post',
                        data: { userName: userName },
                        dataType: 'json',
                        success: function (data) {
                            var divElement = $('#divOutput');
                            if (data.UserNameInUse) {
                                divElement.text(data.UserName + ' already in use');
                                divElement.css('color', 'red');
                            }
                            else {
                                divElement.text(data.UserName + ' available')
                                divElement.css('color', 'green');
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });
        });
    </script>
</head>
<body style="font-family:Arial">
    <table>
        <tr>
            <td>
                UserName :
            </td>
            <td>
                <input id="txtUserName" type="text" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div id="divOutput"></div>
            </td>
        </tr>
    </table>
</body>
</html>

----------------------------------------------------------------------------------

How to suggest available username
In this video we will discuss, how to suggest available username using asp.net web services and jquery ajax.
This is continuation to Part 65, please watch Part 65 before proceeding.

When you try to create a gmail account, if the username that you have provided is already taken by another user, you will be suggested with a username that is available. Once you click on the available username, the username textbox will be populated with the suggested username.
Let us now discuss how to achieve this using asp.net web services and jQuery AJAX.
Step 1 : Modify RegistrationService.asmx.cs as shown below.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    [System.Web.Script.Services.ScriptService]
    public class RegistrationService : System.Web.Services.WebService
    {
        public bool UserNameExists(string userName)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUserNameExists", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@UserName",
                    Value = userName
                });

                con.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        [WebMethod]
        public void GetAvailableUserName(string userName)
        {
            Registration regsitration = new Registration();
            regsitration.UserNameInUse = false;

            while (UserNameExists(userName))
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 100);
                userName = userName + randomNumber.ToString();
                regsitration.UserNameInUse = true;
            }

            regsitration.UserName = userName;
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(regsitration));
        }
    }
}

Step 2 : Modify the HTML and jQuery code in HtmlPage1.html as shown below.

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtUserName').keyup(function () {
                var userName = $(this).val();
                if (userName.length >= 3) {
                    $.ajax({
                        url: 'RegistrationService.asmx/GetAvailableUserName',
                        method: 'post',
                        data: { userName: userName },
                        dataType: 'json',
                        success: function (data) {
                            var divElement = $('#divOutput');
                            var spanElement = $('#spanAvailableUserName');
                            var linkElement = $('#linkAvailableUserName');

                            if (data.UserNameInUse) {
                                divElement.text(userName + ' already in use. Try another?');
                                divElement.css('color', 'red');
                                spanElement.text('Available :');
                                linkElement.text(data.UserName).click(function () {
                                    $('#txtUserName').val($(this).text());
                                    divElement.text('');
                                    spanElement.text('');
                                    linkElement.text('');
                                });
                            }
                            else {
                                divElement.text(data.UserName + ' available')
                                divElement.css('color', 'green');
                                spanElement.text('');
                                linkElement.text('');
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });
        });
    </script>
</head>
<body style="font-family:Arial">
    <table>
        <tr>
            <td>
                UserName :
            </td>
            <td>
                <input id="txtUserName" type="text" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div id="divOutput"></div>
                <span id="spanAvailableUserName"></span>
                <a id="linkAvailableUserName" href="#"></a>
            </td>
        </tr>
    </table>
</body>
</html>

-------------------------------------------------------------------------------------

Calling aspx page method using jquery
In this video we will discuss how to call server side aspx page method using jQuery AJAX. 


 

We will have a c# function in the code-behind file, the retrieves employee details by ID from the SQL Server database. We want to call this server side c# function using jquery and display the data as shown below.
Step 1 : Create SQL Server table and insert employee data

Create table tblEmployee
(
     Id int primary key identity,
     Name nvarchar(50),
     Gender nvarchar(10),
     Salary int
)
GO

Insert into tblEmployee values ('Mark', 'Male', 50000)
Insert into tblEmployee values ('Sara', 'Female', 60000)
Insert into tblEmployee values ('John', 'Male', 45000)
Insert into tblEmployee values ('Pam', 'Female', 45000)
GO

Step 2 : Create a stored procedure to retrieve employee data by ID

Create procedure spGetEmployeeById
@Id int
as
Begin
 Select ID, Name, Gender, Salary
 from tblEmployee
 where ID = @Id
End

Step 3 : Create new asp.net web application project. Name it Demo. 

Step 4 : Include a connection string in the web.config file to your database.
<add name="DBCS"
     connectionString="server=.;database=SampleDB;integrated security=SSPI" />

Step 5 : Add a class file to the project. Name it Employee.cs. Copy and paste the following code.

namespace Demo
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
    }
}

Step 6 : Add a new WebForm. Name it WebForm.aspx. Copy and paste the following code in the code-behind page.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Demo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod]
        public static Employee GetEmployeeById(int employeeId)
        {
            Employee employee = new Employee();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Id",
                    Value = employeeId
                });
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                }
            }

            return employee;
        }
    }
}

Step 7 : Copy and paste the following HTML and jQuery code in WebForm1.aspx

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs"
         Inherits="Demo.WebForm1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGetEmployee').click(function () {
                var empId = $('#txtId').val();
                $.ajax({
                    url: 'WebForm1.aspx/GetEmployeeById',
                    method: 'post',
                    contentType: "application/json",
                    data: '{employeeId:' + empId + '}',
                    dataType: "json",
                    success: function (data) {
                        $('#txtName').val(data.d.Name);
                        $('#txtGender').val(data.d.Gender);
                        $('#txtSalary').val(data.d.Salary);
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });
        });
    </script>
</head>
<body style="font-family: Arial">
    <form id="form1" runat="server">
        ID :
        <input id="txtId" type="text" style="width: 86px" />
        <input type="button" id="btnGetEmployee" value="Get Employee" />
        <br /><br />
        <table border="1" style="border-collapse: collapse">
            <tr>
                <td>Name</td>
                <td><input id="txtName" type="text" /></td>
            </tr>
            <tr>
                <td>Gender</td>
                <td><input id="txtGender" type="text" /></td>
            </tr>
            <tr>
                <td>Salary</td>
                <td><input id="txtSalary" type="text" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
---------------------------------------------------------------------------------

How to call wcf service using jquery

We want to retrieve data from the following database table tblEmployee

The retrieved data should be displayed as shown below

Step 1 : Create SQL Server table and insert employee data

Create table tblEmployee
(
     Id int primary key identity,
     Name nvarchar(50),
     Gender nvarchar(10),
     Salary int
)
GO

Insert into tblEmployee values ('Mark', 'Male', 50000)
Insert into tblEmployee values ('Sara', 'Female', 60000)
Insert into tblEmployee values ('John', 'Male', 45000)
Insert into tblEmployee values ('Pam', 'Female', 45000)
GO

Step 2 : Create a stored procedure to retrieve employee data by ID

Create procedure spGetEmployeeById
@Id int
as
Begin
     Select ID, Name, Gender, Salary
     from tblEmployee
     where ID = @Id
End

Step 3 : Create new asp.net web application project. Name it Demo. 

Step 4 : Include a connection string in the web.config file to your database.
<add name="DBCS"
      connectionString="server=.;database=SampleDB;integrated security=SSPI" />

Step 5 : Add a class file to the project. Name it Employee.cs. Copy and paste the following code.

namespace Demo
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
    }
}

Step 6 : Add a new WCF Service (Ajax-enabled). Name it EmployeeService.svc. Copy and paste the following code.

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Demo
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode =
        AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmployeeService
    {
        [OperationContract]
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = new Employee();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = employeeId;

                cmd.Parameters.Add(parameter);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                }
            }

            return employee;
        }
    }
}

Step 7 : Add an HTML page to the ASP.NET project. Copy and paste the following HTML and jQuery code

<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGetEmployee').click(function () {
                var empId = $('#txtId').val();
                $.ajax({
                    url: 'EmployeeService.svc/GetEmployeeById',
                    method: 'post',
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ employeeId: empId }),
                    dataType: 'json',
                    success: function (data) {
                        $('#txtName').val(data.d.Name);
                        $('#txtGender').val(data.d.Gender);
                        $('#txtSalary').val(data.d.Salary);
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            });
        });
    </script>
</head>
<body style="font-family:Arial">
    ID :
    <input id="txtId" type="text" style="width: 86px" />
    <input type="button" id="btnGetEmployee" value="Get Employee" />
    <br /><br />
    <table border="1" style="border-collapse: collapse">
        <tr>
            <td>Name</td>
            <td><input id="txtName" type="text" /></td>
        </tr>
        <tr>
            <td>Gender</td>
            <td><input id="txtGender" type="text" /></td>
        </tr>
        <tr>
            <td>Salary</td>
            <td><input id="txtSalary" type="text" /></td>
        </tr>
    </table>
</body>
</html>

-------------------------------------------------------------------------------

Difference between window height and document height
In this video we will discuss 
1. Difference between window height and document height
2. How to detect if the user has scrolled to the bottom of the page


$(window).height()	Returns height of the browser window i.e browser viewport
$(document).height()	Returns height of HTML document
$(window).scrollTop()	Returns the current vertical position of the scroll bar

What is the difference between window height and document height
The window height is what you see (i.e browser viewport), but the document height includes everything below or above the visible area.

How to detect if the user has scrolled to the bottom of the page
Here is the formula to detect if the user has scrolled to the bottom of the page

if (verticalScrollBarPosition == $(document).height() - $(window).height()) {
    floatingDiv.html('Reached the bottom of the page');
}

Why window height and document height are the same in Google chrome
Without DOCTYPE tag Chrome reports the same value for both window height and document height. Include the following DOCTYPE declaration
<!DOCTYPE html>

Example used in the demo

<!DOCTYPE html>
<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var floatingDiv = $("#divfloating");
            var floatingDivPosition = floatingDiv.position();
            $(window).scroll(function () {
                var scrollPosition = $(window).scrollTop();
                if (scrollPosition >= floatingDivPosition.top) {
                    floatingDiv.css({
                        'position': 'fixed',
                        'top': 3
                    });
                } else {
                    floatingDiv.css({
                        'position': 'relative',
                        'top': 0
                    });
                }

                floatingDiv.html('Window Height = ' + $(window).height() + '<br/>' +
                        'Document Height = ' + $(document).height() + '<br/>' +
                        'Vertical Scrollbar Position = ' + scrollPosition
                    );

                if (scrollPosition == $(document).height() - $(window).height()) {
                    floatingDiv.html('Reached the bottom of the page');
                }
            });
        });
    </script>
</head>
<body style="font-family:Arial;">
    <table align="center" border="1" style="border-collapse:collapse">
        <tr>
            <td style="width:500px">
                <div>
                    Main Page Content
                </div>
            </td>
            <td style="width:150px; vertical-align:top">
                Side panel content
                <br /><br />
                <div id="divfloating" style="background-color:silver; width:150px; height:150px">
                    Floating Div - Keeps floating as you scroll down the page
                </div>
            </td>
        </tr>
    </table>
</body>
</html>

In our next video we will discuss how to load data like Facebook as you scroll down the page.

------------------------------------------------------------------------------------
Load data on page scroll using jquery
In this video we will discuss, how to load more data on page scroll using jQuery AJAX. 
This is similar to Facebook. As you scroll down on the page more data will be loaded.

When the page is initially loaded we want to retrieve and display the first 50 rows from the following database table tblEmployee. As we scroll down and when we hit the bottom of the page we want to load the next set of 50 rows.
Step 1 : Create SQL Server table and insert employee data

Create table tblEmployee
(
    Id int primary key identity,
    Name nvarchar(50),
    Gender nvarchar(10),
    Salary int
)
GO

Insert into tblEmployee values('Name 1','Male 1',100)
Insert into tblEmployee values('Name 2','Male 2',200)
Insert into tblEmployee values('Name 3','Male 3',300)
Insert into tblEmployee values('Name 4','Male 4',400)
Insert into tblEmployee values('Name 5','Male 5',500)

Step 2 : Create a stored procedure to retrieve employee data. This procedure retrieves the correct set of 50 records depending on the page number.

Create procedure spGetEmployees
@PageNumber int,
@PageSize int
as
Begin

    Declare @StartRow int
    Declare @EndRow int

    Set @StartRow = ((@PageNumber - 1) * @PageSize) + 1
    Set @EndRow = @PageNumber * @PageSize;


    WITH RESULT AS
    (
     SELECT Id, Name, Gender, Salary,
             ROW_NUMBER() OVER (ORDER BY ID ASC) AS ROWNUMBER
     From    tblEmployee
    )
    SELECT *
    FROM RESULT
    WHERE ROWNUMBER BETWEEN @StartRow AND @EndRow
End

Step 3 : Create new asp.net web application project. Name it Demo. 

Step 4 : Include a connection string in the web.config file to your database.
<add name="DBCS"
      connectionString="server=.;database=SampleDB;integrated security=SSPI"/>

Step 5 : Add a class file to the project. Name it Employee.cs. Copy and paste the following code.

namespace Demo
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
    }
}

Step 6 : Add a new WebService (ASMX). Name it EmployeeService.asmx. Copy and paste the following code.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Demo
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {
        [WebMethod]
        public void GetEmployees(int pageNumber, int pageSize)
        {
            List<Employee> listEmployees = new List<Employee>();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployees", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@PageNumber",
                    Value = pageNumber
                });

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@PageSize",
                    Value = pageSize
                });

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    listEmployees.Add(employee);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listEmployees));
        }
    }
}

Step 7 : Add an HTML page to the ASP.NET project. Copy and paste the following HTML and jQuery code

<!DOCTYPE html>
<html>
<head>
    <script src="jquery-1.11.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var currentPage = 1;
            loadPageData(currentPage);
            $(window).scroll(function () {
                if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                    currentPage += 1;
                    loadPageData(currentPage);
                }
            });

            function loadPageData(currentPageNumber) {
                $.ajax({
                    url: 'EmployeeService.asmx/GetEmployees',
                    method: 'post',
                    dataType: "json",
                    data: { pageNumber: currentPageNumber, pageSize: 50 },
                    success: function (data) {
                        var employeeTable = $('#tblEmployee tbody');

                        $(data).each(function (index, emp) {
                            employeeTable.append('<tr><td>' + emp.ID + '</td><td>'
                                + emp.Name + '</td><td>' + emp.Gender
                                + '</td><td>' + emp.Salary + '</td></tr>');
                        });
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
            }
        });
    </script>
</head>
<body style="font-family:Arial">
    <h1>The data will be loaded on demand as you scroll down the page</h1>
    <table id="tblEmployee" border="1"
           style="border-collapse:collapse; font-size:xx-large">
        <thead>
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Gender</th>
                <th>Salary</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>
</body>
</html>

--------------------------------------------------------------------------------



























































































