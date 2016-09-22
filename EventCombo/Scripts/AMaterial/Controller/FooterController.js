eventComboApp.controller('footerController', footerController);

function footerController($scope, $mdDialog) {

    $scope.contactOrganizer = function (event) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'organizer.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: event,
            clickOutsideToClose: true
        });
    };

    $scope.contactEventcombo = function (event) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'eventcombo.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: event,
            clickOutsideToClose: true
        });
    };

    $scope.forwardFriend = function (event) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'forwardfriend.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: event,
            clickOutsideToClose: true
        });
    };

    $scope.eventSubmit = function (event) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'eventsubmit.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: event,
            clickOutsideToClose: true
        });
    };
};

function DialogController($scope, $mdDialog, $mdConstant, footerService) {
    $scope.currencyVal;
    $scope.showHints = true;
    $scope.submitted = false;
    $scope.sendTo = [];

    getCategory();
    function getCategory() {
        debugger;
        var getCategoryData = footerService.getCategory();
        getCategoryData.then(function (category) {
            $scope.Categories = category.data;
        }, function () {
            alert('Error in getting category records');
        });
    };

    $scope.getSubCategories = function () {
        if ($scope.Categories) {
            var getSubCategoryData = footerService.getSubCategory($scope.Categories);
            getSubCategoryData.then(function (subCategory) {
                $scope.SubCategories = subCategory.data;
            }, function () {
                alert('Error in getting sub category records');
            });
        } else {
            $scope.SubCategories = null;
        }
    };

    $scope.sendContactMessageEventcombo = function (form) {
        if ($scope[form].$valid) {
            $mdDialog.hide();
            var contactMessageViewModel = {
                Name: $scope.cntName,
                Email: $scope.cntEmail,
                PhoneNo: $scope.cntPhone,
                Category: $scope.cntCategory,
                SubCategory: $scope.cntSubCategory,
                Message: $scope.cntMessage
            };
            var getMessageData = footerService.sendContactMessage(contactMessageViewModel);
            getMessageData.then(function (d) {
                alert("Successfully Send!");
            }, function () {
               
            });
        } else {
            $scope.submitted = true;
        }
    };

    $scope.hide = function () {
        $mdDialog.hide();
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.answer = function (form) {
        if ($scope[form].$valid) {
            $mdDialog.hide();
            alert("Successfully Send!");
        } else {
            $scope.submitted = true;
        }
    };

    $scope.chipValidation = function (chipText) {
        console.log("chipValidation Called");
        var reg = /^.+@("@").+\..+$/;
        if (reg.test(chipText)) {
            $scope.isEmailValid = false;
            return chipText;
        }
        else {
            $scope.isEmailValid = true;
            return null;
        }
    }
}

eventComboApp.directive('telMask', function ($filter, $browser) {
    return {
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModelCtrl) {
            var listener = function () {
                var value = $element.val().replace(/[^0-9]/g, '');
                $element.val($filter('tel')(value, false));
                console.log("up" + $filter('tel')(value, false));
            };

            // This runs when we update the text field
            ngModelCtrl.$parsers.push(function (viewValue) {
                return viewValue.replace(/[^0-9]/g, '').slice(0, 10);
            });

            // This runs when the model gets updated on the scope directly and keeps our view in sync
            ngModelCtrl.$render = function () {
                $element.val($filter('tel')(ngModelCtrl.$viewValue, false));
            };

            $element.bind('change', listener);
            $element.bind('keydown', function (event) {
                var key = event.keyCode;
                // If the keys include the CTRL, SHIFT, ALT, or META keys, or the arrow keys, do nothing.
                // This lets us support copy and paste too
                if (key == 91 || (15 < key && key < 19) || (37 <= key && key <= 40)) {
                    return;
                }
                $browser.defer(listener); // Have to do this or changes don't get picked up properly
            });

            $element.bind('paste cut', function () {
                $browser.defer(listener);
            });
        }

    };
});

eventComboApp.filter('tel', function () {
    return function (tel) {
        console.log(tel);
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