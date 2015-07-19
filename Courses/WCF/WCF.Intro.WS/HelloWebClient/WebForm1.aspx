<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HelloWebClient.WebForm1" %>

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
                    <asp:TextBox ID="Textbox1" runat="server" /> 
                    <asp:Button ID="Button1" runat="server" Text="Get Message" 
                        onclick="Button1_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
