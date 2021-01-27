<%@ Page Language="C#" AutoEventWireup="true" CodeFile="First.aspx.cs" Inherits="_27_First" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js">
     </script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            // $('li').each(function (index, element) {
            //alert("index is = " + index + "Element text is = " + $(this).text())
            $('#ul').click(function () {
                $('#ul li').toggle();
            });
        });
    </script>

</head>

   <body style="font-family:Arial">
  
    <ul id="ul"> country
        <li>US</li>
        <li>India</li>
        <li>UK</li>
        <li>Canada</li>
        <li>Australia</li>
    </ul>
</body>
</html>
