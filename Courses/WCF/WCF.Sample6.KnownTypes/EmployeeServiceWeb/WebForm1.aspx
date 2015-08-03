<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EmployeeServiceWeb.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <b>Employee Type</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployeeType" runat="server" OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Select Employee Type" Value="-1" />
                        <asp:ListItem Text="Full Time" Value="1" />
                        <asp:ListItem Text="Part Time" Value="2" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAnnual" runat="server" visible="false">
                <td>
                    <b>Annual Salary</b>
                </td>
                <td>
                    <asp:TextBox ID="txtAnnualSalary" runat="server" />
                </td>
            </tr>
            <tr id="trHourlyPay" runat="server" visible="false">
                <td>
                    <b>Hourly Pay</b>
                </td>
                <td>
                    <asp:TextBox ID="txtHourlyPay" runat="server" />
                </td>
            </tr>
            <tr id="trHourWorked" runat="server" visible="false">
                <td>
                    <b>Hours Worked</b>
                </td>
                <td>
                    <asp:TextBox ID="txtHoursWorked" runat="server" />
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
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
