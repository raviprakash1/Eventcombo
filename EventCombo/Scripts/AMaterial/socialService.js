﻿eventComboApp.service('socialService', ['$window',
  function ($window) {
    var facebookShare = function (myhref) {
      FB.ui({
        method: 'share',
        href: myhref
      }, function (response) { });
      return false;
    }

    var facebookSend = function (myhref) {
      FB.ui({
        method: 'send',
        link: myhref
      }, function (response) { });
      return false;
    }

    var twitterShare = function (title, href) {
      $window.open('https://twitter.com/intent/tweet?url=' + encodeURIComponent(href) + '&via=Eventcombo&related=Eventcombo&text=' + title + '', 'targetWindow', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=626,height=436');
      return false;
    }

    var linkedInShare = function (title, href, summary) {
      $window.open('https://www.linkedin.com/shareArticle?mini=true&url=' + encodeURIComponent(href) + '&title=' + title + '&summary=' + summary + '&source=EventCombo', 'targetWindow', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=626,height=436');
      return false;
    }

    return {
      FacebookShare: facebookShare,
      FacebookSend: facebookSend,
      TwitterShare: twitterShare,
      LinkedInShare: linkedInShare
    }
  }]);

