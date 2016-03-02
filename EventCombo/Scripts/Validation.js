function validateEmail(sEmail) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;



    if (reg.test(sEmail) == false) {
        
        return false;
    }

    return true;
}

function returnleap(yr) {
    return (yr % 400) ? ((yr % 100) ? ((yr % 4) ? false : true) : false) : true;
}

