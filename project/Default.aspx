<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        span
        {
            width: 100%;
            float: left;
        }
        img
        {
            float: left;
        }
        .userinfo
        {
            float: left;
            width: 100%;
            border: 1px solid #000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/callback.aspx" onclick="window.open(this.href, 'Facebook_Login', 'status=0,toolbar=0,location=0,menubar=0,width=940,height=550'); return false;">facebook Connect</asp:HyperLink>
        <br />
        <div class="userinfo">
            <asp:Image ID="imguser" runat="server" />
            <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblusername" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblfirstname" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbllastname" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblbirthday" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbllocation" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblschoolName" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblschoolsectionname" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="getfrient" runat="server" Text="arkadaş listesi" OnClick="getfrient_Click" />
        <br />
        <asp:TextBox ID="txtpostinput" runat="server"></asp:TextBox>
        <asp:Button ID="textpost" runat="server" Text="Button" 
            onclick="textpost_Click" />
    </div>
    </form>
</body>
</html>
