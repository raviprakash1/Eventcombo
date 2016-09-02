eventComboApp.service('broadcastService', ['$rootScope',
  function ($rootScope) {
    var broadcastMessage = function (message, param) {
      $rootScope.$broadcast(message, param);
    }

    var eventInfoLoaded = function (param) {
      broadcastMessage('EventInfoLoaded', param);
    }

    var errorExternalLogin = function (param) {
      broadcastMessage('ErrorExternalLogin', param);
    }

    var callLogin = function (param) {
      broadcastMessage('CallLogin', param);
    }

    var completeExternalLogin = function (param) {
      broadcastMessage('CompleteExternalLogin', param);
    }

    var loggedIn = function (param) {
      broadcastMessage('LoggedIn', param);
    }

    var loginProcessed = function (param) {
      broadcastMessage('LoginProcessed', param);
    }

    return {
      EventInfoLoaded: eventInfoLoaded,
      ErrorExternalLogin: errorExternalLogin,
      CallLogin: callLogin,
      CompleteExternalLogin: completeExternalLogin,
      LoggedIn: loggedIn,
      LoginProcessed: loginProcessed
    }
  }]);