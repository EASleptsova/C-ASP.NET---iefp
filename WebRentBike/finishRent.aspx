<%@ Page Language="C#" AutoEventWireup="true" CodeFile="finishRent.aspx.cs" Inherits="finishRent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Finish rent</title>
    <link rel="shortcut icon" href="favicon/favicon.ico"/>
   <link rel="icon" type="image/gif" href="favicon/animated_favicon1.gif"/>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            Finish rent<br />
            <br />
            Are you sure you want to finish the rent ?&nbsp;&nbsp;
            <br />
            <br />
            Choose destination station<br />
            <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" Width="342px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Ok" OnClick="Button1_Click" />
&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" />
        </div>
    </form>
</body>
</html>
