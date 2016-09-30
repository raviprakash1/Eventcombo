eventComboApp.service('accountService', ['$rootScope', '$http', 'broadcastService', 
  function ($rootScope, $http, broadcastService) {

    var userNotRegistered = true;
    var userId = '';
    var userName = 'Log In / Sign Up';

    var updateLoginInfo = function () {
      $http.get('/account/getstatus').then(function (response) {
        if (response.data) {
          userId = response.data.BaseUserId;
          userNotRegistered = !userId;
          userName = userNotRegistered ? 'Log In / Sign Up' : response.data.BaseUserName ? response.data.BaseUserName : response.data.BaseUserEmail;
        }
      }, function (error) {
        userNotRegistered = true;
        userId = '';
        userName = 'Log In / Sign Up';
      });
    };

    updateLoginInfo();

    $rootScope.$on('LoggedIn', function (event, mess) {
      if (!mess)
        broadcastService.reloadPage();
      else
        updateLoginInfo();
    });

    var startLogin = function (param) {
      broadcastService.CallLogin(param);
    };

    var getUserRegistered = function () {
      return !userNotRegistered;
    }

    var getUserId = function () {
      return userId;
    }

    var getUserName = function () {
      return userName;
    }

    return {
      UserRegistered: getUserRegistered,
      UserId: getUserId,
      UserName: getUserName,
      StartLogin: startLogin
    }
  }]);