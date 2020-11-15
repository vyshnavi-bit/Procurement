<%@ Page Title="OnlineMilkTest|PlantReports" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminPlantReport.aspx.cs" Inherits="AdminPlantReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
            <ProgressTemplate >                             
                <div align="center" class="legendloadimg">
                 <img alt="progress" src="loading.gif" border="1" height="100px" width="100px"/>
                 Processing...                    
                 </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
    <div class="legagentsms">
        <fieldset class="fontt">       
            <legend style="color: #3399FF">All Plant Report </legend>
            <table id="table4" align="center" border="0" cellspacing="1" width="100%">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_FromDate" runat="server" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                            TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                            TargetControlID="txt_ToDate">
                                   </asp:CalendarExtender>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_ToDate" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td align="left">
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                            TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                        <asp:Button ID="btn_Ok" runat="server" BackColor="#6F696F" ForeColor="White" 
                             Text="OK" Width="50px" onclick="btn_Ok_Click" OnClientClick="return confirm('Are you sure you want to Generate reports?');"  />
                    </td>
                </tr>
                </table>
            <br />
        </fieldset>
    </div>
 
<div align="center" class="legagentPlant">  
<fieldset class="fontt">
  <legend >All Plants</legend>  
  
<table border="0" cellpadding="0" cellspacing="1" width="100%"> 
     <tr>
  <td >
          <div >
       <fieldset class="fontt">  
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">155_Arani</legend>   
 
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td align="left" > 
       
     <input id="text1" type="text" size="10" maxlength="10"  value="<%=Milkkg%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;  " />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text2" type="text" size="10" maxlength="10"  value="<%=Fat%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text3" type="text" size="10" maxlength="10"  value="<%=Snf%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk   
    </td>
    <td >     
     <input id="text4" type="text" size="10" maxlength="10"  value="<%=MAmt%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text5" type="text" size="10" maxlength="10"  value="<%=Rate%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>
  <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">156_Kaveri</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text6" type="text" size="10" maxlength="10"  value="<%=Milkkg1%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text7" type="text" size="10" maxlength="10" value="<%=Fat1%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text8" type="text" size="10" maxlength="10" value="<%=Snf1%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text9" type="text" size="10" maxlength="10" value="<%=MAmt2%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text10" type="text" size="10" maxlength="10" value="<%=Rate1%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>
  <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">157_Gudlur</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text11" type="text" size="10" maxlength="10"  value="<%=Milkkg2%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text12" type="text" size="10" maxlength="10"  value="<%=Fat2%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text13" type="text"  size="10" maxlength="10" value="<%=Snf2%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >      
     <input id="text14" type="text"  size="10" maxlength="10" value="<%=MAmt%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text15" type="text" size="10" maxlength="10"  value="<%=Rate2%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>
  <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">158_Walaja</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text16" type="text" size="10" maxlength="10"  value="<%=Milkkg3%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text17" type="text" size="10" maxlength="10" value="<%=Fat3%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text18" type="text" size="10" maxlength="10"  value="<%=Snf3%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text19" type="text" size="10" maxlength="10"  value="<%=MAmt3%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text20" type="text" size="10" maxlength="10" value="<%=Rate3%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>
  <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">159_Vkota</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text21" type="text" size="10" maxlength="10"  value="<%=Milkkg4%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text22" type="text" size="10" maxlength="10" value="<%=Fat4%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text23" type="text" size="10" maxlength="10"  value="<%=Snf4%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text24" type="text" size="10" maxlength="10"  value="<%=MAmt4%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text25" type="text" size="10" maxlength="10" value="<%=Rate4%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>        
    </tr>
  

  <tr>
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">160_Pallipattu</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text26" type="text" size="10" maxlength="10"  value="<%=Milkkg5%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text27" type="text" size="10" maxlength="10" value="<%=Fat5%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text28" type="text" size="10" maxlength="10"  value="<%=Snf5%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text29" type="text" size="10" maxlength="10"  value="<%=MAmt5%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text30" type="text" size="10" maxlength="10" value="<%=Rate4%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>   
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">161_Rcpuram</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text31" type="text" size="10" maxlength="10"  value="<%=Milkkg6%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text32" type="text" size="10" maxlength="10" value="<%=Fat6%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text33" type="text" size="10" maxlength="10"  value="<%=Snf6%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text34" type="text" size="10" maxlength="10"  value="<%=MAmt6%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text35" type="text" size="10" maxlength="10" value="<%=Rate6%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>   
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">162_Bomma</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text36" type="text" size="10" maxlength="10"  value="<%=Milkkg7%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text37" type="text" size="10" maxlength="10" value="<%=Fat7%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text38" type="text" size="10" maxlength="10"  value="<%=Snf7%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text39" type="text" size="10" maxlength="10"  value="<%=MAmt7%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text40" type="text" size="10" maxlength="10" value="<%=Rate7%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td> 
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">163_Tari</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text41" type="text" size="10" maxlength="10"  value="<%=Milkkg8%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text42" type="text" size="10" maxlength="10" value="<%=Fat8%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text43" type="text" size="10" maxlength="10"  value="<%=Snf8%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text44" type="text" size="10" maxlength="10"  value="<%=MAmt8%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text45" type="text" size="10" maxlength="10" value="<%=Rate8%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">164_Kalas</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text46" type="text" size="10" maxlength="10"  value="<%=Milkkg9%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text47" type="text" size="10" maxlength="10" value="<%=Fat9%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text48" type="text" size="10" maxlength="10"  value="<%=Snf9%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text49" type="text" size="10" maxlength="10"  value="<%=MAmt9%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text50" type="text" size="10" maxlength="10" value="<%=Rate9%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td>      
    </tr>
    <tr>
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">165_CSpuram</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text51" type="text" size="10" maxlength="10"  value="<%=Milkkg10%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text52" type="text" size="10" maxlength="10" value="<%=Fat10%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text53" type="text" size="10" maxlength="10"  value="<%=Snf10%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text54" type="text" size="10" maxlength="10"  value="<%=MAmt10%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text55" type="text" size="10" maxlength="10" value="<%=Rate10%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td> 
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">166_Kondepi</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text56" type="text" size="10" maxlength="10"  value="<%=Milkkg11%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text57" type="text" size="10" maxlength="10" value="<%=Fat11%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text58" type="text" size="10" maxlength="10"  value="<%=Snf11%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text59" type="text" size="10" maxlength="10"  value="<%=MAmt11%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text60" type="text" size="10" maxlength="10" value="<%=Rate11%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td> 
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">167_Kavali</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text61" type="text" size="10" maxlength="10"  value="<%=Milkkg12%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text62" type="text" size="10" maxlength="10" value="<%=Fat12%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text63" type="text" size="10" maxlength="10"  value="<%=Snf12%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text64" type="text" size="10" maxlength="10"  value="<%=MAmt12%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text65" type="text" size="10" maxlength="10" value="<%=Rate12%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td> 
    <td >
         <div >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">168_Gudipalli</legend>
<table >
    <tr>    
     <td >    
         MilkKg    
    </td>      
    <td >    
     <input id="text66" type="text" size="10" maxlength="10"  value="<%=Milkkg13%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>
    </tr>
      <tr> 
     <td >    
         Fat   
    </td>
    <td >     
     <input id="text67" type="text" size="10" maxlength="10" value="<%=Fat13%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Snf   
    </td>
    <td >     
     <input id="text68" type="text" size="10" maxlength="10"  value="<%=Snf13%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
         Milk    
    </td>
    <td >     
     <input id="text69" type="text" size="10" maxlength="10"  value="<%=MAmt13%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Green;  font-weight:bold;" />
    </td>    
     </tr>
      <tr> 
     <td >    
        Rate   
    </td>
    <td >      
     <input id="text70" type="text" size="10" maxlength="10" value="<%=Rate13%>" readonly="readonly" style="font-family: Arial, Helvetica, sans-serif; color:Red;  font-weight:bold;" />
    </td>    
     </tr>
    </table>    
     </fieldset>
   </div>    
          </td> 
    </tr>

    </table>                
    </fieldset>     
</div>
</ContentTemplate>
</asp:UpdatePanel>        
<br />
</asp:Content>

