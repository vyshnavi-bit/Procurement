<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Gallery.aspx.cs" Inherits="Gallery" Title="OnlineMilkTest|Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <link rel="stylesheet" href="Gallery/stylesheet/vijay.css" />

    <script type="text/javascript" language="javascript" src="Gallery/script/vijay.js"></script>

    <link rel="stylesheet" href="Gallery/style.css" type="text/css" />

    <script type="text/javascript" src="Gallery/script.js"></script>

    <script type="text/javascript" src="Gallery/highslide/highslide-with-gallery.js"></script>

    <link rel="stylesheet" type="text/css" href="highslide/highslide.css" />

   

    <script type="text/javascript">
        hs.graphicsDir = 'highslide/graphics/';
        hs.align = 'center';
        hs.transitions = ['expand', 'crossfade'];
        hs.outlineType = 'rounded-white';
        hs.fadeInOut = true;
        //hs.dimmingOpacity = 0.75;

        // Add the controlbar
        hs.addSlideshow({
            //slideshowGroup: 'group1',
            interval: 5000,
            repeat: false,
            useControls: true,
            fixedControls: 'fit',
            overlayOptions: {
                opacity: 0.75,
                position: 'bottom center',
                hideOnMouseOut: true
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="bg3">
        <div class="part3">
            <div style="width: 830px; height: 450px; float: left;" id="rightcolumn">
                <div style="width: 830px; height: 30px;">
                </div>
                <div style="width: 830px; height: 30px; font-family: Tahoma, Geneva, sans-serif;
                    font-size: 22px; color: #666; text-align: center; font-style: normal;" align="center">
                     Gallery</div>
                <div style="width: 830px; height: 360px; margin-top: 30px; margin-left: 30px;">
                    <div class="highslide-gallery">
                        <a href="Gallery/admin/uploaded/1.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/1.jpg' width='97' height='93' title='Click to Enlarge'  alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/2.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/2.jpg' width='97' height='93' title='Click to Enlarge' alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/3.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/3.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/4.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/4.jpg' width='97' height='93' title='Click to Enlarge' alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/5.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/5.jpg' width='97' height='93' title='Click to Enlarge' alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/6.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/6.jpg' width='97' height='93' title='Click to Enlarge'alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/7.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/7.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        
                        <a href="Gallery/admin/uploaded/9.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/9.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/10.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/10.jpg' width='97' height='93' title='Click to Enlarge'  alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/11.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/11.jpg' width='97' height='93' title='Click to Enlarge'  alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/12.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/12.jpg' width='97' height='93' title='Click to Enlarge'  alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/13.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/13.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/14.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/14.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                        <a href="Gallery/admin/uploaded/15.jpg" class="highslide" onclick="return hs.expand(this)"
                            style="float: left;">
                            <img src='Gallery/admin/uploaded/15.jpg' width='97' height='93' title='Click to Enlarge'  alt=""/></a>
                        <div style="width: 17px; height: 110px; float: left;">
                        </div>
                    
                    <a href="Gallery/admin/uploaded/16.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/16.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    <a href="Gallery/admin/uploaded/17.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/17.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    <a href="Gallery/admin/uploaded/18.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/18.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    <a href="Gallery/admin/uploaded/19.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/19.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    <a href="Gallery/admin/uploaded/20.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/20.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    <a href="Gallery/admin/uploaded/21.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/21.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    <a href="Gallery/admin/uploaded/22.jpg" class="highslide" onclick="return hs.expand(this)"
                        style="float: left;">
                        <img src='Gallery/admin/uploaded/22.jpg' width='97' height='93' title='Click to Enlarge' alt="" /></a>
                    <div style="width: 17px; height: 110px; float: left;">
                    </div>
                    
                    </div></div>
            </div>
        </div>
    </div>
    </asp:Content>
                 