<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fourth.aspx.cs" Inherits="_27_fourth" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
//    var employeesJSON = [
//                 {
//                    "firstName": "Todd",
//                    "lastName": "Grover",
//                    "gender": "Male",
//                    "salary": 50000
//                },
//                 {
//                    "firstName": "Sara",
//                    "lastName": "Baker",
//                    "gender": "Female",
//                    "salary": 40000
//                }
//            ];
//            var JSONString = JSON.stringify(employeesJSON);
            //            $('#resultDiv').html(JSONString);
            var JSONString = '[{ "firstName": "Todd", "lastName": "Grover", "gender": "Male", "salary": 50000 }, { "firstName": "Sara", "lastName": "Baker", "gender": "Female", "salary": 40000 }]';

            var employeesJSON = JSON.parse(JSONString);

            var result = '';

            $.each(employeesJSON, function (i, item) {
                result += 'First Name = ' + item.firstName + '<br/>';
                result += 'Last Name = ' + item.lastName + '<br/>';
                result += 'Gender = ' + item.gender + '<br/>';
                result += 'Salary = ' + item.salary + '<br/><br/>';
            });

            $('#resultDiv').html(result);
            });
            </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="resultDiv" title="muyfshdfhsfu">
    <h3> line number {1000} </h3>
    </div>
    </form>
</body>
</html>
