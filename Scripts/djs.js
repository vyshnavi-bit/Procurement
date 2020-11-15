
 function CallMe(src,dest)
 {     
     var ctrl = document.getElementById(src);
     // call server side method
     PageMethods.GetAgentName(ctrl.value, CallSuccess, CallFailed, dest);
 
 }


 // set the destination textbox value with the ContactName
 function CallSuccess(res, destCtrl)
 {     
     var dest = document.getElementById(destCtrl);
     dest.value = res;
 }

 // alert message on some failure
 function CallFailed(res, destCtrl)
 {
     alert(res.get_message());
 }
 function CallMe1(src,dest)
 {     
   var ctrl = document.getElementById(src);
     // call server side method
     PageMethods.LoadData(ctrl.value, CallSuccess, CallFailed, dest);

 }



//function CallMe(src, dest) {
//    var ctrl = document.getElementById(src);
//    // call server side method
//    PageMethods.GetStateName(ctrl.value, CallSuccess, CallFailed, dest);

//}

//// set the destination textbox value with the ContactName
//function CallSuccess(res, destCtrl) {
//    var dest = document.getElementById(destCtrl);
//    dest.value = res;
//}

//// alert message on some failure
//function CallFailed(res, destCtrl) {
//    alert(res.get_message());
//}
//function CallMe1(src, dest) {
//    var ctrl = document.getElementById(src);
//    // call server side method
//    PageMethods.LoadData(ctrl.value, CallSuccess, CallFailed, dest);

//}