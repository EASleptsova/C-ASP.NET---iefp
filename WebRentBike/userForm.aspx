<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userForm.aspx.cs" Inherits="userForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User</title>
    <link rel="shortcut icon" href="favicon/favicon.ico"/>
   <link rel="icon" type="image/gif" href="favicon/animated_favicon1.gif"/>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            Greetings
            <asp:Label ID="Label1" runat="server" ForeColor="#3366FF" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Font-Italic="True" ForeColor="#009933" Text="You have an active rent" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" ForeColor="#CC3300" Height="23px" Text="Finish rent" Visible="False" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="#0066FF" Text="You can start a new rent" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" ForeColor="#339933" Height="23px" style="margin-left: 0px" Text="Start" Width="93px" Visible="False" OnClick="Button2_Click" />
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CellPadding="4" EnableSortingAndPagingCallbacks="True" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="798px" AutoGenerateColumns="False" Height="65px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>                  
                    <asp:BoundField HeaderText="status" ReadOnly="True"  DataField="status"/>
                    <asp:BoundField HeaderText="user" ReadOnly="True"  DataField="name"/>
                    <asp:BoundField HeaderText="origin" ReadOnly="True"  DataField="origin"/>
                    <asp:BoundField HeaderText="destination" ReadOnly="True"  DataField="destination"/>
                    <asp:BoundField HeaderText="start date" ReadOnly="True"  DataField="startDate" DataFormatString="{0:dd-M-yyyy}"/>
                    <asp:BoundField HeaderText="end date" ReadOnly="True"  DataField="endDate" DataFormatString="{0:dd-M-yyyy}"/>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <br />
            <asp:Button ID="Button3" runat="server" Text="Sign out" OnClick="Button3_Click" />
&nbsp;&nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
