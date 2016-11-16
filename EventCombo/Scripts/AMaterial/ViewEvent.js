eventComboApp.controller('ViewEventController', ['$scope', '$http', '$window', '$attrs', 'eventInfoService', 'broadcastService', 'accountService', '$mdDialog',
  function ($scope, $http, $window, $attrs, eventInfoService, broadcastService, accountService, $mdDialog) {
    $scope.favStyle = { "color": "white" };
    $scope.voteStyle = { "color": "white" };
    $scope.eventInfo = {};

    $scope.displayVEPopups = 'block';
    $scope.popVEShareWithFriends = false;
    $scope.popVEOrganizerMessage = false;
    $scope.popVEPassword = false;
    $scope.VEMessage = {
      Email: '',
      Name: '',
      Phone: '',
      Message: '',
      EventId: 0
    }

    if (!$attrs.eventid) throw new Error("No event ID defined");

    $scope.map = createMap(40.6984237, -73.9890044);
    deleteMarkers();

    $scope.$on('EventInfoLoaded', function (val) {
      $scope.eventInfo = eventInfoService.getEventInfo();
      $scope.eventInfo.Tickets.forEach(function (ticket, i, arr) {
        ticket.PriceText = "";
      });
      deleteMarkers();
      if ($scope.eventInfo.Latitude) {
        $scope.map.panTo(new google.maps.LatLng($scope.eventInfo.Latitude, $scope.eventInfo.Longitude));
        addEventMarker($scope.eventInfo, $scope.map);
      }
      $scope.UpdateStyles();
      $scope.eventInfoLoaded = true;
    });

    $scope.$on('ShowMessage', function (event, val) {
      $scope.showInfoMessage(true, val);
    })

    $scope.resetForm = function (form) {
      form.$setPristine();
      form.$setUntouched();
    }

    $scope.showPasswordForm = function () {
      $scope.eventPassword = '';
      $scope.resetForm($scope.eventPasswordForm);
      $scope.popVEPassword = true;
    }

    $scope.initController = function (state) {
      if (state == 1)
        $scope.ShowPrivateInvateMessage();
      else if (state == 2)
        $scope.showPasswordForm();
      else
        eventInfoService.loadInfo($attrs.eventid);
    }

    $scope.CancelPassword = function () {
      $scope.popVEPassword = false;
      broadcastService.ReloadPage('/');
    }

    $scope.eventPasswordProcessing = function (form) {
      if ($scope[form].$valid) {
        $scope.popVEPassword = false;
        $scope.showLoadingMessage(true, 'Checking...');
        $http.post('/eventmanagement/CheckPrivateEventAccess', {
          json: angular.toJson({
            EventId: $attrs.eventid,
            Password: $scope.eventPassword,
            IniteId: ''
          })
        }).then(function (response) {
          $scope.showLoadingMessage(false, '');
          if (response.data.Success)
            broadcastService.ReloadPage();
          else {
            $scope.incorrectPassword = true;
            $scope.privateLoginError = response.data.ErrorMessage;
            $scope.popVEPassword = true;
          }
        }, function (error) {
          $scope.showLoadingMessage(false, '');
          $scope.showPasswordForm();
        });
      }
      else {
        if ($scope[form].eventPassword.$error.required) { $scope.eventPasswordRequired = true; }
      }
    };

    $scope.CommonCloseInfoMessage = function () {
      $scope.popInfoMessage = false;
    }

    $scope.CloseInfoMessage = $scope.CommonCloseInfoMessage;

    $scope.PrivateIniteCloseMessage = function () {
      $scope.popInfoMessage = false;
      broadcastService.ReloadPage('/');
    }

    $scope.ShowPrivateInvateMessage = function () {
      $scope.InfoMessage = "You need an invitation to view this private event. If you already have an invitation, please use that invitation link."
      $scope.CloseInfoMessage = $scope.PrivateIniteCloseMessage;
      $scope.popInfoMessage = true;
    }

    $scope.$on('LoggedIn', function (event, param) {
      if (param === 'VEFav' + $scope.eventInfo.EventId) {
        eventInfoService.addFavorite($scope.UpdateStyles);
        broadcastService.ReloadPage();
      }
      else if (param === 'VEVote' + $scope.eventInfo.EventId) {
        eventInfoService.voteEvent($scope.UpdateStyles);
        broadcastService.ReloadPage();
      }
    });

    $scope.onPriceChange = function (ticket) {
      if (ticket) {
        ticket.Amount = parseFloat(ticket.PriceText);
        ticket.Amount = isNaN(ticket.Amount) ? 0 : ticket.Amount;
      }
      eventInfoService.recalcTotal();
    }

    $scope.onPriceBlur = function (ticket) {
      ticket.Amount = (ticket.Amount < 0) || isNaN(ticket.Amount) || !ticket.Amount ? 0 : ticket.Amount;
      ticket.PriceText = ticket.Amount > 0 ? ticket.Amount.toFixed(2) : "";
      $scope.onPriceChange(ticket);
    }

    $scope.OrderCheckout = function () {
      eventInfoService.postTickets();
    }

    $scope.StartLogin = function (loginInfo) {
      broadcastService.CallLogin(loginInfo);
    }

    $scope.AddToFavorite = function () {
      if (!accountService.UserRegistered()) {
        accountService.StartLogin('VEFav' + $scope.eventInfo.EventId);
      }
      if ((!$scope.eventInfo) || ($scope.eventInfo.UserFavorite == true))
        return;
      eventInfoService.addFavorite($scope.UpdateStyles);
    }

    $scope.VoteEvent = function () {
      if (!accountService.UserRegistered()) {
        accountService.StartLogin('VEVote' + $scope.eventInfo.EventId);
      }
      if ((!$scope.eventInfo) || ($scope.eventInfo.UserVote == true))
        return;
      eventInfoService.voteEvent($scope.UpdateStyles);
    }

    $scope.UpdateStyles = function () {
      $scope.favStyle = $scope.eventInfo.UserFavorite ? {} : { "color": "white" };
      $scope.voteStyle = $scope.eventInfo.UserVote ? {} : { "color": "white" };
    }

    $scope.showLoadingMessage = function (show, message) {
      $scope.popLoading = show;
      $scope.LoadingMessage = message;
    }

    $scope.showInfoMessage = function (show, message) {
      $scope.CloseInfoMessage = $scope.CommonCloseInfoMessage;
      $scope.InfoMessage = message;
      $scope.popInfoMessage = show;
    }

    $scope.contactOrganizer = function (event, eventName, organizerName) {
      $mdDialog.show({
        controller: DialogContactOrganizerController,
        templateUrl: 'organizer.tmpl.html',
        parent: angular.element(document.body),
        targetEvent: event,
        clickOutsideToClose: true,
        locals: {
          eventId: $scope.eventInfo.EventId,
          organizerId: $scope.eventInfo.organizerId,
          eventName: eventName,
          organizerName: organizerName
        }
      });
    };

    $scope.forwardFriend = function (event, mTitle, mType) {
      $mdDialog.show({
        controller: DialogForwardFriendController,
        templateUrl: 'forwardfriend.tmpl.html',
        parent: angular.element(document.body),
        targetEvent: event,
        clickOutsideToClose: true,
        locals: {
          id: $scope.eventInfo.EventId,
          title: mTitle,
          type: mType
        }
      });
    };

    $scope.SendMessageTo = function (mtype) {
      $scope.VEMessage.Email = '';
      $scope.VEMessage.Name = '';
      $scope.VEMessage.Message = '';
      if (mtype == 1) {
        $scope.VEShareWithFriendsForm.$setPristine();
        $scope.VEShareWithFriendsForm.$setUntouched();
        $scope.popVEShareWithFriends = true;
      } else {
        $scope.VEOrganizerMessageForm.$setPristine();
        $scope.VEOrganizerMessageForm.$setUntouched();
        $scope.popVEOrganizerMessage = true;;
      }
    }

    $scope.TrySendMessage = function (form, url) {
      if (form.$valid) {
        $scope.VEMessage.EventId = $scope.eventInfo.EventId;
        var data = {
          json: angular.toJson($scope.VEMessage)
        };

        $scope.showLoadingMessage(true, 'Sending message');

        $http.post(url, data).then(function (response) {
          $scope.showLoadingMessage(false, '');
          if (response.data && response.data.Success)
            $scope.showInfoMessage(true, "Message send succesfully");
          else
            $scope.showInfoMessage(true, response.data.ErrorMessage);
        }, function (error) {
          $scope.showLoadingMessage(false, '');
          $scope.showInfoMessage(true, "Error while sending message. Try again later.");
        });
      } else {
        $scope.submitted = true;
      }
    }

    $scope.TrySendShareWithFriendsMessage = function (form) {
      $scope.popVEShareWithFriends = false;
      $scope.TrySendMessage(form, '/notificationAPI/shareFriends');
    }

    $scope.TrySendOrganizerMessage = function (form) {
      $scope.popVEOrganizerMessage = false;
      $scope.TrySendMessage(form, '/notificationAPI/sendOrganizer');
    }
  }]);

