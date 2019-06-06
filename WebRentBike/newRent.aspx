<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newRent.aspx.cs" Inherits="newRent" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>
<script runat="server">  
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Rent</title>
    <link rel="shortcut icon" href="favicon/favicon.ico"/>
   <link rel="icon" type="image/gif" href="favicon/animated_favicon1.gif"/>
</head>
<body>
    <form id="form1" runat="server">
      
        <div align="center">
            Create new Rent!<br />
            <br />
            <br />
&nbsp;Choose origin station&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="389px" meta:resourcekey="DropDownList1Resource1">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Start Rent" meta:resourcekey="Button1Resource1" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Cancel" />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
