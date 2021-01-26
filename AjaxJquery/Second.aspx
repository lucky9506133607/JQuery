<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Second.aspx.cs" Inherits="Second" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('Button1').style.backgroundColor = "Yellow";
            alert($('#Button1')[0]);
        });
        
    </script>

</head>
<input id="Button1" type="button" value="button" />
<input id="Button2" type="button" value="button" />
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
