eventComboApp.controller('ViewEventController', ['$scope', '$http', '$window', '$attrs', 'eventInfoService', 'broadcastService',
  function ($scope, $http, $window, $attrs, eventInfoService, broadcastService) {
    $scope.favStyle = { "color": "white" };
    $scope.voteStyle = { "color": "white" };
    $scope.eventInfo = {};
    if (!$attrs.eventid) throw new Error("No event ID defined");

    $scope.map = createMap(40.6984237, -73.9890044);
    deleteMarkers();

    $scope.$on('EventInfoLoaded', function (val) {
      $scope.eventInfo = eventInfoService.getEventInfo();
      deleteMarkers();
      if ($scope.eventInfo.Latitude) {
        $scope.map.panTo(new google.maps.LatLng($scope.eventInfo.Latitude, $scope.eventInfo.Longitude));
        addEventMarker($scope.eventInfo, $scope.map);
      }
      $scope.UpdateStyles();
    });

    eventInfoService.loadInfo($attrs.eventid);

    $scope.$on('LoggedIn', function (event, param) {
      if (param === 'VEFav' + $scope.eventInfo.EventId) {
        eventInfoService.addFavorite($scope.UpdateStyles);
        broadcastService.LoginProcessed();
      }
      else if (param === 'VEVote' + $scope.eventInfo.EventId) {
        eventInfoService.voteEvent($scope.UpdateStyles);
        broadcastService.LoginProcessed();
      }
    });

    $scope.onPriceChange = function () {
      eventInfoService.recalcTotal();
    }

    $scope.onPriceBlur = function (ticket) {
      if (isNaN(ticket.Amount) || (ticket.Amount == null))
        ticket.Amount = 0;
    }

    $scope.OrderCheckout = function () {
      eventInfoService.postTickets();
    }

    $scope.StartLogin = function(loginInfo){
      broadcastService.CallLogin(loginInfo);
    }

    $scope.AddToFavorite = function () {
      if (!$scope.userRegistered) {
        $scope.StartLogin({ callerId: 'VEFav' + $scope.eventInfo.EventId });
      }
      if ((!$scope.eventInfo) || ($scope.eventInfo.UserFavorite == true))
        return;
      eventInfoService.addFavorite($scope.UpdateStyles);
    }

    $scope.VoteEvent = function () {
      if (!$scope.userRegistered) {
        $scope.StartLogin({ callerId: 'VEVote' + $scope.eventInfo.EventId });
      }
      if ((!$scope.eventInfo) || ($scope.eventInfo.UserVote == true))
        return;
      eventInfoService.voteEvent($scope.UpdateStyles);
    }

    $scope.UpdateStyles = function () {
      $scope.favStyle = $scope.eventInfo.UserFavorite ? {} : { "color": "white" };
      $scope.voteStyle = $scope.eventInfo.UserVote ? {} : { "color": "white" };
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
        if (dateInfo.EndDateTime > dateInfo.StartDateTime)
          result = result + ' to ' + FormatDateTimeWithWeekday(dateInfo.EndDateTime);
      }
      else if (dateInfo.Frequency.toLowerCase() == "monthly") {
        result = "Monthly, " + FormatDateTime(dateInfo.StartDateTime);
        if (dateInfo.EndDateTime > dateInfo.StartDateTime)
          result = result + ' to ' + FormatDateTime(dateInfo.EndDateTime);
      }
      else if (dateInfo.Frequency.toLowerCase() == "daily") {
        result = "Daily, " + FormatTime(dateInfo.StartDateTime);
        if (dateInfo.EndDateTime > dateInfo.StartDateTime)
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
          total = total + ticket.Quantity * ticket.TotalPrice;
        else if (ticket.TicketTypeId == 3) {
          ticket.Amount = isNaN(ticket.Amount) || ticket.Amount >= 0 ? ticket.Amount : 0;
          total = total + (isNaN(ticket.Amount) ? 0 : (ticket.Amount == null ? 0 : ticket.Amount));
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
      var selection = '';
      angular.forEach(eventInfo.Tickets, function (ticket, key) {
        var t = null;
        if ((ticket.TicketTypeId == 3) && (ticket.Amount > 0))
          t = {
            TLD_TQD_Id: ticket.TQDId,
            TLD_Locked_Qty: 1,
            TLD_Event_Id: eventInfo.EventId,
            TLD_Donate: ticket.Amount,
            TicketAmount: ticket.Amount
          }
        else if (ticket.Quantity > 0)
          t = {
            TLD_TQD_Id: ticket.TQDId,
            TLD_Locked_Qty: ticket.Quantity,
            TLD_Event_Id: eventInfo.EventId,
            TLD_Donate: 0,
            TicketAmount: ticket.TotalPrice * ticket.Quantity
          }
        if (t != null) {
          selection = (selection == '' ? '' : (selection + '¶')) + t.TLD_TQD_Id + '~' + t.TLD_Locked_Qty + '~' + t.TLD_Donate;
          tickets.push(t);
        }
      });
      if (tickets.length > 0) {
        var model = {
          ev: angular.toJson({
            TLD_TQD_Id: '0',
            TLD_Locked_Qty: '0',
            TLD_List: tickets
          })
        }
        $http.post('/eventmanagement/StartPurchase', model).then(function (response) {
          if (response.data == 'N') {
            $cookies.remove("Selection");
          }
          else if (response.data == "NOTLIVE") {
            $cookies.remove("Selection");
          } else {
            var d = new Date();
            d.setDate(d.getDate() + 1);
            $cookies.put("Selection", selection, { expires: d });
            $window.location.href = '/TicketPayment/TicketPayment';
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
    $scope.$on('ECEventInfoLoaded', function (val) {
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
/****************************************************************************/

function FormatDateTime(date) {
  var myDate = new Date(date);
  myDate = new Date(myDate.getTime() + myDate.getTimezoneOffset() * 60000);
  var options = { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
  return myDate.toLocaleDateString('en-US', options);
}

function FormatDateTimeWithWeekday(date) {
  var myDate = new Date(date);
  myDate = new Date(myDate.getTime() + myDate.getTimezoneOffset() * 60000);
  var options = { weekday: 'long', year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
  return myDate.toLocaleDateString('en-US', options);
}

function FormatTime(date) {
  var myDate = new Date(date);
  myDate = new Date(myDate.getTime() + myDate.getTimezoneOffset() * 60000);
  var options = { hour: 'numeric', minute: 'numeric' };
  return myDate.toLocaleTimeString('en-US', options);
}
