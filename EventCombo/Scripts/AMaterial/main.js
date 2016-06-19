var buttnCount = 0;

var createEventApp = angular.module("CreateEventApp", ['ngMaterial', 'ngMessages', 'color.picker', 'mdDatetimePickerDemo',
  'ngGallery', 'ui.tinymce']);
createEventApp.controller('CreateEventController', ['$scope', '$http', '$window', '$timeout', 'ngGallery',
  function ($scope, $http, $window, $timeout, ngGallery) {


      angular.element(document).ready(function () {

          // automatically adjust textarea
          $('textarea').on('input', function () {
              $(this).outerHeight(38).outerHeight(this.scrollHeight);
          });
      });

  $scope.organiserInfo = [];

  $scope.isPrivateEvent = false;
  $scope.includeSocial = 0;
  $scope.gPlace;

  $scope.tinymceOptions = {
    selector: "textarea",
    height: 100,
    mode: "textareas",
    toolbar1: "newdocument fullpage | bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | styleselect formatselect fontselect fontsizeselect",
    toolbar2: "cut copy paste | searchreplace | bullist numlist | outdent indent blockquote | undo redo | link unlink anchor image media code | insertdatetime preview | forecolor backcolor",
    toolbar3: "table | hr removeformat | subscript superscript | charmap emoticons | print fullscreen | ltr rtl | spellchecker | visualchars visualblocks nonbreaking template pagebreak restoredraft",

    menubar: false,
    toolbar_items_size: 'small',

    style_formats: [{
      title: 'Bold text',
      inline: 'b'
    }]
  }

  $scope.vartypes = [
    { varId: false, varName: "Required" },
    { varId: true, varName: "Optional" }
  ];

  $scope.occurences = [
 "Daily",
 "Weekly",
 "Monthly"
  ];

  $scope.weekdays = [
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday",
      "Sunday"
  ];

  $scope.subCategories = [];
  $scope.organizerEditState = "Saved";

  $scope.selectedDateTimeText = "Pick the date here";
  $scope.showDateTimeDialog = false;

  $http.get('/eventmanagement/getevent', { params: { eventId: 0 } }).then(function (response) {
    $scope.eventInfo = response.data;
    $scope.prepareEventInfo();
  });

  $scope.prepareEventInfo = function () {
      console.log("Prepare event info called");
    $scope.startDateTime = new Date($scope.eventInfo.DateInfo.StartDateTime);
    $scope.endDateTime = new Date($scope.eventInfo.DateInfo.EndDateTime);
    var time = formatAMPM($scope.startDateTime);
    var ctime = $scope.eventInfo.DateInfo.TimeList.filter(function (obj) { return obj.TimeString == time; });
    if (ctime.length > 0)
      $scope.selectedstartTime = ctime[0];
    else
      $scope.selectedstartTime = null;
    time = formatAMPM($scope.endDateTime);
    ctime = $scope.eventInfo.DateInfo.TimeList.filter(function (obj) { return obj.TimeString == time; });
    if (ctime.length > 0)
      $scope.selectedendTime = ctime[0];
    else
      $scope.selectedendTime = null;
    $scope.startingDate = new Date($scope.startDateTime.getFullYear(), $scope.startDateTime.getMonth(), $scope.startDateTime.getDate());
    $scope.endingDate = new Date($scope.endDateTime.getFullYear(), $scope.endDateTime.getMonth(), $scope.endDateTime.getDate());
    $scope.occurence = $scope.eventInfo.DateInfo.Frequency;
    if ($scope.eventInfo.DateInfo.Frequency == "Single") {
      $scope.dateTab = 0;
      if (!$scope.eventInfo.DateInfo.IsNewDate)
        $scope.showDates();
    }
    else {
      $scope.dateTab = 1;
      if (!$scope.eventInfo.DateInfo.IsNewDate)
        $scope.showMultipleDates();
    }
    if ($scope.eventInfo.VariableChargesList.length <= 0)
        $scope.addVarCharge();
    console.log("Prepare event called");
    $scope.eventInfo.TicketList.forEach(function (ticket, i, arr) {
      ticket.localSaleStartDate = ticket.Sale_Start_Date == null ? null : new Date(ticket.Sale_Start_Date);
      ticket.localSaleEndDate = ticket.Sale_End_Date == null ? null : new Date(ticket.Sale_End_Date);
      ticket.localHideUntillDate = ticket.Hide_Untill_Date == null ? null : new Date(ticket.Hide_Untill_Date);
      ticket.localHideAfterDate = ticket.Hide_After_Date == null ? null : new Date(ticket.Hide_After_Date);
    });
  }

  $scope.showDates = function () {
    $scope.showDateTimeDialog = false;
    $scope.eventInfo.DateInfo.IsNewDate = false;
    var startingDate = ($scope.startingDate.getMonth() + 1) + "/" + $scope.startingDate.getDate() + "/" + $scope.startingDate.getFullYear();
    var endingDate = ($scope.endingDate.getMonth() + 1) + "/" + $scope.endingDate.getDate() + "/" + $scope.endingDate.getFullYear();
    $scope.occurence = "Single";
    $scope.selectedDateTimeText = startingDate + " " + $scope.selectedstartTime.TimeString + " To " + endingDate + " " + $scope.selectedendTime.TimeString;
  }

  $scope.showMultipleDates = function () {
    $scope.showDateTimeDialog = false;
    $scope.eventInfo.DateInfo.IsNewDate = false;
    var occurence = $scope.occurence;
    if (occurence == "Weekly") {

      var alldays = "";
      for (var day = 0; day < $scope.eventInfo.Weekdays.length; day++) {
        alldays += $scope.eventInfo.Weekdays[day] + ",";
      }

      alldays = alldays.substr(0, alldays.length - 1);

      $scope.selectedDateTimeText = "Days (" + alldays + ") Time " + $scope.selectedstartTime.TimeString + " To " + $scope.selectedendTime.TimeString;
    }
    else if (occurence == "Daily") {
      $scope.selectedDateTimeText = "Daily Time " + $scope.selectedstartTime.TimeString + " To " + $scope.selectedendTime.TimeString;
    }
    else {
      var startingDate = ($scope.startingDate.getMonth() + 1) + "/" + $scope.startingDate.getDate() + "/" + $scope.startingDate.getFullYear();
      var endingDate = ($scope.endingDate.getMonth() + 1) + "/" + $scope.endingDate.getDate() + "/" + $scope.endingDate.getFullYear();
      $scope.selectedDateTimeText = "Monthly " + startingDate + " " + $scope.selectedstartTime.TimeString + " To " + endingDate + " " + $scope.selectedendTime.TimeString;
    }


  }

  $scope.onCategoryChange = function () {
    $scope.subCategories = [];
    $scope.eventInfo.EventCategoryList.forEach(function (cat, i, arr) {
      if (cat.EventCategoryId == $scope.eventInfo.EventCategoryID)
        $scope.subCategories = cat.SubCategories;
    });
  }

  $scope.startDateChange = function () {
    if ($scope.endingDate < $scope.startingDate)
      $scope.endingDate = new Date($scope.startingDate.getFullYear(), $scope.startingDate.getMonth(), $scope.startingDate.getDate());
  }

  $scope.endDateChange = function () {
    if ($scope.endingDate < $scope.startingDate)
      $scope.startingDate = new Date($scope.endingDate.getFullYear(), $scope.endingDate.getMonth(), $scope.endingDate.getDate());
  }

  $scope.openGallery = function () {
    var iList = [];
    $scope.eventInfo.EventImages.forEach(function (img, i, arr) {
      iList.push(img.ImageUrl)
    });
    ngGallery.open({
      template: '<p>Hello</p>',
      plain: true,
      images: iList,
      prefix: '',
      showClose: true,
      closeByEscape: true,
      closeLabel: '<i class="material-icons">clear</i>',
      nextLabel: '<i class="material-icons">keyboard_arrow_right</i>',
      prevLabel: '<i class="material-icons">keyboard_arrow_left</i>',
      closeByNavigation: true,
      infiniteLoop: false,
      auto: false
    });
  };

  $scope.callImageClick = function (imgctrl) {
    $timeout(function () {
      document.getElementById(imgctrl).click();
    }, 100);
  }

  $scope.organizerAdd = function () {
    var i = Math.max.apply(Math, $scope.eventInfo.OrganizerList.map(function (o) { return o.InternalId; }));
    $scope.eventInfo.CurrentOrganizer = {
      Orgnizer_Id: 0,
      Orgnizer_Name: "",
      Organizer_Desc: "",
      Organizer_FBLink: "",
      Organizer_Twitter: "",
      Organizer_Linkedin: "",
      UserId: $scope.eventInfo.UserID,
      Organizer_Email: "",
      Organizer_Phoneno: "",
      Organizer_Status: "A",
      Imagepath: "",
      IncludeSocialLinks: false,
      ECImageId: null,
      InternalId: i + 1,
      Image: {
        Id: 0,
        ImageType: 0,
        Filename: "",
        ImageUrl: "",
        ContentType: ""
      }
    };
    $scope.organizerEditState = "Add";
  }

  $scope.editOrganiser = function () {
    $scope.eventInfo.CurrentOrganizer = null;
    var org = $scope.eventInfo.OrganizerList.filter(function (obj) {
      return obj.InternalId == $scope.eventInfo.InternalOrganizerId;
    });
    if (org.length > 0) {
      $scope.eventInfo.CurrentOrganizer = {
        InternalId: org[0].InternalId,
        Orgnizer_Name: org[0].Orgnizer_Name,
        Organizer_Email: org[0].Organizer_Email,
        Organizer_Desc: org[0].Organizer_Desc,
        IncludeSocialLinks: org[0].IncludeSocialLinks,
        Organizer_FBLink: org[0].Organizer_FBLink,
        Organizer_Twitter: org[0].Organizer_Twitter,
        Organizer_Linkedin: org[0].Organizer_Linkedin,
        Image: {
          Id: org[0].Image.Id,
          ImageType: org[0].Image.ImageType,
          Filename: org[0].Image.Filename,
          ImageUrl: org[0].Image.ImageUrl,
          ContentType: org[0].Image.ContentType
        }
      };
      $scope.organizerEditState = "Edit";
    }
    console.debug($scope.eventInfo.CurrentOrganizer);
  }

  $scope.saveNewOrganizer = function () {
    if ($scope.eventInfo.CurrentOrganizer.Orgnizer_Name) {
      var doubleObj = $scope.eventInfo.OrganizerList.filter(function (obj) {
        return obj.Orgnizer_Name.trim() == $scope.eventInfo.CurrentOrganizer.Orgnizer_Name.trim();
      });
      if (doubleObj.length > 0) {
        alert("Organizer with name " + $scope.eventInfo.CurrentOrganizer.Orgnizer_Name.trim() + " already exists. Please enter another name.");
        return;
      }

      if (!$scope.eventInfo.CurrentOrganizer.IncludeSocialLinks) {
        $scope.eventInfo.CurrentOrganizer.Organizer_FBLink = null;
        $scope.eventInfo.CurrentOrganizer.Organizer_Twitter = null;
        $scope.eventInfo.CurrentOrganizer.Organizer_Linkedin = null;
      }

      $scope.eventInfo.OrganizerList.push($scope.eventInfo.CurrentOrganizer);
      $scope.eventInfo.InternalOrganizerId = $scope.eventInfo.CurrentOrganizer.InternalId;
      $scope.eventInfo.CurrentOrganizer = null;
    }
    $scope.organizerEditState = "Saved";
  }

  $scope.saveEditedOrganizer = function () {
    var org = $scope.eventInfo.OrganizerList.filter(function (obj) {
      return obj.InternalId == $scope.eventInfo.CurrentOrganizer.InternalId;
    });
    if (org.length > 0) {
      org[0].Orgnizer_Name = $scope.eventInfo.CurrentOrganizer.Orgnizer_Name;
      org[0].Organizer_Email = $scope.eventInfo.CurrentOrganizer.Organizer_Email;
      org[0].Organizer_Desc = $scope.eventInfo.CurrentOrganizer.Organizer_Desc;
      org[0].IncludeSocialLinks = $scope.eventInfo.CurrentOrganizer.IncludeSocialLinks;
      org[0].Organizer_FBLink = org[0].IncludeSocialLinks ? $scope.eventInfo.CurrentOrganizer.Organizer_FBLink : null;
      org[0].Organizer_Twitter = org[0].IncludeSocialLinks ? $scope.eventInfo.CurrentOrganizer.Organizer_Twitter : null;
      org[0].Organizer_Linkedin = org[0].IncludeSocialLinks ? $scope.eventInfo.CurrentOrganizer.Organizer_Linkedin : null;
      org[0].Image.Id = $scope.eventInfo.CurrentOrganizer.Image.Id;
      org[0].Image.ImageType = $scope.eventInfo.CurrentOrganizer.Image.ImageType;
      org[0].Image.Filename = $scope.eventInfo.CurrentOrganizer.Image.Filename;
      org[0].Image.ImageUrl = $scope.eventInfo.CurrentOrganizer.Image.ImageUrl;
      org[0].Image.ContentType = $scope.eventInfo.CurrentOrganizer.Image.ContentType;
      console.debug(org[0]);
      console.debug($scope.eventInfo.CurrentOrganizer);
    };
    $scope.organizerEditState = "Saved";
  }

  $scope.getTotalTickets = function () {
    var total = 0;
    if (($scope.eventInfo != null) && ($scope.eventInfo.TicketList != null))
      $scope.eventInfo.TicketList.forEach(function (t, i, arr) {
        total = total + parseInt(t.Qty_Available);
      });
    return total;
  }

  $scope.addTicket = function (ttype) {
    var i = 0;
    if ($scope.eventInfo.TicketList.length > 0)
      Math.max.apply(Math, $scope.eventInfo.TicketList.map(function (o) { return o.InternalId; }));
    var newticket = {
      T_Id: 0,
      InternalId: i + 1,
      E_Id: 0,
      T_name: "",
      Qty_Available: 0,
      Price: 0,
      TicketTypeID: ttype,
      T_Sold: 0,
      T_Desc: "",
      Show_T_Desc: "0",
      Fees_Type: "0",
      Sale_Start_Date: null,
      Sale_End_Date: null,
      Hide_Ticket: "0",
      Auto_Hide_Sche: "0",
      Hide_Untill_Date: null,
      Hide_After_Date: null,
      Min_T_Qty: null,
      Max_T_Qty: null,
      T_Disable: 0,
      T_Mark_SoldOut: "0",
      EC_Fee: 0,
      Customer_Fee: 0,
      T_Displayremaining: "0",
      T_AutoSechduleType: "0",
      T_Discount: 0,
      TicketType: {
        TicketTypeId: ttype,
        TicketType: (ttype == 1 ? "Free ticket" : ttype == 2 ? "Paid ticket" : ttype == 3 ? "Donation" : "Unkonwn")
      },
      TotalPrice: 0,
      T_Customize: 0,
      T_Ecpercent: 0,
      T_EcAmount: 0,
      localSaleStartDate: null,
      localSaleEndDate: null,
      localHideUntillDate: null,
      localHideAfterDate: null
    }
    $scope.onPriceChange(newticket);
    console.log("add ticket called");
    $scope.eventInfo.TicketList.push(newticket);
  }

  $scope.onPriceChange = function (ticket) {
    ticket.EC_Fee = ticket.Price == 0 ? 0 : ticket.Price * $scope.eventInfo.FeeStruct.FS_Percentage / 100 + $scope.eventInfo.FeeStruct.FS_Amount;
    ticket.Customer_Fee = ticket.EC_Fee;
    ticket.TotalPrice = ticket.Price == 0 ? 0 : ticket.Fees_Type == 0
      ? ticket.Price + ticket.EC_Fee - ticket.T_Discount
      : ticket.Price - ticket.T_Discount;
  }

  $scope.onPriceBlur = function (ticket) {
    ticket.Price = ticket.Price < 0 ? 0 : ticket.Price > 10000000 ? 9999999 : ticket.Price;
    ticket.T_Discount = ticket.T_Discount < 0 ? 0 : ticket.T_Discount > ticket.Price ? ticket.Price : ticket.T_Discount;
    $scope.onPriceChange(ticket);
  }

  $scope.deleteTicket = function (ticket) {
    var idx = $scope.eventInfo.TicketList.indexOf(ticket);
    if (idx >= 0)
      $scope.eventInfo.TicketList.splice(idx, 1);
  }

  $scope.addVarCharge = function () {
    var vcharge = {
      VariableId: 0,
      EventId: 0,
      VariableDesc: null,
      Price: 0.0,
      Optional: false
    }
    $scope.eventInfo.VariableChargesList.push(vcharge);
  }

  $scope.deleteVarCharge = function (vcharge) {
    var idx = $scope.eventInfo.VariableChargesList.indexOf(vcharge);
    if (idx >= 0)
      $scope.eventInfo.VariableChargesList.splice(idx, 1);
  }

  $scope.saveEvent = function () {
    $scope.eventInfo.EventStatus = "Save";
    console.debug($scope.eventInfo);
    $scope.sendEvent();
  }

  $scope.previewEvent = function () {
    console.debug($scope.eventInfo);
  }

  $scope.publishEvent = function () {
    $scope.eventInfo.EventStatus = "Live";
    console.debug($scope.eventInfo);
    $scope.sendEvent();
  }

  $scope.sendEvent = function () {
      console.log("Send event called");
    $scope.eventInfo.TicketList.forEach(function (ticket, i, arr) {
      ticket.Sale_Start_Date = ticket.localSaleStartDate == null ? null : formatDateTime(ticket.localSaleStartDate);
      ticket.Sale_End_Date = ticket.localSaleEndDate == null ? null : formatDateTime(ticket.localSaleEndDate);
      ticket.Hide_Untill_Date = ticket.localHideUntillDate == null ? null : formatDateTime(ticket.localHideUntillDate);
      ticket.Hide_After_Date = ticket.localHideAfterDate == null ? null : formatDateTime(ticket.localHideAfterDate);
    });
    var startingDate = $scope.startingDate.setMinutes($scope.selectedstartTime.Minutes);
    var endingDate = $scope.endingDate.setMinutes($scope.selectedendTime.Minutes);
    $scope.eventInfo.DateInfo.Frequency = $scope.occurence;
    $scope.eventInfo.DateInfo.StartDateTime = formatDateTime(new Date(startingDate));
    $scope.eventInfo.DateInfo.EndDateTime = formatDateTime(new Date(endingDate));
    $scope.eventInfo.CurrentEventType = null;
    $scope.eventInfo.CurrentEventCategory = null;
    $scope.eventInfo.CurrentEventSubCategory = null;
    $scope.eventInfo.CurrentOrganizer = null;
    $scope.eventInfo.CurrentInternalOrganizer = null;
    var data = {
      json: angular.toJson($scope.eventInfo)
    };
    $http.post('/eventmanagement/saveevent', data).then(function (response) {
      $scope.eventInfo = response.data;
      if ($scope.eventInfo.ErrorEvent)
        $scope.prepareEventInfo();
      else
        $window.location.href = link_publish + "?EventId=" + $scope.eventInfo.EventID;
    });
  }

  $scope.UploadFiles = function (files, callback) {
    var fd = new FormData();
    for (var i = 0; i < files.length; i++) {
      fd.append('files', files[i]);
    }
    $http.post('/imageAPI/UploadImages', fd, {
      transformRequest: angular.identity,
      headers: { 'Content-Type': undefined }
    }).then(function (response) {
      var images = response.data;
      callback(images);
    });
  }

  $scope.DeleteFile = function (image) {
    var data = {
      json: angular.toJson(image)
    };
    console.debug(data);
    $http.post('/imageAPI/DeleteImage', data).then(function (response) {
    });
  }

  $scope.uploadBGImage = function (event) {
    var files = event.target.files;

    $scope.UploadFiles(files, function (images) {
      if ((images != null) && (images.length > 0)) {
        var oldimage = null;
        if ($scope.eventInfo.BGImage.ImageUrl)
          oldimage = $scope.eventInfo.BGImage;
        $scope.eventInfo.BGImage = images[0];
        if (oldimage != null)
          $scope.DeleteFile(oldimage);
      }
    });
  };

  $scope.onBGColorChange = function () {
    $scope.clearImage($scope.eventInfo.BGImage);
  }

  $scope.deleteEventImage = function (image) {
    var idx = $scope.eventInfo.EventImages.indexOf(image);
    if (idx >= 0)
      $scope.eventInfo.EventImages.splice(idx, 1);
    $scope.DeleteFile(image);
  }

  $scope.uploadEventImage = function (event) {
    var files = event.target.files;

    $scope.UploadFiles(files, function (images) {
      if ((images != null) && (images.length > 0)) {
        Array.prototype.push.apply($scope.eventInfo.EventImages, images);
      }
    });
  }

  $scope.uploadOrganizerImage = function (event) {
    var files = event.target.files;

    $scope.UploadFiles(files, function (images) {
      if ((images != null) && (images.length > 0)) {
        var oldimage = null;
        if (($scope.eventInfo.CurrentOrganizer.Image != null) && ($scope.eventInfo.CurrentOrganizer.Image.ImageUrl))
          oldimage = $scope.eventInfo.CurrentOrganizer.Image;
        $scope.eventInfo.CurrentOrganizer.Image = images[0];
        if (oldimage != null)
          $scope.DeleteFile(oldimage);
      }
    });
  }

  $scope.deleteOrganizerImage = function () {
    console.debug($scope.eventInfo.CurrentOrganizer.Image);
    $scope.clearImage($scope.eventInfo.CurrentOrganizer.Image);
    console.debug($scope.eventInfo.CurrentOrganizer.Image);
  }

  $scope.clearImage = function (image) {
    if (image != null) {
      if (image.ImageUrl)
        $scope.DeleteFile(image);
      image.Filename = "";
      image.ImageUrl = "";
      image.ImageType = 0;
      image.ContentType = "";
    }
  }

}]);

