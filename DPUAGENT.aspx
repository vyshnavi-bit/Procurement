<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DPUAGENT.aspx.cs" Inherits="DPUAGENT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
    {
            color: #333300;
            font-family: serif;
        }
        .style4
        {
            height: 24px;
        }
                
             .style104
        {
            width:100%;
            
        }
             .style106
        {
            width: 155px;
        }
        
        .style103
        {
            width: 313px;
        }
        
    </style>
    <script language="javascript" type="text/javascript">
//        function validate() {
//            var summary = "";
//            summary += isvalidFirstname();
//            summary += isvalidphoneno();
//            if (summary != "") {
//                alert(summary);
//                return false;
//            }
//            else {
//                return true;
//            }

//        }
//        function isvalidphoneno() {

//            var uid;
//            var temp = document.getElementById("<%=txt_magentname.ClientID %>");
//            uid = temp.value;
//            var re;
//            re = /^[0-9]+$/;
//            var digits = /\d(10)/;
//            if (uid == "") {
//                return ("Please enter phoneno" + "\n");
//            }
//            else if (re.test(uid)) {
//                return "";
//            }

//            else {
//                return ("Phoneno should be digits only" + "\n");
//            }
//        }
//        function isvalidFirstname() {
//            var uid;
//            var temp = document.getElementById("<%=txt_magentname.ClientID %>");
//            uid = temp.value;
//            var re = /^[a-zA-Z.]+$/
//            if (uid == "") {
//                return ("Please enter firstname" + "\n");
//            }
//            else if (re.test(uid)) {
//                return "";

