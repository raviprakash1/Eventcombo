$(document).ready(function () {
    $('#accordion .panel-heading').click(function () {
        if (!$(this).hasClass('active')) {
            // the element clicked was not active, but now is - 
            $('.panel-heading').removeClass('active');
            $(this).addClass('active');
            setIconOpened(this);
        }
        else {
            // active panel was reclicked
            if ($(this).find('b').hasClass('opened')) {
                setIconOpened(null);
            }
            else {
                setIconOpened(this);
            }
        }
    });
});
// create a function to set the open icon for the given panel
// clearing out all the rest (activePanel CAN be null if nothing is open)
function setIconOpened(activePanel) {
  $('.panel-heading').find('b').addClass('closed').removeClass('opened'); 

  if (activePanel)
  {
    $(activePanel).find('b').addClass('opened').removeClass('closed'); 
  }
}



// Manage events search box code below
// This code for when u click on search input the button bg/color chagne
$(function () {
    $("body").on("input propertychange", ".search-label-form-group", function (e) {
        $(this).toggleClass("search-label-form-group-with-value", !!$(e.target).val());
    }).on("focus", ".search-label-form-group", function () {
        $(this).addClass("search-label-form-group-with-focus");
    }).on("blur", ".search-label-form-group", function () {
        $(this).removeClass("search-label-form-group-with-focus");
    });
});


// Manage events tab panel code below
// This funtion for change tab panel
$(document).ready(function () {
    $("#myTab a").click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
});

