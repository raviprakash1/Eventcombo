
function FacebookShare(myhref) {
  FB.ui({
    method: 'share',
    href: myhref,
  }, function (response) { });
  return false;
}

function TwitterShare(title, href) {
  window.open('https://twitter.com/intent/tweet?url=' + encodeURIComponent(href) + '&via=Eventcombo&related=Eventcombo&text=' + title + '', 'targetWindow', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=626,height=436');
  return false;
}

function LinkedInShare(title, href, summary) {
  window.open('https://www.linkedin.com/shareArticle?mini=true&url=' + encodeURIComponent(href) + '&title=' + title + '&summary=' + summary + '&source=EventCombo', 'targetWindow', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=626,height=436');
  return false;
}
