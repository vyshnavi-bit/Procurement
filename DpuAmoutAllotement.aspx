<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuAmoutAllotement.aspx.cs" Inherits="DpuAmoutAllotement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 24px;
        }
                
             .style103
        {
            width: 313px;
        }
        
        .style104
        {
            width: 100%;
        }
        
    </style>

 <script language="javascript" type="text/javascript">


     function isNumber(evt) {
         var iKeyCode = (evt.which) ? evt.which : evt.keyCode
         if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
             return false;

         return true;

     }


     function isstring(evt) 
     {
         var pwd = (evt.which) ? evt.which : evt.keyCode;
         if (pwd.match("^[a-z]*$")) {
           
         }
         else {
             alert('Not alphanumeric');
         }
     }



     function Allvalidate() {
         var summary = "";
         summary += isvalidamount();
         summary += isvalidFirstname();
         if (summary != "") {
             alert(summary);
             return false;
         }
         else {
             return false;
         }
     }
     function isvalidamount() {
         var uid;
         var temp = document.getElementById("<%=tbNumbers.ClientID %>");
         uid = temp.value;
         if (uid == "") {
             return ("Please Enter Amount" + "\n");
         }
         else {
             return "";
         }
     }

     function isvalidFirstname() {
         var uid;
         var temp = document.getElementById("<%=txt_description.ClientID %>");
         uid = temp.value;
         if (uid == "") {
             return ("Please Enter Description" + "\n");
         }
         else {
             return "";
         }
     }


       function confirmationclose() {
            if (confirm('are you  want to close ?')) {
                return true;
            }
            else {
                return false;
            }
        }



</script>



    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<form>
<body>
<html>
    <div>
<center>


    <br />


    <asp:Label ID="Label1" runat="server" Text="DPU Amount Allotment" 
        style="font-family: serif; font-size: medium;"></asp:Label>

 
    <br />
   
    <table class="style104">
        <tr align="right">
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" 
                    PostBackUrl="~/DpuAmountForRoute.aspx">Plant Amount Allot</asp:LinkButton>
            </td>
        </tr>
    </table>
   
    <br />

</center>

</div>
<div  align="center">

<center style="border-style: none">
<asp:Panel ID="Panel1"  Width="40%" runat="server"    BorderWidth="1px">

   <center>

    <table  class="style103" cellspacing="2">
        <tr align="left">
            <td width=50%>


    <asp:Label ID="Plantname" runat="server" Text="Cash In Hnd" 
        style="font-family: serif"></asp:Label>

                <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" 
                    Height="16px" ReadOnly="true" TabIndex="1" Visible="False" Width="20px">
                </asp:DropDownList>

            </td>
            <td>
                        <asp:TextBox ID="txt_cashamt" runat="server" CssClass="tb10" Height="25px" 
                            Width="142px" Enabled="False"></asp:TextBox>
                    </td>
        </tr>
        <tr align="left">
            <td class="style4">


    <asp:Label ID="Plantname12" runat="server" Text="Ref Id" 
        style="font-family: serif"></asp:Label>

            </td>
            <td class="style4">
                        <asp:TextBox ID="txt_ref" runat="server" CssClass="tb10" Height="25px" 
                            Width="142px" Enabled="False"></asp:TextBox>
                    </td>
        </tr>
        <tr align="left">
            <td>


    <asp:Label ID="Plantname1" runat="server" Text="Date &amp;Time" 
        style="font-family: serif"></asp:Label>

            </td>
            <td>
                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Height="25px" 
                            Width="142px" Enabled="False"></asp:TextBox>
                    </td>
        </tr>
        <tr align="left">
            <td>
                                      <asp:Label ID="Plantname2" runat="server" style="font-family: serif" 
                                          Text="Amount"></asp:Label>
            </td>
            <td>

                                       <div>
     <asp:TextBox ID="tbNumbers" runat="server"  MaxLength="8"  onkeypress="javascript:return isNumber(event)" CssClass="tb10" Height="25px" Width="143px">  </asp:TextBox>


                                              


    </div>
                                    </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="Plantname3" runat="server" style="font-family: serif" 
                    Text="Description"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_description" runat="server"   MaxLength="75"   onkeypress="javascript:return isstring(event)" CssClass="tb10"      Height="25px" Width="143px"     TextMode="MultiLine" /></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:TextBox ID="txt_name" runat="server" Enabled="False" Height="19px" 
                    Visible="False" Width="100px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txt_time" runat="server" Enabled="False" Height="18px" 
                    Visible="False" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:TextBox ID="txt_userid" runat="server" Enabled="False" Height="18px" 
                    Visible="False" Width="20px"></asp:TextBox>
              <%--  <asp:Button ID="Savebtn" runat="server" BackColor="Green" BorderStyle="Double" xmlns:asp="#unknown"
                    Font-Bold="True" ForeColor="White" Height="26px"   onclick="Savebtn_Click" Style="height: 26px" Text="Save" OnClientClick ="javascript:validate()"    Width="50px" />
--%>
                      <asp:Button ID="Savebtn" runat="server" text="Save" xmlns:asp="#unknown"   BackColor="Green" BorderStyle="Inset" Font-Bold="True" ForeColor="White" Height="26px"  Width="60px"   OnClientClick="javascript:Allvalidate()"   onclick="Savebtn_Click"/></asp:Button>
                

                <asp:Button ID="btn_delete" runat="server" BackColor="RED" BorderStyle="Inset" 
                   Style="height: 26px" text="Close" 
                    Width="60px" xmlns:asp="#unknown" onclick="btn_delete_Click" 
                    Visible="False" />
                <asp:TextBox ID="txt_previous" runat="server" Height="16px" 
                    ontextchanged="txt_previous_TextChanged" Visible="False" Width="24px"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:Label ID="lbl_msg" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        </table>
    </center>
   
   </asp:Panel>

   
  

    <br />
  </center>
</div>
<center>
  <asp:Panel ID="Panel2" runat="server"  Width="50%">
      <asp:GridView ID="GridView1" runat="server" CssClass="gridview1" 
          AutoGenerateColumns="False" AllowPaging="True" Font-Size="12px" 
          onpageindexchanging="GridView1_PageIndexChanging" 
          onrowdatabound="GridView1_RowDataBound" PageSize="20">
          <Columns>
              <asp:BoundField DataField="Tid" HeaderText="Tid" SortExpression="Tid" />
              <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
              <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
              <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
              <asp:BoundField DataField="Amount" HeaderText="Amount" 
                  SortExpression="Amount" />
              <asp:BoundField DataField="Description" HeaderText="Description" 
                  SortExpression="Description" />
          </Columns>
      </asp:GridView>
    </asp:Panel>

    </center>


<div  align="center">
    <br />
    <br />
  
</div>
</form>
</body>
</html>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>



