<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EMPLOYEEMASTER.aspx.cs" Inherits="EMPLOYEEMASTER" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style20
        {
            height: 29px;
        }
        #table5
        {
            width: 32%;
        }
        #table6
        {
            width: 32%;
        }
        #table7
        {
            width: 32%;
        }
        #table3
        {
            width: 39%;
            color: #0099FF;
        }
        #table8
        {
            width: 39%;
            color: #0099FF;
        }
        .style44
        {
            width: 14%;
            font-family: Arial;
            font-size: small;
            color: #0066FF;
        }
        .style45
        {
            width: 14%;
            font-size: small;
            color: #0066FF;
        }
        .style46
        {
            font-size: small;
            font-family: Arial;
        }
        .style49
        {
            width: 799px;
            font-size: small;
            font-family: Arial;
            color: #0066FF;
        }
        .style50
        {
            width: 14%;
            font-family: Arial;
            font-size: small;
            height: 29px;
            color: #0066FF;
        }
        .style51
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: #0099FF;
            width: 12%;
            height: 29px;
        }
        .style52
        {
            width: 799px;
            height: 29px;
            color: #0066FF;
        }
        .style53
        {
            width: 154px;
            height: 29px;
            font-family: Arial;
            font-size: small;
            color: #0066FF;
        }
        .style54
        {
            font-family: Arial;
            font-size: xx-small;
        }
        .style55
        {
            width: 154px;
            font-family: Arial;
            font-size: small;
            color: #0066FF;
        }
        .style56
        {
            width: 799px;
            font-family: Arial;
            font-size: small;
        }
        .style57
        {
            width: 14%;
            font-family: Arial;
            font-size: small;
            height: 24px;
            color: #0066FF;
        }
        .style58
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: #0099FF;
            height: 24px;
        }
        .style59
        {
            font-size: small;
            font-family: Arial;
            height: 24px;
        }
        .style60
        {
            color: #0066FF;
        }
        .style61
        {
            font-size: small;
            font-family: Arial;
            color: #0066FF;
        }
        .style62
        {
            color: #996633;
        }
        .style63
        {
            color: #663300;
        }
        .style64
        {
            width: 54px;
            font-family: Arial;
            font-size: small;
        }
        .style65
        {
            width: 54px;
            font-size: small;
            font-family: Arial;
            color: #0066FF;
        }
        .style66
        {
            width: 54px;
            height: 29px;
            color: #0066FF;
        }
        .style67
        {
            font-size: small;
            font-family: Arial;
            height: 24px;
            width: 54px;
        }
        .style68
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: black;
        }
        .style69
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: #0099FF;
            height: 29px;
        }
        .style70
        {
            text-decoration: underline;
        }
        .style71
        {
            color: #663300;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
 <center> </center>
    <p>
        &nbsp;<span class="style62"><span class="style70"><strong>Employee Personal Details</strong></span>&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
            class="style62"><span class="style70"><strong>Employee</strong></span></span>
        <span class="style63"><span class="style70"><strong>Address details</strong></span>&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
            class="style62"><span class="style70"><strong>Employee</strong></span></span>&nbsp;<span 
            class="style71"><strong>Office details</strong></span>&nbsp;<table class="style1">
            <tr>
       
       <td class="style44">Employee Id</td>
       
       <td class="style68">
           <asp:TextBox ID="txt_fat10" runat="server"   Height="20px" 
               Width="162px" CssClass="style54" ></asp:TextBox>
           </td>
     
       
       <td class="style64" rowspan="3" colspan="0">
                </td>
       
       
       <td class="style56" rowspan="3">
           <br class="style60" />
           <br class="style60" />
           <span class="style60">Employee Address</span></td>
       
       <td class="altd1" rowspan="3">
           <asp:TextBox ID="txt_fat12" runat="server"   Height="86px" 
               Width="162px" CssClass="style54" ></asp:TextBox>
           </td>
     
       
                <td class="style55">
                    &nbsp;</td>
     
       
                <td class="style55">
                    DOJ</td>
                <td>
                              <asp:TextBox ID="txt_ToDate0" runat="server" Height="19px" 
                            Width="172px"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="txt_ToDate0_CalendarExtender" runat="server" TargetControlID="txt_ToDate0"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" 
                            PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                </td>
            </tr>
            <tr>
       
       <td class="style44">Employee Name</td>
       
       <td class="style68">
           <asp:TextBox ID="TextBox25" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>
     
       
                <td class="style55">
                    &nbsp;</td>
     
       
                <td class="style55">
                    Emp Designation&nbsp;&nbsp;</td>
                <td>
                        <asp:TextBox ID="dtp_DateTime11" runat="server" Height="20px" 
                            Width="175px" />
                </td>
            </tr>
            <tr>
       
       <td class="style44">Father&#39;s Name</td>
       
       <td class="style68">
           <asp:TextBox ID="TextBox26" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>
     
       
                <td class="style55">
                    &nbsp;</td>
     
       
                <td class="style55">
                    Emp Salary</td>
                <td>
                        <asp:TextBox ID="dtp_DateTime10" runat="server" Height="20px" 
                            Width="175px" />
                </td>
            </tr>
            <tr>
       
       <td class="style45">Spouse Name</td>
       
       <td class="style68">
           <asp:TextBox ID="txt_fat11" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>

     
       
       <td class="style65" colspan="0">&nbsp;</td>
       
     
       
       <td class="style49">Dstrict</td>
       
       <td class="altd1">
           <asp:TextBox ID="txt_fat13" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>

     
       
                <td class="style61">
                    &nbsp;</td>

     
       
                <td class="style61">
                    Department</td>
                <td>
                        <asp:TextBox ID="dtp_DateTime12" runat="server" Height="20px" 
                            Width="175px" />
                </td>
            </tr>
            <tr>
        <td class="style50">Dob</td>
       
       <td class="style69">
                              <asp:TextBox ID="txt_ToDate" runat="server" Height="19px" Width="158px"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
           </td>

     
       
        <td class="style66" colspan="0">&nbsp;</td>
       
     
       
        <td class="style52">State</td>
       
       <td class="style51">
                        <asp:DropDownList ID="DropDownList6" runat="server" Height="25px" 
               Width="162px">
                            <asp:ListItem Value="0">---------Select ------------</asp:ListItem>
                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                            <asp:ListItem>Assam</asp:ListItem>
                            <asp:ListItem>Bihar</asp:ListItem>
                            <asp:ListItem>Chhattisgarh</asp:ListItem>
                            <asp:ListItem>Goa</asp:ListItem>
                            <asp:ListItem>Gujarat</asp:ListItem>
                            <asp:ListItem>Haryana</asp:ListItem>
                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                            <asp:ListItem>Jharkhand</asp:ListItem>
                            <asp:ListItem>Karnataka</asp:ListItem>
                            <asp:ListItem>Kerala</asp:ListItem>
                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                            <asp:ListItem>Maharashtra</asp:ListItem>
                            <asp:ListItem>Manipur</asp:ListItem>
                            <asp:ListItem>Meghalaya</asp:ListItem>
                            <asp:ListItem>Mizoram</asp:ListItem>
                            <asp:ListItem>Nagaland</asp:ListItem>
                            <asp:ListItem>Odisha</asp:ListItem>
                            <asp:ListItem>Punjab</asp:ListItem>
                            <asp:ListItem>Rajasthan</asp:ListItem>
                            <asp:ListItem>Sikkim</asp:ListItem>
                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                            <asp:ListItem>Tripura</asp:ListItem>
                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                            <asp:ListItem>Uttarakhand</asp:ListItem>
                            <asp:ListItem>West Bengal</asp:ListItem>
                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                            <asp:ListItem>Chandigarh</asp:ListItem>
                            <asp:ListItem>Dadar and Nagar</asp:ListItem>
                            <asp:ListItem>Daman and Diu</asp:ListItem>
                            <asp:ListItem>Delhi</asp:ListItem>
                            <asp:ListItem>Lakshadweep</asp:ListItem>
                            <asp:ListItem>Pondicherry</asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
           </td>

     
       
                <td class="style53">
                    &nbsp;</td>

     
       
                <td class="style53">
                    Qualification</td>
                <td class="style20">
                        <asp:TextBox ID="dtp_DateTime13" runat="server" Height="20px" 
                            Width="175px" />
                </td>
            </tr>
            <tr>
     
       <td class="style44">Gender</td>
      
       <td  class="style68">
								<asp:RadioButton ID="auto2" runat="server" Checked="true" Text="Male" 
                                        AutoPostBack="True" 
               oncheckedchanged="auto_CheckedChanged" />
								<asp:RadioButton ID="manual2" runat="server" 
               Checked="false" Text="Female" 
                                        AutoPostBack="True" 
               oncheckedchanged="manual_CheckedChanged" />								
           </td>
      
       <td class="style65" colspan="0">&nbsp;</td>
      
       <td class="style49">Pincode</td>
      
       <td  class="altd1">
           <asp:TextBox ID="txt_fat14" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>
      
                <td class="style61">
                    &nbsp;</td>
      
                <td class="style61">
                    Emp A/c No</td>
                <td>
                        <asp:TextBox ID="dtp_DateTime14" runat="server" Height="20px" 
                            Width="175px" />
                </td>
            </tr>
            <tr>
       
       <td class="style44">Marital Status</td>
    
       <td  class="style68">
								<asp:RadioButton ID="auto3" runat="server" Checked="true" Text="Married" 
                                        AutoPostBack="True" 
               oncheckedchanged="auto3_CheckedChanged" />
								<asp:RadioButton ID="manual3" runat="server" 
               Checked="false" Text="UnMarried" 
                                        AutoPostBack="True" 
               oncheckedchanged="manual3_CheckedChanged" />								
           </td>
      
       <td class="style65" colspan="0">&nbsp;</td>
    
       <td class="style49">Phone</td>
    
       <td  class="altd1">
                        <asp:TextBox ID="dtp_DateTime8" runat="server" Height="20px" 
                            Width="162px" />
           </td>
      
                <td class="style61">
                    &nbsp;</td>
      
                <td class="style61">
                    Ref Name</td>
                <td>
                        <asp:TextBox ID="dtp_DateTime15" runat="server" Height="20px" 
                            Width="175px" />
                </td>
            </tr>
            <tr>
      
       <td class="style44">Blood Group</td>
      
       <td  class="style68">
                        <asp:DropDownList ID="cmb_sampleno6" runat="server" Height="20px" Width="162px">
                            <asp:ListItem Value="0">--------------Select -------------</asp:ListItem>
                            <asp:ListItem>A(+ve)</asp:ListItem>
                            <asp:ListItem>A(-ve)</asp:ListItem>
                            <asp:ListItem>B(+ve)</asp:ListItem>
                            <asp:ListItem>B(-ve)</asp:ListItem>
                            <asp:ListItem>AB(+ve)</asp:ListItem>
                            <asp:ListItem>AB(-ve)</asp:ListItem>
                            <asp:ListItem>O(+ve)</asp:ListItem>
                            <asp:ListItem>O(-ve)</asp:ListItem>
                        </asp:DropDownList>
           </td>
     
       <td class="style65" colspan="0">&nbsp;</td>
      
       <td class="style49">Mobile</td>
      
       <td  class="altd1">
								<asp:TextBox ID="txt_fat15" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>
     
                <td class="style46" rowspan="3">
                    &nbsp;</td>
     
                <td class="style46" colspan="2" rowspan="3">
                    <asp:Image ID="Image3" runat="server" Height="83px" Width="253px" 
                        CssClass="style60" style="text-align: right" /></td>
            </tr>
            <tr>
       
       <td class="style57">Mother Tongue</td>
       
       <td class="style58">
                        <asp:DropDownList ID="DropDownList5" runat="server" Height="25px" Width="162px">
                            <asp:ListItem Value="0">-------------Select ------------</asp:ListItem>
                            <asp:ListItem>English</asp:ListItem>
                            <asp:ListItem>Hindi</asp:ListItem>
                            <asp:ListItem>Telugu</asp:ListItem>
                            <asp:ListItem>Tamil</asp:ListItem>
                        </asp:DropDownList>
           </td>

           
     
       
       <td class="style67" colspan="0">
           &nbsp;</td>
    
       
      
       
       <td class="style59" colspan="2">
    <asp:FileUpload ID="Excel_FileUpload" runat="server" CssClass="style60" />
                </td>
    
       
      
            </tr>
            <tr>
       
       <td class="style44">Email Id</td>
       
       <td class="style68">
           <asp:TextBox ID="TextBox27" runat="server"   Height="20px" 
               Width="162px" ></asp:TextBox>
           </td>

           
     
       
                <td class="style65" colspan="0">
                    &nbsp;</td>

           
     
       
                <td class="style49">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </p>
   <%-- <div class="legmanual">--%>
     <%-- <fieldset class="fontt">
      <legend class="fontt">EMPLOYEE PERSONAL INFORMATION</legend>--%>
       
       
       
     <%--  </table>--%>

     <%-- </fieldset>
      
        </div>--%>
    <%--  
    <div class="legmanual">
      <fieldset class="fontt">
      <legend class="fontt">EMPLOYEE ADDRESS INFORMATION</legend>--%>

     
     <%-- </fieldset>
        </div>
      --%>
      <p>
 <center><asp:Button ID="btn_Ok" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="100px" Height="26px" onclick="btn_Ok_Click" />
           
 <asp:Button ID="btn_Ok0" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Exit" Width="100px" Height="26px" onclick="btn_Ok_Click" />
           </center>
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

