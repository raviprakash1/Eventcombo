﻿eventComboApp.service('broadcastService', ['$rootScope',
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

    var loggedOut = function (param) {
      broadcastMessage('LoggedOut', param);
    }

    var loginProcessed = function (param) {
      broadcastMessage('LoginProcessed', param);
    }

    var addFavoriteProcessed = function (param) {
      broadcastMessage('AddFavoriteProcessed', param);
    }

    var addFavoriteWithLoginProcessed = function (param) {
      broadcastMessage('AddFavoriteWithLoginProcessed', param);
    }

    var currentCoordinatesChanged = function (param) {
      broadcastMessage('CurrentCoordinatesChanged', param)
    }

    var reloadPage = function (param) {
      broadcastMessage('ReloadPage', param);
    }

    var setCitySearchRedirect = function (param) {
      broadcastMessage('SetCitySearchRedirect', param);
    }

    return {
      EventInfoLoaded: eventInfoLoaded,
      ErrorExternalLogin: errorExternalLogin,
      CallLogin: callLogin,
      CompleteExternalLogin: completeExternalLogin,
      LoggedIn: loggedIn,
      LoggedOut: loggedOut,
      LoginProcessed: loginProcessed,
      AddFavoriteProcessed: addFavoriteProcessed,
      AddFavoriteWithLoginProcessed: addFavoriteWithLoginProcessed,
      CurrentCoordinatesChanged: currentCoordinatesChanged,
      ReloadPage: reloadPage,
      SetCitySearchRedirect: setCitySearchRedirect
    }
  }]);