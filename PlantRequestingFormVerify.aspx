<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantRequestingFormVerify.aspx.cs" Inherits="PlantRequestingFormVerify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
    <style type="text/css">
.body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
        .style1
        {
            width: 100%;
            height: 84px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


   <form id="form1" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
       
            <asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" runat="server" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div align="center">








  

    <table>
    
    <tr>
    
    <td>
    
  
                                               <asp:GridView ID="GridView1" runat="server" 
                               CssClass="gridcls" Font-Bold="True" 
                                                   ForeColor="White" onrowcreated="GridView1_RowCreated" 
                                                   onrowdatabound="GridView1_RowDataBound" Width="100%" 
                               Font-Size="12px" AutoGenerateColumns="False">
                                                   <EditRowStyle BackColor="#999999" />
                                                   <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                                       Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                   <Columns>
                                                   

                                                    
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								 
                                                       <asp:BoundField DataField="RefId" HeaderText="RefId" SortExpression="RefId" />
								 
                                                       <asp:BoundField DataField="Plant_Code" HeaderText="PlantCode" 
                                                           SortExpression="Plant_Code" />
                                                       <asp:BoundField DataField="Plant_Name" HeaderText="PlantName" 
                                                           SortExpression="Plant_Name" />
                                                       <asp:BoundField DataField="Dept" HeaderText="Department" 
                                                           SortExpression="Dept" />
                                                       <asp:BoundField DataField="remarks" HeaderText="Remarks" 
                                                           SortExpression="remarks" />
                                                       <asp:BoundField DataField="RequesterDate" HeaderText="RequesterDate" 
                                                           SortExpression="RequesterDate" />
                                                       <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                                       <asp:BoundField DataField="LoginUser" HeaderText="LoginUser" 
                                                           SortExpression="LoginUser" />
                                                       <asp:BoundField DataField="Requester" HeaderText="Requester" 
                                                           SortExpression="Requester" />
								 
                                  <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                                   
                                                   
                                                   </Columns>
                                               </asp:GridView>
    
    
    </td>
    



        
 
</tr>

</table>
</div>

<table width=100%  >
        <tr width="100%" align=center>
            <td align="CENTER" >
           
                       <asp:Button ID="btn_Save" runat="server" CssClass="form93" Font-Bold="False" 
                           Font-Size="X-Small" onclick="btn_Save_Click" TabIndex="6" Text="Save" 
                           xmlns:asp="#unknown" />
            </td>
            
        </tr>
        <tr align="center" width="100%">
            <td align="CENTER">
                <label for="field3">
                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                    CLASS="field4" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                    Visible="False" Width="160px">
                </asp:DropDownList>
                </label>
            </td>
        </tr>
        </table>
 
</div>


    </tr>
    
    </table>


        </div>
</ContentTemplate>


<Triggers>
    <asp:PostBackTrigger ControlID="btn_Save" />
</Triggers>
</asp:UpdatePanel>
  
    </form>
            
            
            
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

