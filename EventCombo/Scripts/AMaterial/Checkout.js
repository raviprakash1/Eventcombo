/*
Developer				: Om Prakash Choudhary
Developer's ID	: om@thecatalystindia.in
Company 				: The Catalyst
Company Website	: www.thecatalystindia.in
*/
/****************************************************************************/
eventComboApp
    .controller('checkoutController', checkoutController);

function checkoutController($scope, $mdDialog)
{
  $scope.paypalConfirm = function (ev)
  {
    var confirm = $mdDialog.confirm()
      .title('Please Confirm to continue')
      .textContent('Amount: $3,022.30')
      .ariaLabel('Checkout Confirmation')
      .targetEvent(ev)
      .ok('Confirm')
      .cancel('Cancel');
    $mdDialog.show(confirm).then(function ()
    {
      $scope.status = 'Paypal payment is processed.';
    },
    function ()
    {
      $scope.paymentMethod = 'creditcard'
      $scope.status = 'Paypal payment was canceled.';
    });
  };

  $scope.creditcardSubmit = function (form)
  {
    if ($scope[form].$valid)
    {
      $scope.status = 'Credit card detail was submitted successfully.';
    }
    else
    {
      $scope.submitted = true;
      $scope.status = 'Please enter all details.';
    }
  };

  $scope.paypalSubmit = function (form, event)
  {
    if ($scope[form].$valid)
    {
      $mdDialog.hide();
      $scope.paypalConfirm(event);
    }
    else
    {
      $scope.submitted = true;
      $scope.status = 'Please enter all details.';
    }
  };

  $scope.detectCardType = function (cardNumber, eventType, form)
  {
    $scope[form].checkoutCardNumber.$valid = true;
    $scope.validTry = false;
    var cardType =
    {
      mastercard: /^5[1-5][0-9]{14}$/,
      visa: /^4[0-9]{12}(?:[0-9]{3})?$/,
      discover: /^6(?:011|5[0-9]{2})[0-9]{12}$/
    }
    for (var key in cardType)
    {
      if (cardType[key].test(cardNumber))
      {
        $scope.crCardType = key;
        $scope.validTry = true;
        $scope[form].checkoutCardNumber.$error.crCardMessage = false;
        return key
      }
      else
      {
        $scope.crCardType = '';
      }
    }
    if (eventType == 'blur' && !$scope.validTry)
    {
      $scope[form].checkoutCardNumber.$error.crCardMessage = true;
    }
  };
};
eventComboApp.directive('creditCard', function ($filter, $browser)
{
  return {
    require: 'ngModel',
    link: function ($scope, $element, $attrs, ngModelCtrl)
    {
      var listener = function ()
      {
        var value = $element.val().replace(/[^0-9]/g, '');
        var newVAlue = $filter('card')(value, false);
        var cardNo = '';
        var cardNoTrim = '';
        for (var i = 1; i <= newVAlue.length; i++)
        {
          if (i <= 15)
          {
            if (newVAlue.charAt(i - 1) == ' ')
            {
              cardNo += ' ';
            }
            else
            {
              cardNo += '*';
            }
          }
          else
          {
            cardNo += newVAlue.charAt(i - 1);
          }
          if (newVAlue.charAt(i - 1) != ' ')
          {
            cardNoTrim += newVAlue.charAt(i - 1);
          }
        }
        $element.val(newVAlue);
        $scope.starValue = cardNo;

        if (cardNoTrim.length > 12)
        {
          $scope.crCardType =
            (/^5[1-5][0-9]{14}$/.test(cardNoTrim)) ? "mastercard"
            : (/^4[0-9]{12}(?:[0-9]{3})?$/.test(cardNoTrim)) ? "visa"
            : (/^3[47]/.test(cardNoTrim)) ? 'amex'
            : (/^6(?:011|5[0-9]{2})[0-9]{12}$/.test(cardNoTrim)) ? 'discover'
            : undefined
          if ($scope.crCardType == undefined)
          {
            $scope.creditcardForm.checkoutCardNumber.$setValidity('crCardMessage', false);
          }
          else if ($scope.crCardType == 'amex')
          {
            $scope.creditcardForm.checkoutCardNumber.$setValidity('amexMessage', false);
          }
          else
          {
            $scope.creditcardForm.checkoutCardNumber.$setValidity('crCardMessage', true);
            $scope.creditcardForm.checkoutCardNumber.$setValidity('amexMessage', true);
          }
        }
        $scope.$apply();
      };

      // This runs when we update the text field
      ngModelCtrl.$parsers.push(function (viewValue)
      {
        return viewValue.replace(/[^0-9]/g, '').slice(0, 16);
      });

      // This runs when the model gets updated on the scope directly and keeps our view in sync
      ngModelCtrl.$render = function ()
      {
        $element.val($filter('card')(ngModelCtrl.$viewValue, false));
      };

      $element.bind('change', listener);
      $element.bind('keyup', function (event)
      {
        $scope.crCardType = '';
        var key = event.keyCode;
        // If the keys include the CTRL, SHIFT, ALT, or META keys, or the arrow keys, do nothing.
        // This lets us support copy and paste too
        if (key == 91 || (15 < key && key < 19) || (37 <= key && key <= 40))
        {
          return;
        }
        $browser.defer(listener); // Have to do this or changes don't get picked up properly
      });

      $element.bind('paste cut', function ()
      {
        $browser.defer(listener);
      });
    }

  };
});

