<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CompanyClientWeb.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPublic" runat="server" Text="Get Public Info" 
            onclick="btnPublic_Click" /><asp:Label ID="lblPublic"
            runat="server" Text="{{Public Info Here}}"></asp:Label>
    </div>
    <div>
        <asp:Button ID="btnConfidential" runat="server" Text="Get Public Info" 
            onclick="btnConfidential_Click" /><asp:Label ID="lblConfidential"
            runat="server" Text="{{Confidential Info Here}}"></asp:Label>
    </div>
    </form>
</body>
</html>