//            }
//            else {
//                return ("FirstName accepts Characters,dots and spaces only" + "\n");
//            }
//        }
















        //        function validate()
        //      {
        //         var summary = "";
        //         summary += isproducername();
        //         summary += isvalidphoneno()
        ////         summary += isddecimalcart();
        ////         summary += isddecimalspl(); 
        //         if (summary != "")
        //          {
        //             alert(summary);
        //             return false;
        //         }
        //         else {
        //             return true;
        //         }

        //     }


        function confirmation() {
            if (confirm('are you  want to Update ?')) {
                return true;
            }
            else {
                return false;
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


        //     function isproducername()
        //      {

        //         var uid;
        //       
        //         uid = temp.value;
        //         if (uid == "") {
        //             return ("Please enter Your Name" + "\n");
        //         }

        //     }

        //     function isproducername()
        //      {
        //         var uid;
        //         var temp = document.getElementById("<%=txt_magentname.ClientID %>");
        //         uid = temp.value;
        //         if (uid == "") {
        //             return ("Please Enter UserName" + "\n");
        //         }
        //         else {
        //             return "";
        //         }
        //     }


        //     function isvalidphoneno() {

        //         var uid;
        //         var temp = document.getElementById("<%=txt_magentmobno.ClientID %>");
        //         uid = temp.value;
        //         var re;
        //         re = /^[0-9]+$/;
        //         var digits = /\d(10)/;
        //         if (uid == "") {
        //             return ("Please enter phoneno" + "\n");
        //         }
        //         else if (re.test(uid)) {
        //             return "";
        //         }

        //         else {
        //             return ("Phoneno should be digits only" + "\n");
        //         }
        //     }






        //     function isddecimalcart()
        //      {
        //         // regular expression
        //         var rgexp = new RegExp("^\d*([.]\d{1})?$");
        //         var input = document.getElementById("<%=txt_mcartage1.ClientID %>").value; 

        //         if (input.match(rgexp))
        //             return "";
        //         else
        //             return ("Should be decimal" + "\n");
        //     }


        //     function isddecimalspl() 
        //     {
        //         // regular expression
        //         var rgexp = new RegExp("^\d*([.]\d{1})?$");
        //         var input = document.getElementById("<%=txt_mcartage.ClientID %>").value;

        //         if (input.match(rgexp))
        //             return "";
        //         else
        //             return ("Should be decimal" + "\n");
        //     }
        //    
 




    </script>




    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<form>
<body>
<html>
    <div>
<center>


    <br />


    <asp:Label ID="Label1" runat="server" Text="DPU AGENT MASTER" 
        style="font-family: serif"></asp:Label>

    <br />

</center>

</div>
<div  align="center">

<center style="border-style: none">
<asp:Panel ID="Panel1"  Width="49%" runat="server"    BorderWidth="2px">

   <center>

    <table  class="style103" cellspacing="2">
        <tr align="left">
            <td width=50%>


    <asp:Label ID="Plantname" runat="server" Text="Plant Name" 
        style="font-family: serif"></asp:Label>

            </td>
            <td>
                        <asp:DropDownList ID="ddl_Plantname" runat="server" TabIndex="2" Width="149px" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" CssClass="tb10" 
                            Font-Size="12px">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr align="left">
            <td class="style4">


    <asp:Label ID="Plantname0" runat="server" Text="Agent Id" 
        style="font-family: serif"></asp:Label>

            </td>
            <td class="style4">
                        <asp:DropDownList ID="ddl_agentid" runat="server" TabIndex="2" Width="149px" 
                            AutoPostBack="True" 
                    onselectedindexchanged="ddl_agentid_SelectedIndexChanged" CssClass="tb10" 
                            Font-Size="12px">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr align="left">
            <td>


    <asp:Label ID="Plantname1" runat="server" Text="Producer Id" 
        style="font-family: serif"></asp:Label>

            </td>
            <td>
                        <asp:DropDownList ID="ddl_producerdetails" runat="server" TabIndex="2" Width="149px" 
                            AutoPostBack="True" CssClass="tb10" Font-Size="12px">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr align="left">
            <td>
                                      &nbsp;</td>
            <td>
                                    <asp:Button ID="btn_Insert" runat="server" Text="Show" 
                    BackColor="Green" BorderStyle="Double"
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="50px" Style="height: 26px"
                                        OnClick="btn_Insert_Click" />
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" TabIndex="1" Width="20px"                           
                            AutoPostBack="True" ReadOnly="true" Visible="False" Height="16px">
                        </asp:DropDownList>
                                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                    </td>
        </tr>
        </table>
    </center>
   
   </asp:Panel>


    <br />
  </center>
</div>


<div  align="center">
<center>
    <asp:Panel ID="Panel2"  Width="49%" runat="server" 
        BorderWidth="2px">
   

  

    <table  class="style103" cellspacing="2">
        <tr align="left">
            <td width="50%">
                <asp:Label ID="Label2" runat="server" style="font-family: serif" 
                    Text="Mil Nature"></asp:Label>
            </td>
            <td class="style106">
                <asp:RadioButton ID="rbcow" runat="server" AutoPostBack="True" Checked="True" 
                    CssClass="style3" Font-Bold="True" ForeColor="#333300" 
                    oncheckedchanged="rbcow_CheckedChanged" Text="Cow" />
                <asp:RadioButton ID="rbbuff" runat="server" AutoPostBack="True" 
                    CssClass="style3" Font-Bold="True" ForeColor="Black" 
                    oncheckedchanged="rbbuff_CheckedChanged" Text="Buffalo" />
            </td>
        </tr>
        <tr>
            <td align="left">


    <asp:Label ID="Plantname2" runat="server" Text="Producer Id" 
        style="font-family: serif"></asp:Label>

            </td>
            <td class="style106">
                        <asp:DropDownList ID="droproducer" runat="server" TabIndex="2" Width="149px" 
                            AutoPostBack="True" 
                    onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" CssClass="tb10" 
                            Font-Size="12px">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td align="left">


    <asp:Label ID="Label11" runat="server" Text="Producer Name" 
        style="font-family: serif"></asp:Label>

            </td>
            <td align="left" class="style106">
                        <asp:TextBox ID="txt_magentname" runat="server" TabIndex="4" Width="149px" 
                            CssClass="tb10" Font-Size="12px" />
                    </td>
        </tr>
        <tr>
            <td align="left">


    <asp:Label ID="Label4" runat="server" Text="Mobile" 
        style="font-family: serif"></asp:Label>

            </td>
            <td align="left" class="style106">
                        <asp:TextBox ID="txt_magentmobno" runat="server" Width="142px" 
                    TabIndex="5" CssClass="tb10" Font-Size="12px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td align="left">


    <asp:Label ID="Label10" runat="server" Text="Cartage" 
        style="font-family: serif"></asp:Label>

            </td>
            <td align="left" class="style106">
                        <asp:TextBox ID="txt_mcartage1" runat="server" TabIndex="7" 
                    Width="80px" CssClass="tb10" Font-Size="12px" >0.0</asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td align="left">


    <asp:Label ID="Label5" runat="server" Text="Spl Bonus" 
        style="font-family: serif"></asp:Label>

            </td>
            <td align="left" class="style106">
                        <asp:TextBox ID="txt_mcartage" runat="server" TabIndex="7" 
                    Width="80px" CssClass="tb10" Font-Size="12px" >0.0</asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td align="left">


    <asp:Label ID="Label12" runat="server" Text="Account Holder" 
        style="font-family: serif"></asp:Label>

            </td>
            <td align="left" class="style106">
                        <asp:CheckBox ID="Chk_BankAccount" runat="server" Text="Account" 
                            AutoPostBack="True" Checked="True" 
                            oncheckedchanged="Chk_BankAccount_CheckedChanged" 
                            style="font-family: Andalus"/>
                        </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label19" runat="server" style="font-family: serif" 
                    Text="Select Bank"></asp:Label>
            </td>
            <td align="left" class="style106">
                <asp:DropDownList ID="ddl_BankName" runat="server" AutoPostBack="True" 
                    CssClass="tb10" Font-Size="12px" 
                    onselectedindexchanged="ddl_BankName_SelectedIndexChanged" TabIndex="8" 
                    Width="149px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label20" runat="server" style="font-family: serif" 
                    Text="Select Branch"></asp:Label>
            </td>
            <td align="left" class="style106">
                <asp:DropDownList ID="ddl_branchname" runat="server" AutoPostBack="True" 
                    CssClass="tb10" Font-Size="12px" 
                    onselectedindexchanged="ddl_branchname_SelectedIndexChanged" Width="149px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label21" runat="server" style="font-family: serif" 
                    Text="Ifsc Code"></asp:Label>
            </td>
            <td align="left" class="style106">
                <asp:DropDownList ID="ddl_Ifsc" runat="server" AutoPostBack="True" 
                    CssClass="tb10" Font-Size="12px" 
                    onselectedindexchanged="ddl_Ifsc_SelectedIndexChanged" TabIndex="9" 
                    Width="149px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label22" runat="server" style="font-family: serif" 
                    Text="Agent A/c No"></asp:Label>
            </td>
            <td align="left" class="style106">
                <asp:TextBox ID="txt_AgentAccNo" runat="server" CssClass="tb10" 
                    Font-Size="12px" TabIndex="13" Width="149px" />
            </td>
        </tr>
        </table>
   
    <center>
      

    <table class="style104" cellspacing="2">
        <tr align="center"  >
            <td  width=50%>
                        <asp:DropDownList ID="ddl_bankid" runat="server" TabIndex="1" Width="20px"                           
                            AutoPostBack="True" ReadOnly="true" Visible="False" Height="16px">
                        </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList5" runat="server" 
                            AutoPostBack="True" Height="16px" ReadOnly="true" TabIndex="1" Visible="False" 
                            Width="20px">
                        </asp:DropDownList>
                                    <asp:Button ID="Button5" runat="server" Text="Edit" 
                    BackColor="Green" BorderStyle="Double"
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="50px" Style="height: 26px"
                                        OnClick="Button5_Click" />
                                 <%--   <asp:Button ID="update" runat="server" Text="update" 
                    BackColor="Green" BorderStyle="Inset"    
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="60px" Style="height: 26px"    onclick="update_Click" />--%>


            <%--     <asp:Button ID="update" runat="server" text="update" xmlns:asp="#unknown"       OnClientClick="return confirmation();" onclick="update _Click"/></asp:Button>--%>
            <asp:button id="update" runat="server" text="update" xmlns:asp="#unknown"   BackColor="Green" BorderStyle="Inset"    
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="60px" Style="height: 26px"

OnClientClick="return confirmationclose();" onclick="update_Click"/></asp:button>








                     <%--                  
                        <asp:Button ID="btn_delete" runat="server" BackColor="Red" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="White" Height="26px" Style="height: 26px" 
                            Text="Close" Width="50px" onclick="btn_delete_Click" />--%>


                            <asp:Button id="btn_delete" runat="server" text="Close" xmlns:asp="#unknown"     BackColor="RED" BorderStyle="Inset"     Font-Bold="True" ForeColor="White" Height="26px"      Width="60px" Style="height: 26px" OnClientClick="return confirmationclose();" onclick="btn_delete_Click"/></asp:Button>




                        <br />
                        <asp:Label ID="lblmsg" runat="server" style="font-family: Andalus" Text="Label"></asp:Label>
                    </td>
        </tr>
    </table>

   </center>


  
   </asp:Panel>
   </center>
    <br />
    <br />
  
</div>
</form>
</body>
</html>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

