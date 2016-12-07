var captcha = angular.module('ngCaptcha', []);
captcha.directive('captcha', function () {
    return {
        restrict: 'E',
        template: 'Captcha: <span ng-model="field1">{{field1}}</span> ' +
		'<span ng-model="operator">{{operator}}</span> ' +
		'<span ng-model="field2">{{field2}}</span> = ',
        scope:
		{
		    field1: "@",
		    field2: "@",
		    operator: "@"
		},
    }
});

captcha.factory('$captcha', ['$rootScope', function ($rootScope) {
    return {
        getOperation: function () {
            $rootScope.operators = ["+", "-", "*", "/"];
            $rootScope.operator = $rootScope.operators[Math.floor(Math.random() * $rootScope.operators.length)];
            $field1 = Math.floor(Math.random() * (25 - 6) + 6);
            if ($rootScope.operator == "/") {
                $num = this.getDivisors($field1);
                $field2 = $num[Math.floor(Math.random() * $num.length)];
            }
            else {
                $field2 = Math.floor((Math.random() * 5) + 1);
                while ($field1 < $field2) {
                    $field1 = Math.floor((Math.random() * 15));
                    $field2 = Math.floor((Math.random() * 5) + 1);
                }
            }
            $rootScope.field1 = $field1;
            $rootScope.field2 = $field2;
        },

        getDivisors: function (n) {
            if (n < 1)
                n = 0;

            var small = [];
            var large = [];
            var end = Math.floor(Math.sqrt(n));
            for (var i = 1; i <= end; i++) {
                if (n % i == 0) {
                    small.push(i);
                    if (i * i != n)
                        large.push(n / i);
                }
            }
            large.reverse();
            return small.concat(large);
        },

        getResult: function (n1, n2, operator) {
            if (operator == "*") {
                return (n1) * (n2);
            }
            else if (operator == "+") {
                return (n1) + (n2);
            }
            else if (operator == "/") {
                return (n1) / (n2);
            }
            else {
                return (n1) - (n2);
            }
        },

        checkResult: function (resultado) {
            if (parseInt(resultado) == this.getResult(parseInt($rootScope.field1), parseInt($rootScope.field2), $rootScope.operator)) {
                return true;
            }
            else {
                this.getOperation();
            }
        }
    }
}]);

captcha.run(function ($captcha) {
    $captcha.getOperation();
});
var myApp = angular.module('app', ['ngMaterial', 'ngRoute', 'ngMessages', 'ngCaptcha']);
myApp.config(function ($routeProvider) {
    $routeProvider
    .when("/index", {
        templateUrl: "home.html",
        controller: "GiveawayController"
    })
    .when("/thanks", {
        templateUrl: "thanks.html"
    })
    .otherwise({
        redirectTo: "/index"
    });
});

myApp.controller('GiveawayController', function ($scope, $mdToast, $location, GiveawayService, $captcha) {
    $scope.eventType = ["Festivals", "Parties", "Conventions", "Food & Drink", "Non - Profit", "Fashion & Style", "Hobbies", "Networking", "Music", "Concerts", "Plays / Theater", "Tech", "Sports & Fitness", "Travel & Outdoor"];
    $scope.eventFrequency = ["1-3 Per Year", "4-6 Per Year", "7-10 Per Year", "10 Or More"];
    $scope.whereHear = ["Facebook", "Google", "Eventcombo", "Friend", "Can't remember"];
    $scope.giveawaySubmit = function (form) {
        console.log($scope[form]);
        console.log($captcha.checkResult($scope.resultado));
        if ($scope[form].$valid) {
            if ($captcha.checkResult($scope.resultado) == true) {
                var requestObject = {
                    "FullName": $scope[form].giveawayFullName.$modelValue,
                    "Email": $scope[form].giveawayEmail.$modelValue,
                    "Phone": $scope[form].giveawayPhone.$modelValue,
                    "OrgName": $scope[form].giveawayOrganizationName.$modelValue,
                    "EventType": $scope[form].giveawayEventType.$modelValue,
                    "EventFrequency": $scope[form].giveawayEventFrequency.$modelValue,
                    "EventLocation": $scope[form].giveawayEventLocation.$modelValue,
                    "WhereHear": $scope[form].giveawayWhereHear.$modelValue
                };
                GiveawayService.mailGiveawayDetails(requestObject).then(function (data) {
                    if (data.status == 200) {
                        $scope.showSimpleToast('success-toast', 'Successfully submitted!');
                        $location.url('/thanks');
                    } else {
                        $scope.showSimpleToast('success-toast', 'Something went wrong!');
                    }
                });
            }
            else {
                $scope.showSimpleToast('error-toast', 'Please enter valid captcha.');
                $("[name=resultado]").val("");
            }
        } else {
            $scope.submitted = true;
            $scope.showSimpleToast('error-toast', 'Please enter all details!');
        }
    };

    $scope.showSimpleToast = function (theme, message) {
        $mdToast.show(
          $mdToast.simple()
            .textContent(message)
            .position('bottom right')
            .hideDelay(3000)
            .theme(theme)
        );
    };
});

myApp.directive('telMask', function ($filter, $browser) {
    return {
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModelCtrl) {
            var listener = function () {
                var value = $element.val().replace(/[^0-9]/g, '');
                $element.val($filter('tel')(value, false));
            };
            ngModelCtrl.$parsers.push(function (viewValue) {
                return viewValue.replace(/[^0-9]/g, '').slice(0, 10);
            });
            ngModelCtrl.$render = function () {
                $element.val($filter('tel')(ngModelCtrl.$viewValue, false));
            };

            $element.bind('change', listener);
            $element.bind('keydown', function (event) {
                var key = event.keyCode;
                if (key == 91 || (15 < key && key < 19) || (37 <= key && key <= 40)) {
                    return;
                }
                $browser.defer(listener);
            });
            $element.bind('paste cut', function () {
                $browser.defer(listener);
            });
        }

    };
});

myApp.filter('tel', function () {
    return function (tel) {
        if (!tel) { return ''; }

        var value = tel.toString().trim().replace(/^\+/, '');

        if (value.match(/[^0-9]/)) {
            return tel;
        }

        var slice1, slice2, slice3;

        switch (value.length) {

            default:
                slice1 = value.slice(0, 3);
                slice2 = value.slice(3, 6);
                slice3 = value.slice(6, 10);
        }

        if (slice1 && slice2 && slice3) {
            return (slice1 + "-" + slice2 + "-" + slice3).trim();
        }
        else if (slice1 && slice2) {
            return (slice1 + "-" + slice2).trim();
        } else if (slice1) {
            return (slice1).trim();
        }

    };
});

myApp.service('GiveawayService', function ($http) {
    return {
        mailGiveawayDetails: function (data) {
            var response = $http({
                method: "post",
                url: "/NotificationAPI/SendGiveAwayEmail",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
    }
});