eventComboApp.controller('tickets', ["$scope", "$filter", "$attrs", function ($scope, $filter, $attrs) {
  $scope.dealBox = false;
  $scope.$watch('dealBox', function () {
    $scope.toggleText = $scope.dealBox ? 'Hide' : 'Show';
  });

  $scope.dealsArray = props;

  $scope.cartDeals = [];
  $scope.cartDealsMap = [];
  $scope.id = [];

  $scope.addToCart = function (id) {
    $scope.result = $filter('filter')($scope.dealsArray, { dealid: id })[0];
    $scope.cartDeals.push($scope.result);
  }
  $scope.removeFormCart = function (id) {
    $scope.result = $filter('filter')($scope.cartDeals, { dealid: id })[0];
    $scope.dealsArray.push($scope.result);
  }
  $scope.addToCartMap = function (id) {
    if ($scope.id.indexOf(id) != -1) {
      alert("This item is already in Cart!");
    }
    else {
      $scope.result = $filter('filter')($scope.dealsArray, { dealid: id })[0];
      $scope.cartDealsMap.push($scope.result);
      $scope.id.push($scope.result.dealid);
      $scope.$apply();
    }
  }
}]);
/****************************************************************************/
eventComboApp.service('eventInfoService', ['$http', '$rootScope', '$cookies', '$window', 'broadcastService',
  function ($http, $rootScope, $cookies, $window, broadcastService) {

    var eventInfo = {};
    var selectedImage = 0;

    function getEventDateTimeInfoString(dateInfo) {
      var result = "";
      if (dateInfo.Frequency.toLowerCase() == "single") {
        result = FormatDateTimeWithWeekday(dateInfo.StartDateTime);
        if (dateInfo.EndDateTime != dateInfo.StartDateTime)
          result = result + ' to ' + FormatDateTimeWithWeekday(dateInfo.EndDateTime);
      }
      else if (dateInfo.Frequency.toLowerCase() == "monthly") {
        result = "Monthly, " + FormatDateTime(dateInfo.StartDateTime);
        if (dateInfo.EndDateTime != dateInfo.StartDateTime)
          result = result + ' to ' + FormatDateTime(dateInfo.EndDateTime);
      }
      else if (dateInfo.Frequency.toLowerCase() == "daily") {
        result = "Daily, " + FormatTime(dateInfo.StartDateTime);
        if (dateInfo.EndDateTime != dateInfo.StartDateTime)
          result = result + ' to ' + FormatTime(dateInfo.EndDateTime);
      }
      else {
        result = dateInfo.Weekdays.join(', ') + ' ' + FormatTime(dateInfo.StartDateTime);
        if (dateInfo.EndDateTime > dateInfo.StartDateTime)
          result = result + ' to ' + FormatTime(dateInfo.EndDateTime);
      }
      return result;
    }

    var loadInfo = function (eventId) {
      $http.get('/eventmanagement/geteventinfo', { params: { eventId: eventId } }).then(function (response) {
        eventInfo = response.data;
        eventInfo.DateInfo.StartDateTime = setUTC(eventInfo.DateInfo.StartDateTime);
        eventInfo.DateInfo.EndDateTime = setUTC(eventInfo.DateInfo.EndDateTime);
        angular.forEach(eventInfo.Tickets, function (ticket, key) {
          if (ticket.TicketTypeId == 1)
            ticket.TypeName = 'FREE'
          else if (ticket.TicketTypeId == 3)
            ticket.TypeName = 'DONATE'
          else
            ticket.TypeName = '$' + ticket.Price.toFixed(2);
          ticket.Quants = [0];
          for (i = ticket.Minimum; i <= ticket.Maximum; i++)
            ticket.Quants.push(i);
          ticket.StartDate = setUTC(ticket.StartDate);
          ticket.StartDateFormatted = FormatDateTimeWithWeekday(ticket.StartDate);
        });

        eventInfo.EventDateTimeInfoString = getEventDateTimeInfoString(eventInfo.DateInfo);
        recalcTotal();
        broadcastService.EventInfoLoaded();
      });
    }

    var recalcTotal = function () {
      var total = 0.0;
      angular.forEach(eventInfo.Tickets, function (ticket, key) {
        if (ticket.TicketTypeId == 2)
          total = total + ticket.Quantity * ticket.Price;
        else if (ticket.TicketTypeId == 3) {
          ticket.Amount = isNaN(ticket.Amount) || ticket.Amount < 0 ? 0 : ticket.Amount;
          total = total + (isNaN(ticket.Amount) || (ticket.Amount == null ? 0 : ticket.Amount));
        }
      });
      eventInfo.TotalPrice = total;
    }

    var getEventInfo = function () {
      return eventInfo;
    }

    var setSelectedImage = function (id) {
      selectedImage = id;
    }

    var getSelectedImage = function () {
      return selectedImage;
    }

    var addFavorite = function (func) {
      if ((!eventInfo) || (eventInfo.UserFavorite == true))
        return;
      var model = {
        eventId: eventInfo.EventId
      }
      $http.post('/eventmanagement/AddFavorite', model).then(function (response) {
        eventInfo.FavoriteCount = response.data.Count;
        eventInfo.UserFavorite = response.data.Processed;
        if (func)
          func();
      });
    }

    var voteEvent = function (func) {
      if ((!eventInfo) || (eventInfo.UserVote == true))
        return;
      var model = {
        eventId: eventInfo.EventId
      }
      $http.post('/eventmanagement/VoteEvent', model).then(function (response) {
        eventInfo.VoteCount = response.data.Count;
        eventInfo.UserVote = response.data.Processed;
        if (func)
          func();
      });
    }

    var postTickets = function () {
      var tickets = [];
      angular.forEach(eventInfo.Tickets, function (ticket, key) {
        var t = null;
        if ((ticket.TicketTypeId == 3) && (ticket.Amount > 0))
          t = {
            TicketQuantityDetailId: ticket.TQDId,
            Quantity: 1,
            Donate: ticket.Amount,
            Amount: ticket.Amount
          }
        else if (ticket.Quantity > 0)
          t = {
            TicketQuantityDetailId: ticket.TQDId,
            Quantity: ticket.Quantity,
            Donate: 0,
            Amount: ticket.TotalPrice * ticket.Quantity
          }
        if (t != null) {
          tickets.push(t);
        }
      });
      if (tickets.length > 0) {
        var model = {
          json: angular.toJson({
            EventId: eventInfo.EventId,
            Tickets: tickets
          })
        }
        $http.post('/TicketPurchase/LockTickets', model).then(function (response) {
          var res = response.data;
          if (!res.LockId) {
            if (res.TicketsAvailable) {
              broadcastService.ShowMessage("Tickets temporarily locked.")
            } else {
              broadcastService.ShowMessage("Tickets unavailable.")
            }
          }
          else {
            $window.location.href = '/TicketPurchase/Checkout?lockId=' + res.LockId;
          }

        });
      }
    }

    return {
      loadInfo: loadInfo,
      getEventInfo: getEventInfo,
      getSelectedImage: getSelectedImage,
      setSelectedImage: setSelectedImage,
      recalcTotal: recalcTotal,
      postTickets: postTickets,
      addFavorite: addFavorite,
      voteEvent: voteEvent
    }
  }]);
