<%@ Page Language="C#" AutoEventWireup="true" CodeFile="third.aspx.cs" Inherits="third" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            alert($('table').length);
            $(".mydiv").attr("class", "container");
            $('table thead tr').css('background-color', 'orange');
            $('table tbody tr:even').css('background-color', 'yellow');
            $('table tbody tr:Odd').css('background-color', 'blue');
            $('table tbody td:even').css('background-color', 'green');
            $('table tbody td:Odd').css('background-color', 'brown');
            //$('.small').filter('.big').css('border', '5px solid red');
        });
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="mydiv">
    <table class="table">
    <thead>
      <tr>
        <th>Firstname</th>
        <th>Lastname</th>
        <th>Email</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>John</td>
        <td>Doe</td>
        <td>john@example.com</td>
      </tr>
      <tr>
        <td>Mary</td>
        <td>Moe</td>
        <td>mary@example.com</td>
      </tr>
      <tr>
        <td>July</td>
        <td>Dooley</td>
        <td>july@example.com</td>
      </tr>
    </tbody>
  </table>
    </div>
    </form>
</body>
</html>
