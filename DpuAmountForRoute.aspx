<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuAmountForRoute.aspx.cs" Inherits="DpuAmountForRoute" %>
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


//     function isstring(evt) {
//         var pwd = (evt.which) ? evt.which : evt.keyCode;
//         if (pwd.match("^[a-z]*$")) {

//         }
//         else {
//             alert('Not alphanumeric');
//         }
//     }



     function Allvalidate() {
         var summary = "";
         summary += isvalidamount();
       //  summary += isvalidFirstname();
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
         var temp = document.getElementById("<%=Txtamount.ClientID %>");
         uid = temp.value;
         if (uid == "") {
             return ("Please Enter Amount" + "\n");
         }
         else {
             return "";
         }
     }




     function confirmationclose() 
     {
         if (confirm('are you  want to close ?')) 
         {
             return true;
         }
         else
          {
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


    <table class="style104">
        <tr align="center">
            <td colspan="2">


    <asp:Label ID="Label1" runat="server" Text="DPU Plant Amount Allotment" 
        style="font-family: serif; font-size: medium;"></asp:Label>

            </td>
        </tr>
        <tr align="right">
            <td align="left">
                <asp:LinkButton ID="LinkButton2" runat="server" 
                    PostBackUrl="~/DpuAmoutAllotement.aspx">Amount Allotement</asp:LinkButton>
            </td>
            <td width="50%">
                <asp:LinkButton ID="LinkButton1" runat="server" 
                    PostBackUrl="~/DpuBankPaymentAllotment.aspx">Bank Payment Allotement</asp:LinkButton>
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

    <table  class="style103" cellspacing="0">
        <tr align="left">
            <td class="style4">


    <asp:Label ID="Plantname12" runat="server" Text="Ref Id" 
        style="font-family: serif"></asp:Label>

            </td>
            <td class="style4">
                        <asp:TextBox ID="txt_tid" runat="server" CssClass="tb10" Height="25px" 
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
     <asp:TextBox ID="Txt_Availamount" runat="server"  MaxLength="8"  onkeypress="javascript:return isNumber(event)" 
                                               CssClass="tb10" Height="25px" Width="143px" Enabled="False"></asp:TextBox>


                                              


    </div>
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
                    Visible="False" Width="33px"></asp:TextBox>
                <%--  <asp:Button ID="Savebtn" runat="server" BackColor="Green" BorderStyle="Double" xmlns:asp="#unknown"
                    Font-Bold="True" ForeColor="White" Height="26px"   onclick="Savebtn_Click" Style="height: 26px" Text="Save" OnClientClick ="javascript:validate()"    Width="50px" />
</asp:Button>--%>
                

                <asp:TextBox ID="txt_pcode" runat="server" Height="18px" 
                    Visible="False" Width="33px"></asp:TextBox>
                

                <asp:TextBox ID="fd" runat="server" Height="18px" Visible="False" Width="33px"></asp:TextBox>
                <asp:TextBox ID="txt_daat" runat="server" Enabled="False" Height="18px" 
                    Visible="False" Width="33px"></asp:TextBox>
                

            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <br />
            </td>
        </tr>
        </table>
    </center>
   
   </asp:Panel>

   
  

<asp:Panel ID="Panel3"  Width="40%" runat="server"    BorderWidth="1px">

   <center>

    <table  class="style103" cellspacing="0">
        <tr align="left">
            <td>


                <asp:Label ID="Plantname17" runat="server" style="font-family: serif" 
                    Text="Plant Name"></asp:Label>
            </td>
            <td>
                        <asp:TextBox ID="Txtplant" runat="server" CssClass="tb10" Enabled="False" 
                            Height="25px" MaxLength="8" onkeypress="javascript:return isNumber(event)" 
                            Width="143px"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td>
                                      <asp:Label ID="Plantname15" runat="server" style="font-family: serif" 
                                          Text="Balance Amount"></asp:Label>
            </td>
            <td>

                                       <div>


                                              


                                           <asp:TextBox ID="Txtbalance" runat="server" CssClass="tb10" Enabled="False" 
                                               Height="25px" MaxLength="8" onkeypress="javascript:return isNumber(event)" 
                                               Width="143px"></asp:TextBox>


                                              


    </div>
                                    </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="Plantname16" runat="server" style="font-family: serif" 
                    Text="Amount"></asp:Label>
            </td>
            <td>
                </asp:TextBox>
                <asp:TextBox ID="Txtamount" runat="server" CssClass="tb10" Enabled="true" 
                    Height="25px" MaxLength="8" onkeypress="javascript:return isNumber(event)" 
                    Width="143px"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <%--  <asp:Button ID="Savebtn" runat="server" BackColor="Green" BorderStyle="Double" xmlns:asp="#unknown"
                    Font-Bold="True" ForeColor="White" Height="26px"   onclick="Savebtn_Click" Style="height: 26px" Text="Save" OnClientClick ="javascript:validate()"    Width="50px" />
--%>
                <asp:Button ID="Savebtn1" runat="server" BackColor="Green" BorderStyle="Inset" 
                    Font-Bold="True" ForeColor="White" Height="26px" onclick="Savebtn1_Click" 
                    OnClientClick="javascript:Allvalidate()" text="Save" Width="60px" 
                    xmlns:asp="#unknown" />
                </asp:Button>
                

              <%--  <asp:Button ID="btn_delete" runat="server" BackColor="RED" BorderStyle="Inset" 
                    Font-Bold="True" ForeColor="White" Height="26px" onclick="btn_delete_Click" 
                    OnClientClick="return confirmationclose();" Style="height: 26px" text="Close" 
                    Width="60px" xmlns:asp="#unknown" />


                --%>

                  <asp:Button ID="btn_delete" runat="server" BackColor="RED" BorderStyle="Inset" 
                    Font-Bold="True" ForeColor="White" Height="26px" onclick="btn_delete_Click" 
                  Style="height: 26px" text="Close" 
                    Width="60px" xmlns:asp="#unknown" />
                

            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:Label ID="lbl_msg" runat="server" Text="Label"></asp:Label>
                <br />
            </td>
        </tr>
        </table>
    </center>
   
   </asp:Panel>

   
  

    <br />

   
  

    <br />
  </center>
</div>
<center>
  <asp:Panel ID="Panel2" runat="server"  Width="60%">
      <asp:GridView ID="GridView1" runat="server" CssClass="gridview1" 
          AutoGenerateColumns="False" Font-Size="10px" 
          onpageindexchanging="GridView1_PageIndexChanging" 
          onrowdatabound="GridView1_RowDataBound" PageSize="15" DataKeyNames="pcode" 
          onrowcancelingedit="GridView1_RowCancelingEdit" 
          onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
          onselectedindexchanged="GridView1_SelectedIndexChanged">
          <Columns>
              <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" 
                  ApplyFormatInEditMode="True" InsertVisible="False" >
              <ControlStyle Width="150px" />
              <FooterStyle Width="150px" />
              <HeaderStyle Width="150px" />
              <ItemStyle Width="150px" />
              </asp:BoundField>
              <asp:BoundField DataField="pcode" HeaderText="pcode" SortExpression="pcode" 
                  ApplyFormatInEditMode="True" />
              <asp:BoundField DataField="PlantName" HeaderText="PlantName" 
                  SortExpression="PlantName" ApplyFormatInEditMode="True" 
                  InsertVisible="False" />
              <asp:BoundField DataField="DpuAmount" HeaderText="DpuAmount" 
                  SortExpression="DpuAmount" />
            <%--  <asp:BoundField DataField="Amount" HeaderText="Amount" 
                  SortExpression="Amount" />--%>

                  <asp:BoundField DataField="CheckAmount" HeaderText="CheckAmount" 
                  SortExpression="CheckAmount" />
              <asp:CommandField ButtonType="Button" EditText="Check" 
                  ShowSelectButton="True" />
              
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



