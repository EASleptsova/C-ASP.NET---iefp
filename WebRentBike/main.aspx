<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main</title>
   <link rel="shortcut icon" href="favicon/favicon.ico"/>
   <link rel="icon" type="image/gif" href="favicon/animated_favicon1.gif"/>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            Welcome to the&nbsp; Rent Bike Application!<br />
            <br />
            How does it work?<br />
            <br />
            - Sign up or login<br />
            - Unlock a bike<br />
            - Enjoy the ride<br />
            - Return the bike<br />
            <br />
            <br />
            Username&nbsp;&nbsp; <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            Password&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Sign in" OnClick="Button1_Click" />
            &nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" ForeColor="#CC3300" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            Create account&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Sign up" OnClick="Button2_Click" />
        </div>
    </form>
</body>
</html>