createEventApp.directive('ngInitial', function () {
  return {
    restrict: 'A',
    controller: [
      '$scope', '$element', '$attrs', '$parse', function ($scope, $element, $attrs, $parse) {
        var getter, setter, val;
        val = $attrs.ngInitial || $attrs.value;
        getter = $parse($attrs.ngModel);
        setter = getter.assign;
        setter($scope, val);
      }
    ]
  };
});

createEventApp.directive('numbersOnly', function () {
  return {
    require: 'ngModel',
    link: function (scope, element, attr, ngModelCtrl) {
      function fromUser(number) {
        if (number) {
          var transformedInput = number.replace(/[^0-9]/g, '');

          if (transformedInput !== number) {
            ngModelCtrl.$setViewValue(transformedInput);
            ngModelCtrl.$render();
          }
          return transformedInput;
        }
        return undefined;
      }
      ngModelCtrl.$parsers.push(fromUser);
    }
  };
});

createEventApp.directive('googleplace', function () {
  return {
    require: 'ngModel',
    link: function (scope, element, attrs, model) {
      var options = {
        types: [],
        componentRestrictions: {}
      };
      scope.gPlace = new google.maps.places.Autocomplete(element[0], options);

      google.maps.event.addListener(scope.gPlace, 'place_changed', function () {
        scope.$apply(function () {
          model.$setViewValue(element.val());
        });
      });
    }
  };
});

