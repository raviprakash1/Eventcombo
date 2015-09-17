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