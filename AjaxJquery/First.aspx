<%@ Page Language="C#" AutoEventWireup="true" CodeFile="First.aspx.cs" Inherits="First" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script type="text/javascript">
       
        $(window).load(function () {
            $('#div1').html("height =" + $('#img1').height() + "<br/>" + "width = "+$('#img1').width());
        });

     //   $(document).ready(function () {
      //      $("#Click").click(function () {
       //         alert("dom loaded");
        //    });
       // });
</script>

</head>
<body>
    <div id="div1">
    </div>
    <img id="img1" src="Penguins.jpg"/>

</body>
</html>
