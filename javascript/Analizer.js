<script type="text/javascript">
var message="Sorry, right-click has been disabled";
///////////////////////////////////
function clickIE() {if (document.all) {(message);return false;}}
function clickNS(e) {if
(document.layers||(document.getElementById&&!document.all)) {
if (e.which==2||e.which==3) {(message);return false;}}}
if (document.layers)
{document.captureEvents(Event.MOUSEDOWN);document.onmousedown=clickNS;}
else{document.onmouseup=clickNS;document.oncontextmenu=clickIE;}
document.oncontextmenu=new Function("return false")

 function OpenActiveX()
{

	var port = document.getElementById("Comport");
    port.attachEvent('EventDataReceived1', EventDataReceived); 
    port.Port=d1.options[d1.selectedIndex].text;
    port.Baudrate=d2.options[d2.selectedIndex].text;
    port.DataBits=8;
    port.Parity=0; //NOPARITY
    port.StopBits=0; //one stop bit
    port.DataAnalizerMode=1;
    port.AnalizerPortConnecteed=1;
   if(port.error>0) //Display errors if found
       alert(port.ErrorDescription);
}
 function EventDataReceived(getvalue,getcount) 
 {
    if(getcount==0)
    {
    //text1.value=" ";
     text1.value=text1.value+getvalue;
     var hiddenControl = '<%= inpHide.ClientID %>';
      document.getElementById(hiddenControl).value=text1.value;  
    }          
 }




 

</script>