eventComboApp.filter('card', function ()
{
  return function (card)
  {
    if (!card) { return ''; }
    var value = card.toString().trim().replace(/^\+/, '');
    if (value.match(/[^0-9]/))
    {
      return card;
    }
    var slice1, slice2, slice3, slice4;

    switch (value.length)
    {
      default:
        slice1 = value.slice(0, 4);
        slice2 = value.slice(4, 8);
        slice3 = value.slice(8, 12);
        slice4 = value.slice(12, 16);
    }
    if (slice1 && slice2 && slice3 && slice4)
    {
      return (slice1 + " " + slice2 + " " + slice3 + " " + slice4).trim();
    }
    else if (slice1 && slice2 && slice3)
    {
      return (slice1 + " " + slice2 + " " + slice3).trim();
    }
    else if (slice1 && slice2)
    {
      return (slice1 + " " + slice2).trim();
    }
    else if (slice1)
    {
      return (slice1).trim();
    }
  };
});

eventComboApp.directive('expirationDate', function ($filter, $browser)
{
  return {
    require: 'ngModel',
    link: function ($scope, $element, $attrs, ngModelCtrl)
    {
      var listener = function ()
      {
        var value = $element.val().replace(/[^0-9]/g, '');
        $element.val($filter('expiry')(value, false));
      };

      // This runs when we update the text field
      ngModelCtrl.$parsers.push(function (viewValue)
      {
        return viewValue.replace(/[^0-9]/g, '').slice(0, 4);
      });

      // This runs when the model gets updated on the scope directly and keeps our view in sync
      ngModelCtrl.$render = function ()
      {
        $element.val($filter('expiry')(ngModelCtrl.$viewValue, false));
      };

      $element.bind('change', listener);
      $element.bind('keydown', function (event)
      {
        var key = event.keyCode;
        // If the keys include the CTRL, SHIFT, ALT, or META keys, or the arrow keys, do nothing.
        // This lets us support copy and paste too
        if (key == 91 || (15 < key && key < 19) || (37 <= key && key <= 40))
        {
          return;
        }
        $browser.defer(listener); // Have to do this or changes don't get picked up properly
      });

      $element.bind('paste cut', function ()
      {
        $browser.defer(listener);
      });
    }

  };
});
eventComboApp.filter('expiry', function ()
{
  return function (expiry)
  {
    if (!expiry) { return ''; }
    var value = expiry.toString().trim().replace(/^\+/, '');
    if (value.match(/[^0-9]/))
    {
      return expiry;
    }
    var slice1, slice2;
    switch (value.length)
    {
      default:
        slice1 = value.slice(0, 2);
        slice2 = value.slice(2, 4);
    }
    if (slice1 && slice2)
    {
      return (slice1 + "/" + slice2).trim();
    }
    else if (slice1)
    {
      return (slice1).trim();
    }
  };
});

eventComboApp.directive('securityCode', function ($filter, $browser)
{
  return {
    require: 'ngModel',
    link: function ($scope, $element, $attrs, ngModelCtrl)
    {
      var listener = function ()
      {
        var value = $element.val().replace(/[^0-9]/g, '');
        $element.val($filter('cvv')(value, false));
      };

      // This runs when we update the text field
      ngModelCtrl.$parsers.push(function (viewValue)
      {
        return viewValue.replace(/[^0-9]/g, '').slice(0, 4);
      });

      // This runs when the model gets updated on the scope directly and keeps our view in sync
      ngModelCtrl.$render = function ()
      {
        $element.val($filter('cvv')(ngModelCtrl.$viewValue, false));
      };

      $element.bind('change', listener);
      $element.bind('keyup', function (event)
      {
        var key = event.keyCode;
        // If the keys include the CTRL, SHIFT, ALT, or META keys, or the arrow keys, do nothing.
        // This lets us support copy and paste too
        if (key == 91 || (15 < key && key < 19) || (37 <= key && key <= 40))
        {
          return;
        }
        $browser.defer(listener); // Have to do this or changes don't get picked up properly
      });

      $element.bind('paste cut', function ()
      {
        $browser.defer(listener);
      });
    }

  };
});
eventComboApp.filter('cvv', function ()
{
  return function (cvv)
  {
    if (!cvv) { return ''; }
    var value = cvv.toString().trim().replace(/^\+/, '');
    if (value.match(/[^0-9]/))
    {
      return cvv;
    }
    var slice1, slice2;
    switch (value.length)
    {
      default:
        slice1 = value.slice(0, 3);
    }
    if (slice1)
    {
      return (slice1).trim();
    }
  };
});