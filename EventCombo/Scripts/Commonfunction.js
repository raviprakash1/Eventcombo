function validateEmail(sEmail) {
    var filter = /^[\w\-\.\+]+\@@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;

    if (filter.test(sEmail)) {

        return true;
    }
    else {


        return false;
    }
}



function checknumeric(event) {

    var code = event.charCode ? event.charCode : event.keyCode;
  
    if ((!event.shiftKey && !event.ctrlKey && !event.altKey) && ((code >= 48 && code <= 57) || (code >= 96 && code <= 105))) // 0-9 or numpad 0-9, disallow shift, ctrl, and alt
    {

    }
    else if (code != 8 && code != 46 && code != 37 && code != 39 && code != 9 && code != 190) // not esc, del, left or right
    {
      return false;
    }

}