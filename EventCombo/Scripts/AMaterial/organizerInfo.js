eventComboApp
    .controller('organizerInfo', organizerInfo, ['organizerService', '$mdDialog', 'accountService', 'eventInfoService']);

function organizerInfo($scope, $filter, $mdDialog, organizerService, accountService, eventInfoService) {
    $scope.onTabSelected = function (tab, organizerId) {
        console.log(tab);
        if (tab == 'Upcoming Events') {
            var tmp = organizerService.getOrganizerEvent(true, organizerId).then(function (response) {
                $scope.eventsData = response.data;
                $scope.search("");
            });
        }
        else if (tab == 'Previous Events') {
            var tmp = organizerService.getOrganizerEvent(false, organizerId).then(function (response) {
                $scope.eventsData = response.data;
                $scope.search("");
            });
        }
    }

    $scope.filteredEvents = [];
    $scope.groupedEvents = [];
    $scope.eventsPerPage = 12;
    $scope.pagedEvents = [];
    $scope.currentPage = 0;

    var searchMatch = function (haystack, needle) {
        if (!needle) {
            return true;
        }
        return haystack != needle;
    };

    $scope.search = function (query) {
        $scope.filteredEvents = $filter('filter')($scope.eventsData, function (event) {
            for (var attr in event) {
                if (attr == "EventId") {
                    if (searchMatch(event[attr], query))
                        return true;
                }
            }
            return false;
        });
        $scope.currentPage = 0;
        $scope.groupToPages();
        if (query != "") {
            $scope.invisibleClass = 'invisible';
        }
        else {
            $scope.invisibleClass = '';
        }
    };

    $scope.groupToPages = function () {
        $scope.pagedEvents = [];

        for (var i = 0; i < $scope.filteredEvents.length; i++) {
            if (i % $scope.eventsPerPage === 0) {
                $scope.pagedEvents[Math.floor(i / $scope.eventsPerPage)] = [$scope.filteredEvents[i]];
            } else {
                $scope.pagedEvents[Math.floor(i / $scope.eventsPerPage)].push($scope.filteredEvents[i]);
            }
        }
    };

    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pagedEvents.length - 1) {
            $scope.currentPage++;
        }
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };
    $scope.contactOrganizer = function (event, eventName, organizerName, oId) {
        $mdDialog.show({
            controller: DialogContactOrganizerController,
            templateUrl: 'organizer.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: event,
            clickOutsideToClose: true,
            locals: {
                eventId: 0,
                organizerId: oId,
                eventName: eventName,
                organizerName: organizerName
            }
        });
    };
    $scope.AddToFavorite = function (event) {
        if (!accountService.UserRegistered()) {
            eventInfoService.loadInfo(event.EventId);
            accountService.StartLogin('VEFav' + event.EventId);
        }
        if ((!event) || (event.UserFavorite == true))
            return;
        eventInfoService.addFavorite($scope.UpdateStyles);
    }

    $scope.VoteEvent = function (event) {
        if (!accountService.UserRegistered()) {
            accountService.StartLogin('VEVote' + event.EventId);
        }
        if ((!eventInfo) || (event.UserVote == true))
            return;
        eventInfoService.voteEvent($scope.UpdateStyles);
    }

    $scope.UpdateStyles = function () {
        $scope.favStyle = $scope.event.UserFavorite ? {} : { "color": "white" };
        $scope.voteStyle = $scope.event.UserVote ? {} : { "color": "white" };
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
    $scope.contactOrganizerSaveFav = function (clickfav, eid) {
        SaveFav(clickfav, eid);
    };
}