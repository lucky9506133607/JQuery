<html>
<head>
    <title></title>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('li').css('color', 'blue');
            $('li').slideUp(1000);
            $('li').slideDown(1000);
            $('li').attr('title', 'MY TITLE');
        });
    </script>
</head>
<body style="font-family:Arial">
    <ul>
        <li>US</li>
        <li>India</li>
        <li>UK</li>
        <li>Canada</li>
        <li>Australia</li>
    </ul>
</body>
</html>