/****************************************************************************/
eventComboApp.controller('gallery', ['$scope', 'eventInfoService', '$mdDialog', '$mdMedia',
  function ($scope, eventInfoService, $mdDialog, $mdMedia) {
    $scope.images = GetImageList(eventInfoService.getEventInfo());
    $scope.$on('EventInfoLoaded', function (val) {
      $scope.images = GetImageList(eventInfoService.getEventInfo());
    });


    $scope.carouselInitializer = function () {
      setTimeout(function () {
        $scope.owl = $('#owlCarousel');
        $scope.owl.owlCarousel({
          dots: false,
          nav: false,
          responsive: {
            0: {
              items: 2
            },
            479: {
              items: 3
            },
            767: {
              items: 4
            },
            991: {
              items: 5
            },
            1170: {
              items: 6
            }
          }
        });
      }, 100);
    };

    $scope.status = '  ';
    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

    $scope.showAdvanced = function (ev, id) {
      eventInfoService.setSelectedImage(id);
      var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;

      $mdDialog.show({
        controller: DialogController,
        templateUrl: 'dialog1.tmpl.html',
        parent: angular.element(document.body),
        targetEvent: ev,
        clickOutsideToClose: true,
        fullscreen: useFullScreen
      })
      .then(function (answer) {
        $scope.status = 'You said the information was "' + answer + '".';
      }, function () {
        $scope.status = 'You cancelled the dialog.';
      });

      $scope.$watch(function () {
        return $mdMedia('xs') || $mdMedia('sm');
      }, function (wantsFullScreen) {
        $scope.customFullscreen = (wantsFullScreen === true);
      });

    };

  }]);