createEventApp.directive('customOnChange', function () {
  return {
    restrict: 'A',
    link: function (scope, element, attrs) {
      var onChangeHandler = scope.$eval(attrs.customOnChange);
      element.bind('change', onChangeHandler);
    }
  };
});

createEventApp.controller('CreateEventHeaderController', ['$scope', function ($scope, $http) {
  $scope.showAccountDiv = function () {

    if ($('.myAccountDiv').is(':visible')) {
      $('.down').text("keyboard_arrow_down");
      $(' .myAccountDiv').hide();
      $(".accountMainDiv").css("background-color", "unset");
      $(".accountMainDiv").css("box-shadow", "unset");
    }
    else {
      $(".accountMainDiv").css("background-color", "white");
      $(".accountMainDiv").css("box-shadow", "0 2px 5px 0 rgba(0, 0, 0, 0.26)");
      $('.down').text("keyboard_arrow_up")
      $(' .myAccountDiv').show();
    }

  }

}]);

createEventApp.directive('dragDropElements', function ($compile) {
    return {
        restrict: 'A',
        transclude: true,
        template: '<div class="something" ng-transclude> This is my directive content</div>',
        link: function (scope, element, attrs) {
            // All dom manipulation should be in link function for better approach and making segreggation.

            scope.handleDragObjReference1;
            scope.handleDragObjReference2;
            scope.handleDragObjReference3;
            // scope.dragSrcEl;
            scope.element = element;

            scope.element.on('dragstart', function (event) {

                dragSrcEl = element;
                console.log(dragSrcEl);
                scope.handleDragObjReference1 = false;
                scope.handleDragObjReference2 = true;
                scope.handleDragObjReference3 = false;

                event.originalEvent.dataTransfer.effectAllowed = 'move';
                event.originalEvent.dataTransfer.setData('text/html', element.html());
            });

            scope.element.on('dragover', function (event) {
                console.log('flag:dragover');

                if (event.preventDefault) {
                    event.preventDefault();
                }

                event.originalEvent.dataTransfer.dropEffect = 'move';
                return false;
            });

            scope.element.on('drop', function (event) {
                console.log("Drop Element: ", element);
                console.log('flag:drop');
                console.log(element);
                //    console.log("DragDrcElement");
                //   console.log(dragSrcEl);

                //   if (dragSrcEl != scope.element.parent().parent().parent().parent() && scope.handleDragObjReference1 == false && scope.handleDragObjReference2 == true && scope.handleDragObjReference3 == false) 
                {
                    //dragSrcEl.html($compile(element.parent().parent().parent().parent().html())(scope));

                    //element.parent().parent().parent().parent().html($compile(event.originalEvent.dataTransfer.getData('text/html'))(scope));
                   // dragSrcEl.html("");
                    dragSrcEl.html(element.html());
                    element.html(event.originalEvent.dataTransfer.getData('text/html'));
                    $compile(element.contents())(scope);
                    $compile(dragSrcEl.contents())(scope);
                }
                if (event.stopPropagation) {
                    event.stopPropagation();
                }
                scope.$apply();
                $(".duplicate").children(".md-container").empty();
                $(".radio").children(".md-container").empty();

                return false;

            });
        }
    };
});


function formatAMPM(date) {
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var ampm = hours >= 12 ? 'PM' : 'AM';
  hours = hours % 12;
  hours = hours ? hours : 12;
  minutes = minutes < 30 ? '00' : '30';
  var strTime = hours + ':' + minutes + ' ' + ampm;
  return strTime;
}

function formatDateTime(date) {
  var year = date.getFullYear();
  var month = date.getMonth() + 1;
  var day = date.getDate();
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var res = "" + year + "-" + (month < 10 ? "0" : "") + month + "-" + (day < 10 ? "0" : "") + day + "T" + (hours < 10 ? "0" : "") + hours + ":" + (minutes < 10 ? "0" : "") + minutes + ":00";
  return res;
}

