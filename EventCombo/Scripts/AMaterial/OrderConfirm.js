eventComboApp.controller('orderconfirmController', ['$scope', '$mdDialog', '$rootScope', '$window', 'accountService',
function ($scope, $mdDialog, $rootScope, $window, accountService) {

  $scope.forwardFriend = function (event, mTitle, mType, eventId) {
    $mdDialog.show({
      controller: DialogForwardFriendController,
      templateUrl: 'forwardfriend.tmpl.html',
      parent: angular.element(document.body),
      targetEvent: event,
      clickOutsideToClose: true,
      locals: {
        id: eventId,
        title: mTitle,
        type: mType
      }
    });
  };

  $scope.contactEventcombo = function (event) {
    $mdDialog.show({
      controller: DialogContactEventComboController,
      templateUrl: 'eventcombo.tmpl.html',
      parent: angular.element(document.body),
      targetEvent: event,
      clickOutsideToClose: true
    });
  };

  $scope.callCreateEvent = function () {
    $window.location.href = '@Url.Action("CreateEvent", "EventManagement")';
  }

  $scope.callPurchasedTickets = function () {
    $window.location.href = '@Url.Action("PurchasedTicket", "Account")';
  }

  $rootScope.$on('LoggedIn', function (event, param) {
    if (param === 'ConfirmCreate')
      $scope.callCreateEvent();
    else if (param === "ConfirmPurchases")
      $scope.callPurchasedTickets();
  });

  $scope.createEvent = function () {
    if (accountService.UserRegistered())
      $scope.callCreateEvent();
    else
      accountService.StartLogin('ConfirmCreate');
  }

  $scope.purchasedTickets = function () {
    if (accountService.UserRegistered()) {
      $scope.callPurchasedTickets();
    }
    else
      accountService.StartLogin('ConfirmPurchases');
  }

  $scope.AddToCalendar = function (event) {
    $mdDialog.show({
      controller: DialogAddToCalanderController,
      templateUrl: 'addtocalander.tmpl.html',
      parent: angular.element(document.body),
      targetEvent: event,
      clickOutsideToClose: true
    });
  };

}]);


function DialogAddToCalanderController($scope, $mdDialog, $mdConstant) {
  $scope.cancel = function () {
    $mdDialog.cancel();
  };
}