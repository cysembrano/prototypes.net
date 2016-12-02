<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EmployeeServiceWeb.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <b>ID</b>
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>NAME</b>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Gender</b>
                </td>
                <td>
                    <asp:TextBox ID="txtGender" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Date Of Birth</b>
                </td>
                <td>
                    <asp:TextBox ID="txtDateOfBirth" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnGet" Text="Get Employee" runat="server" OnClick="btnGet_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSave" Text="Save Employee" runat="server" OnClick="btnSave_Click" />
                </td>
            </tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" />
            </td>
        </table>
    </div>
    </form>
</body>
</html>
