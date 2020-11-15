<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>
<%@ Register src="MessageBoxUserControl.ascx" tagname="MessageBoxUserControl" tagprefix="uc1" %>f
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-image: url('images.jpg'); background-repeat: no-repeat; background-attachment: fixed; background-position: 100% 100%">
    <form id="form1" runat="server">
    <div>
    <p style="font-family: 'Courier New', Courier, monospace; font-size: large">Error Page</p>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
