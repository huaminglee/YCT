<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OALoginSetting.aspx.cs" Inherits="EDoc2.YCT.Website.OALoginSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>OA系统登录设置</h3>
        <table>
            <tr>
                <td style="width: 100px">用户名</td>
                <td><asp:TextBox ID="txtUserName" runat="server" style="width: 200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>密码</td>
                <td><asp:TextBox ID="txtPassword" TextMode="Password" style="width: 200px" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        
        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_OnClick" />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
