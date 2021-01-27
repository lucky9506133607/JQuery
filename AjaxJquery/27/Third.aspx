<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Third.aspx.cs" Inherits="_27_Third" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //            var employeeJSON = {
            //                "firstName": "Todd",
            //                "lastName": "Grover",
            //                "gender": "Male",
            //                "salary": 50000
            //            };
            //            var result = '';
            //            result += 'First Name = ' + employeeJSON.firstName + '<br/>';
            //            result += 'Last Name = ' + employeeJSON.lastName + '<br/>';
            //            result += 'Gender = ' + employeeJSON.gender + '<br/>';
            //            result += 'Salary = ' + employeeJSON.salary + '<br/>';
            //            $('#result').html(result);
            //        });       

            var employeesJSON = {
                "Todd": {
                    "firstName": "Todd",
                    "lastName": "Grover",
                    "gender": "Male",
                    "salary": 50000
                },
                "Sara": {
                    "firstName": "Sara",
                    "lastName": "Baker",
                    "gender": "Female",
                    "salary": 40000
                }
            };

            var result = "";
            result += "first name = " + employeesJSON.Sara.firstName + "<br/>";
            result += "last name = " + employeesJSON.Sara.lastName + "<br/>";
            result += "gender = " + employeesJSON.Sara.gender + "<br/>";
            result += "salary = " + employeesJSON.Sara.salary + "<br/>";

            $('#result').html(result);
        });
        </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="result">
    
    </div>
    </form>
</body>
</html>
