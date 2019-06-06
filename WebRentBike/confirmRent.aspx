<%@ Page Language="C#" AutoEventWireup="true" CodeFile="confirmRent.aspx.cs" Inherits="confirmRent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirm rent</title>
   <link rel="shortcut icon" href="favicon/favicon.ico"/>
   <link rel="icon" type="image/gif" href="favicon/animated_favicon1.gif"/>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            New rent<br />
            <br />
            Are you sure you want to start a new rent ?&nbsp;&nbsp;
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Ok" OnClick="Button1_Click" />
&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" />
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="#CC3300" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
