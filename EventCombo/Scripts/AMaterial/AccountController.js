eventComboApp.controller('AccountController', ['$scope', '$http', '$window', '$attrs', '$filter',
  function ($scope, $http, $window, $attrs, $filter) {
    $scope.displayPopups = 'block';
    $scope.email = '';
    $scope.loginPassword = '';
    $scope.loginError = 'Incorrect password, retry or use Forgot Password.';

    $scope.users = [
      { id: 1, email: 'joeeventer@hotmail.com', password: '12345678' },
      { id: 2, email: 'ayon@thecatalystindia.in', password: '12345678' },
      { id: 3, email: 'om@thecatalystindia.in', password: '12345678' },
    ];

    $scope.showLoadingMessage = function (show, message) {
      $scope.popLoading = show;
      $scope.LoadingMessage = message;
    }

    $scope.resetForm = function (form) {
      form.$setPristine();
      form.$setUntouched();
    }

    $scope.reloadPage = function () {
      $window.location.reload();
    }

    $scope.showLoginForm = function () {
      $scope.email = '';
      $scope.resetForm($scope.myForm);
      $scope.popLogin = true;
    }

    $scope.showPasswordForm = function () {
      $scope.loginPassword = '';
      $scope.resetForm($scope.loginPasswordForm);
      $scope.popLoginPassword = true;
    }

    $scope.showRegisterForm = function () {
      $scope.firstName = '';
      $scope.lastName = '';
      $scope.password = '';
      $scope.resetForm($scope.registerForm);
      $scope.popRegister = true;
    }

    // logout submit
    $scope.logout = function () {
      $scope.showLoadingMessage(true, 'Logging out');
      $http.post('/account/logoutAPI', {}).then(function (response) {
        $scope.showLoadingMessage(false, '');
        $window.location.reload();
      }, function (response) {
        $scope.showLoadingMessage(false, '');
        $window.location.reload();
      });
    }

    // signin submit
    $scope.signInNext = function (form) {
      if ($scope[form].$valid) {
        $scope.submitted = false;
        $scope.popLogin = false;
        $scope.showLoadingMessage(true, 'Checking email');
        $http.get('/account/checkusername', { params: { userName: $scope.email } }).then(function (response) {
          $scope.showLoadingMessage(false, '');
          var result = response.data.Success;
          if (result) { $scope.showPasswordForm(); }
          else { $scope.showRegisterForm(); }
        }, function (error) {
          $scope.showLoadingMessage(false, '');
          $scope.popServerError = true;
        });
      } else { $scope.submitted = true; }
    };

    // login password submit
    $scope.loginPasswordNext = function (form) {
      if ($scope[form].$valid) {
        $scope.popLoginPassword = false;
        $scope.showLoadingMessage(true, 'Logging in');
        $http.post('/account/loginAPI', {
            json: angular.toJson({
              Email: $scope.email,
              Password: $scope.loginPassword,
              RememberMe: false
            })
        }).then(function (response) {
          $scope.showLoadingMessage(false, '');
          var result = response.data.Success;
          if (result) {
            $scope.popLoginCongrats = true;
          }
          else {
            $scope.popLoginPassword = true;
            $scope.loginError = response.data.ErrorMessage;
            $scope.IncorrectPassword = true;
          }
        }, function (error) {
          $scope.showLoadingMessage(false, '');
          $scope.popServerError = true;
        });
      }
      else {
        if ($scope[form].loginPassword.$error.required) { $scope.loginPasswordRequired = true; }
      }
    };

    // register submit
    $scope.registerNext = function (form) {
      if ($scope[form].$valid) {
        $scope.popRegister = false;
        $scope.showLoadingMessage(true, 'Registering');
        $http.post('/account/SignupAPI', {
          json: angular.toJson({
            Email: $scope.email,
            FirstName: $scope.firstName,
            LastName: $scope.lastName,
            Password: $scope.password
          })
        }).then(function (response) {
          $scope.showLoadingMessage(false, '');
          var result = response.data.Success;
          if (result) {
            $scope.popRegisterCongrats = true;
          }
          else {
            $scope.popRegister = true;
            $scope.signupError = response.data.ErrorMessage;
          }
        }, function (error) {
          $scope.showLoadingMessage(false, '');
          $scope.popServerError = true;
        });




        $scope.popRegisterCongrats = true;
        console.log('Email: ' + $scope.email + ' First name: ' + $scope.firstName + ' Last name: ' + $scope.lastName + ' Password: ' + $scope.password);
      }
      else {
        if ($scope[form].firstName.$error.required) { $scope.firstNameRequired = true; }
        if ($scope[form].lastName.$error.required) { $scope.lastNameRequired = true; }
        if ($scope[form].password.$error.required) { $scope.passwordRequired = true; }
      }
    };

    // OTP Password submit
    $scope.otpPasswordNext = function (form) {
      if ($scope[form].$valid) {
        if ($scope.otpPassword == '123456') {
          $scope.popForgetPassword = false;
          $scope.popCreatePassword = true;
        }
        else {
          $scope.IncorrectotpPassword = true;
        }
      }
      else {
        if ($scope[form].otpPassword.$error.required) { $scope.otpPasswordRequired = true; }
      }
    };

    // Create a new password 
    $scope.newPasswordNext = function (form) {
      if ($scope[form].$valid) {
        if ($scope.newPassword == $scope.repeatPassword) {
          $scope.popCreatePassword = false;
          $scope.popCreatePasswordCongrats = true;
        }
        else {
          $scope.passwordNotMatch = true;
        }
      }
      else {
        if ($scope[form].newPassword.$error.required) { $scope.newPasswordRequired = true; }
        if ($scope[form].repeatPassword.$error.required) { $scope.repeatPasswordRequired = true; }
      }
    };

    // Escape on the keyboard close all popup
    $scope.closeLightBoxWithEsc = function () {
      $scope.popLogin = false;
      $scope.popRegister = false;
      $scope.popLoginPassword = false;
      $scope.popForgetPassword = false;
      $scope.popCreatePassword = false;
      $scope.popCreatePasswordCongrats = false;
      $scope.popRegisterCongrats = false;
      $scope.popLoginCongrats = false;
      $scope.popLoading = false;
      $scope.popServerError = false;
    };
  }]);

eventComboApp.directive('ngEsc', function () {
  return function (scope, element, attrs) {
    element.bind("keydown keypress keyup", function (event) {
      if (event.which === 27) {
        scope.$apply(function () {
          scope.$eval(attrs.ngEsc);
        });

        event.preventDefault();
      }
    });
  };
});