/****************************************************************************/
function GetImageList(eventInfo) {
  var images = [];
  if (!eventInfo || !eventInfo.ImagesUrl || !Array.isArray(eventInfo.ImagesUrl))
    return images;
  for (var i = 0; i < eventInfo.ImagesUrl.length; i++) {
    var image = { id: i + 1, thumb: eventInfo.ImagesUrl[i], img: eventInfo.ImagesUrl[i], title: eventInfo.EventTitle }
    images.push(image);
  };
  return images;
}

function DialogController($scope, $mdDialog, eventInfoService, $filter) {
  $scope.images = GetImageList(eventInfoService.getEventInfo());
  $scope.image = $filter('filter')($scope.images,
	{
	  id: eventInfoService.getSelectedImage()
	})[0];
  $scope.hide = function () {
    $mdDialog.hide();
  };

  $scope.cancel = function () {
    $mdDialog.cancel();
  };

  $scope.answer = function (answer) {
    $mdDialog.hide(answer);
  };

  $scope.onkeydown = function (evt) {
    evt = evt || window.event;
    switch (evt.keyCode) {
      case 38:
        $scope.activeElement.previousElementSibling.focus();
        break;
      case 40:
        $scope.activeElement.nextElementSibling.focus();
        break;
    }
  };
}

