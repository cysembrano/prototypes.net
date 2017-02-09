<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WCF.P15.ExceptionHandling_C.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Numerator"></asp:Label><asp:TextBox ID="TextBox1"
            runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Denominator"></asp:Label><asp:TextBox
            ID="TextBox2" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="Divide" onclick="Button1_Click" />
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="Result"></asp:Label>
    </div>
    </form>
</body>
</html>
