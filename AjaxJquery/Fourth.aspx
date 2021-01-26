<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fourth.aspx.cs" Inherits="Fourth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script>
      $(document).ready(function () {
          $('div[title]').filter(function () {
              // return $(this).attr('title').toLowerCase() == "divtitle";
              // return (/1Div/i).test($(this).attr('title'));
              return RegExp('Div','i').test($(this).attr('title'));
          }).css('border', '3px solid red');
      });
  </script>

<body>
   <div title="DivTitle">
   div1
   </div>
<div title="DIVTITLE">
DIV2
</div>
<div title="1divtitle">
div3
</div>
<div title="1DIVTITLE">
div4
</div>
</body>
</html>
