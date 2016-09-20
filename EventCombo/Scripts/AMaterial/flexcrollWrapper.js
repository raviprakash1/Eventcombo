/*
 * this is a new directive for updating flexcroll where
 * flexcroll is used. It also has a variation to it where
 * the value of flexcroll directive is given as dynamic
 * and it calculates the height using the maxheight/actual height
 * to determine if the scroll bar should come or not.
 */
eventComboApp.directive('flexcroll', ['$timeout', function ($timeout) {
  return {
    restrict: 'A',
    scope: {
      datasource: '='
    },
    link: function (scope, lElement, attr) {
      var parentHeight = lElement.height()
      var child = lElement.children(':eq(0)');

      scope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase == '$apply' || phase == '$digest') {
          if (fn && (typeof (fn) === 'function')) {
            fn();
          }
        } else {
          this.$apply(fn);
        }
      };

      scope.$watch(childHeight, function (newVal, oldVal) {

        if (attr.flexcroll == 'dynamic') {
          var maxHeight = parseInt(lElement.css('max-height'));
          var childNewHeight = childHeight();
          if (childNewHeight > maxHeight) {
            lElement.css({ 'height': maxHeight + "px" });
          } else {
            lElement.css({ 'height': (childNewHeight + 5) + "px" });
          }
        }

        setTimeout(function () {
          scope.safeApply(fleXenv.fleXcrollMain(lElement[0]));
        }, 200);
      })

      //adding events to stop outer scrollbars from scrolling simultaneously with the scrollbar.Condition is only for precaution.
      if (lElement[0]) {
        lElement[0].addEventListener(navigator.userAgent.indexOf("Firefox") > -1 ? "DOMMouseScroll" : "mousewheel", function (e) {
          e.stopPropagation();
        }, false);

        lElement[0].addEventListener("touchmove", function (e) {
          e.preventDefault();
          e.stopPropagation();
        }, false);
      }

      function childHeight() {
        return child.height();
      }

      scope.$watch(function () {
        return scope.datasource ? scope.datasource.length : 0
      }, function () {
        fleXenv.updateScrollBars();
      });
    }
  }
}])