eventComboApp.directive('decimalOnly', function () {
  return {
    require: 'ngModel',
    link: function (scope, element, attr, ngModelCtrl) {
      function fromUser(number) {
        if (number) {
          var transformedInput = number.replace(/[^0-9\.]/g, '');
          var nth = 0;
          transformedInput = transformedInput.replace(/\./g, function (match, i, original) {
            nth++;
            return (nth > 1) ? "" : match;
          });
          if (transformedInput !== number) {
            ngModelCtrl.$setViewValue(transformedInput);
            ngModelCtrl.$render();
          }
          return transformedInput;
        }
        return number;
      }
      ngModelCtrl.$parsers.push(fromUser);
    }
  };
});

/****************************************************************************/

function FormatDateTime(date) {
  var myDate = new Date(date);
  myDate = new Date(myDate.getTime() + myDate.getTimezoneOffset() * 60000);
  var options = { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
  return myDate.toLocaleDateString('en-US', options);
}

function FormatDateTimeWithWeekday(date) {
  var oldDate = new Date(date);
  var myDate = new Date(oldDate.getTime() + oldDate.getTimezoneOffset() * 60000);
  var options = { weekday: 'long', year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
  var res = myDate.toLocaleDateString('en-US', options);
  return myDate.toLocaleDateString('en-US', options);
}

function FormatTime(date) {
  var myDate = new Date(date);
  myDate = new Date(myDate.getTime() + myDate.getTimezoneOffset() * 60000);
  var options = { hour: 'numeric', minute: 'numeric' };
  return myDate.toLocaleTimeString('en-US', options);
}

function setUTC(date) {
  if (date.length == 19)
    return date + "Z";
  return date;
}
