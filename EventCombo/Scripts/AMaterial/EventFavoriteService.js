eventComboApp.service('eventFavoriteService', ['$rootScope', '$http', 'broadcastService', 'accountService', 
  function ($rootScope, $http, broadcastService, accountService) {

    tryAddFavorite = function (eventId, afterLogin) {
      var model = {
        eventId: eventId
      }
      $http.post('/eventmanagement/AddFavorite', model).then(function (response) {
        favResult = {
          EventId: eventId,
          FavoriteCount: response.data.Count,
          UserFavorite: response.data.Processed || response.data.AlreadyProcessed,
          Processed: response.data.Processed
        };
        if (afterLogin)
          broadcastService.AddFavoriteWithLoginProcessed(favResult);
        else
          broadcastService.AddFavoriteProcessed(favResult)
      });
    }

    $rootScope.$on('LoggedIn', function (event, param) {
      var paramArray = param.split('_');
      var eventId = paramArray[paramArray.length - 1];
      if (paramArray[0] === 'EVFAdd') {
        tryAddFavorite(eventId, 1);
      }
    });

    addFavorite = function (eventId) {
      if (!eventId)
        return;
      if (accountService.UserRegistered())
        tryAddFavorite(eventId, 0);
      else
        accountService.StartLogin('EVFAdd_' + eventId);
    }

    return {
      AddFavorite: addFavorite
    }
  }]);