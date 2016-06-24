var test;
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
    $scope.tempDateInfo = {};
    $scope.isPrivateEvent = false;
    $scope.includeSocial = 0;
    $scope.minDate = new Date();
    $scope.minDate.setHours(0, 0, 0, 0);
    $scope.formValidation = false;
    $scope.gPlace;
    console.log("window width: ", $(window).width());
    if ($(window).width() > 768) {
      console.log("In iF check");
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
      $scope.onShowDateTimeDialog(false);
      if ($scope.eventInfo.VariableChargesList.length <= 0)
        $scope.addVarCharge();
      $scope.eventInfo.TicketList.forEach(function (ticket, i, arr) {
        ticket.localSaleStartDate = !ticket.Sale_Start_Date ? null : new Date(ticket.Sale_Start_Date);
        ticket.localSaleEndDate = !ticket.Sale_End_Date ? null : new Date(ticket.Sale_End_Date);
        ticket.localHideUntilDate = !ticket.Hide_Untill_Date ? null : new Date(ticket.Hide_Untill_Date);
        ticket.localHideAfterDate = ticket.Hide_After_Date ? null : new Date(ticket.Hide_After_Date);
      });
    }

    $scope.onShowDateTimeDialog = function (showDialog) {
      console.log($scope.eventInfo.DateInfo.StartDateTime);
      var startDateTime = new Date($scope.eventInfo.DateInfo.StartDateTime);
      if ($scope.eventInfo.DateInfo.StartDateTime.length <= 19)
        startDateTime = new Date(startDateTime.getTime() + startDateTime.getTimezoneOffset() * 60000);
      console.log(startDateTime);
      var endDateTime = new Date($scope.eventInfo.DateInfo.EndDateTime);
      if ($scope.eventInfo.DateInfo.EndDateTime.length <= 19)
        endDateTime = new Date(endDateTime.getTime() + endDateTime.getTimezoneOffset() * 60000);
      var time = formatAMPM(startDateTime);
      var ctime = $scope.eventInfo.DateInfo.TimeList.filter(function (obj) { return obj.TimeString == time; });
      console.debug(ctime);
      if (ctime.length > 0)
        $scope.tempDateInfo.selectedstartTime = ctime[0];
      else
        $scope.tempDateInfo.selectedstartTime = null;
      time = formatAMPM(endDateTime);
      ctime = $scope.eventInfo.DateInfo.TimeList.filter(function (obj) { return obj.TimeString == time; });
      if (ctime.length > 0)
        $scope.tempDateInfo.selectedendTime = ctime[0];
      else
        $scope.tempDateInfo.selectedendTime = null;
      $scope.tempDateInfo.startingDate = new Date(startDateTime.getFullYear(), startDateTime.getMonth(), startDateTime.getDate());
      $scope.tempDateInfo.endingDate = new Date(endDateTime.getFullYear(), endDateTime.getMonth(), endDateTime.getDate());
      $scope.tempDateInfo.occurence = $scope.eventInfo.DateInfo.Frequency;
      $scope.tempDateInfo.Weekdays = $scope.eventInfo.DateInfo.Weekdays.slice();
      $scope.tempDateInfo.TimeZone = $scope.eventInfo.TimeZone;
      if ($scope.eventInfo.DateInfo.Frequency == "Single") {
        $scope.dateTab = 0;
        if (!$scope.eventInfo.DateInfo.IsNewDate)
          $scope.showDates(false);
      }
      else {
        $scope.dateTab = 1;
        if (!$scope.eventInfo.DateInfo.IsNewDate)
          $scope.showMultipleDates(false);
      }
      $scope.showDateTimeDialog = showDialog;
    }

    $scope.saveDateTime = function () {
      $scope.eventInfo.DateInfo.IsNewDate = false;
      $scope.eventInfo.DateInfo.Frequency = $scope.tempDateInfo.occurence;
      $scope.eventInfo.TimeZone = $scope.tempDateInfo.TimeZone;

      $scope.eventInfo.DateInfo.StartDateTime = $scope.getDateTime($scope.tempDateInfo.startingDate, $scope.tempDateInfo.selectedstartTime);
      $scope.eventInfo.DateInfo.EndDateTime = $scope.getDateTime($scope.tempDateInfo.endingDate, $scope.tempDateInfo.selectedendTime);

      if ($scope.tempDateInfo.occurence == "Weekly")
        $scope.eventInfo.DateInfo.Weekdays = $scope.tempDateInfo.Weekdays.slice();
      else
        $scope.eventInfo.DateInfo.Weekdays = [];
    }

    $scope.showDates = function (save) {
      $scope.dateInfoTouched = true;
      $scope.tempDateInfo.occurence = "Single";
      var check = $scope.checkTempDateInfo();
      if (check.timezone || check.singleDates)
        return;
      $scope.showDateTimeDialog = false;
      var startingDate = ($scope.tempDateInfo.startingDate.getMonth() + 1) + "/" + $scope.tempDateInfo.startingDate.getDate() + "/" + $scope.tempDateInfo.startingDate.getFullYear();
      var endingDate = ($scope.tempDateInfo.endingDate.getMonth() + 1) + "/" + $scope.tempDateInfo.endingDate.getDate() + "/" + $scope.tempDateInfo.endingDate.getFullYear();
      $scope.selectedDateTimeText = startingDate + " " + $scope.tempDateInfo.selectedstartTime.TimeString + " To " + endingDate + " " + $scope.tempDateInfo.selectedendTime.TimeString;
      if (save)
        $scope.saveDateTime();
    }

    $scope.showMultipleDates = function (save) {
      $scope.dateInfoTouched = true;
      var check = $scope.checkTempDateInfo();
      if (check.weekdays || check.timezone || check.multiDates)
        return;
      $scope.showDateTimeDialog = false;

      if ($scope.tempDateInfo.occurence == "Weekly") {
        var alldays = "";
        for (var day = 0; day < $scope.tempDateInfo.Weekdays.length; day++) {
          alldays += $scope.tempDateInfo.Weekdays[day] + ",";
        }
        alldays = alldays.substr(0, alldays.length - 1);
        $scope.selectedDateTimeText = "Days (" + alldays + ") Time " + $scope.tempDateInfo.selectedstartTime.TimeString + " To " + $scope.tempDateInfo.selectedendTime.TimeString;
      }
      else if ($scope.tempDateInfo.occurence == "Daily") {
        $scope.selectedDateTimeText = "Daily Time " + $scope.tempDateInfo.selectedstartTime.TimeString + " To " + $scope.tempDateInfo.selectedendTime.TimeString;
      }
      else {
        var startingDate = ($scope.tempDateInfo.startingDate.getMonth() + 1) + "/" + $scope.tempDateInfo.startingDate.getDate() + "/" + $scope.tempDateInfo.startingDate.getFullYear();
        var endingDate = ($scope.tempDateInfo.endingDate.getMonth() + 1) + "/" + $scope.tempDateInfo.endingDate.getDate() + "/" + $scope.tempDateInfo.endingDate.getFullYear();
        $scope.selectedDateTimeText = "Monthly " + startingDate + " " + $scope.tempDateInfo.selectedstartTime.TimeString + " To " + endingDate + " " + $scope.tempDateInfo.selectedendTime.TimeString;
      }
      if (save)
        $scope.saveDateTime();
    }

    $scope.onCategoryChange = function () {
      $scope.subCategories = [];
      $scope.eventInfo.EventCategoryList.forEach(function (cat, i, arr) {
        if (cat.EventCategoryId == $scope.eventInfo.EventCategoryID)
          $scope.subCategories = cat.SubCategories;
      });
    }

    $scope.startDateChange = function () {
      if ($scope.tempDateInfo.endingDate < $scope.tempDateInfo.startingDate)
        $scope.tempDateInfo.endingDate = new Date($scope.stempDateInfo.tartingDate.getFullYear(),
          $scope.tempDateInfo.startingDate.getMonth(), $scope.tempDateInfo.startingDate.getDate());
    }

    $scope.endDateChange = function () {
      if ($scope.tempDateInfo.endingDate < $scope.tempDateInfo.startingDate)
        $scope.tempDateInfo.startingDate = new Date($scope.tempDateInfo.endingDate.getFullYear(),
          $scope.tempDateInfo.endingDate.getMonth(), $scope.tempDateInfo.endingDate.getDate());
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
      $scope.OrganizerForm.$setPristine();
      $scope.OrganizerForm.$setUntouched();
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
        $scope.OrganizerForm.$setUntouched();
      }
    }

    $scope.saveNewOrganizer = function () {
      $scope.OrganizerForm.$setSubmitted();
      if (isEmptyOrSpaces($scope.eventInfo.CurrentOrganizer.Orgnizer_Name) || isEmptyOrSpaces($scope.eventInfo.CurrentOrganizer.Organizer_Email))
        return;
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
      $scope.organizerEditState = "Saved";

    }

    $scope.saveEditedOrganizer = function () {
      $scope.OrganizerForm.$setSubmitted();
      if (isEmptyOrSpaces($scope.eventInfo.CurrentOrganizer.Orgnizer_Name) || isEmptyOrSpaces($scope.eventInfo.CurrentOrganizer.Organizer_Email)) {
        return;
      }
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
      };
      $scope.organizerEditState = "Saved";
    }

    $scope.getTotalTickets = function () {
      var total = 0;
      if ($scope.eventInfo && $scope.eventInfo.TicketList)
        $scope.eventInfo.TicketList.forEach(function (t, i, arr) {
          var val = parseInt(t.Qty_Available);
          total = total + (isNaN(val) ? 0 : val);
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
        localHideUntilDate: null,
        localHideAfterDate: null,
        localSaleStartTime: null,
        localSaleEndTime: null,
        localHideUntilTime: null,
        localHideAfterTime: null
      }
      $scope.onPriceChange(newticket);
      $scope.eventInfo.TicketList.push(newticket);
    }

    $scope.onPriceChange = function (ticket) {
      var price = isNaN(ticket.Price) ? 0 : ticket.Price;
      var discount = isNaN(ticket.T_Discount) ? 0 : ticket.T_Discount;
      ticket.EC_Fee = price == 0 ? 0 : price * $scope.eventInfo.FeeStruct.FS_Percentage / 100 + $scope.eventInfo.FeeStruct.FS_Amount;
      ticket.Customer_Fee = ticket.EC_Fee;
      ticket.TotalPrice = price == 0 ? 0 : ticket.Fees_Type == 0
        ? price + ticket.EC_Fee - discount
        : price - discount;
    }

    $scope.onPriceBlur = function (ticket) {
      ticket.Price = (ticket.Price < 0) || isNaN(ticket.Price) ? 0 : ticket.Price > 10000000 ? 9999999 : ticket.Price;
      ticket.T_Discount = isNaN(ticket.T_Discount) || (ticket.T_Discount < 0) ? 0 : ticket.T_Discount > ticket.Price ? ticket.Price : ticket.T_Discount;
      $scope.onPriceChange(ticket);
    }

    $scope.onQtyBlur = function (ticket) {
      ticket.Qty_Available = isNaN(ticket.Qty_Available) ? 0 : ticket.Qty_Available;
      console.log(ticket.Qty_Available);
    }

    $scope.deleteTicket = function (ticket) {
      var idx = $scope.eventInfo.TicketList.indexOf(ticket);
      if (idx >= 0)
        $scope.eventInfo.TicketList.splice(idx, 1);
    }

    $scope.onExpandTicketClick = function (ticket, form) {
      console.debug(form);
      if (!ticket.expandedOptions) {
        ticket.expandedOptions = true;
        return;
      }

      ticket.ticketValidation = true;

      var hideDatesValid = ((ticket.useUntilDate == 1) && ticket.localHideUntilDate && !form.ticketHideUntilDate.$error.mindate) ||
        ((ticket.useAfterDate == 1) && ticket.localHideAfterDate && !form.ticketHideAfterDate.$error.mindate);

      ticket.expandedOptions = form.ticketStartDate.$error.mindate ||
        form.ticketEndDate.$error.mindate ||
        ((ticket.T_AutoSechduleType == 1) && !hideDatesValid);
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
      console.debug($scope.MainForm.eventType);
    }

    $scope.publishEvent = function () {
      $scope.eventInfo.EventStatus = "Live";
      console.debug($scope.eventInfo);
      $scope.sendEvent();
    }

    $scope.getDateTime = function (date, time) {
      var cDate = date.setMinutes(!time ? 0 : time.Minutes);
      return formatDateTime(new Date(cDate));
    }


    $scope.sendEvent = function () {
      var elem = $scope.validateEvent();
      if (!elem.valid) {
        alert("Form contain invalid data. Please, check all fields.");
        return;
      }
      $scope.eventInfo.TicketList.forEach(function (ticket, i, arr) {

        ticket.Sale_Start_Date = !ticket.localSaleStartDate
          ? null : $scope.getDateTime(ticket.localSaleStartDate, ticket.localSaleStartTime);
        ticket.Sale_End_Date = !ticket.localSaleEndDate
          ? null : $scope.getDateTime(ticket.localSaleEndDate, ticket.localSaleEndTime);
        ticket.Hide_Untill_Date = !ticket.localHideUntilDate || (ticket.useUntilDate == 0)
          ? null : $scope.getDateTime(ticket.localHideUntilDate, ticket.localHideUntilTime);
        ticket.Hide_After_Date = !ticket.localHideAfterDate || (ticket.useAfterDate == 0)
          ? null : $scope.getDateTime(ticket.localHideAfterDate, ticket.localHideAfterTime);
      });
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

    $scope.validateEvent = function () {
      $scope.formValidation = true;
      elem = {
        valid: true,
        messages: []
      };
      if (!$scope.MainForm.eventTitle.$valid)
        elem.valid = false;
      console.log(elem.valid);
      if (!$scope.MainForm.eventVenueName.$valid && !$scope.eventInfo.OnlineEvent)
        elem.valid = false;
      console.log(elem.valid);
      if (!$scope.MainForm.eventAddress.$valid && !$scope.eventInfo.OnlineEvent)
        elem.valid = false;
      console.log(elem.valid);
      if (!$scope.checkDateInfo().valid)
        elem.valid = false;
      console.log(elem.valid);
      if (!$scope.eventInfo.EventTypeID)
        elem.valid = false;
      console.log(elem.valid);
      if (!$scope.eventInfo.EventCategoryID)
        elem.valid = false;
      console.log(elem.valid);
      if (!$scope.eventInfo.InternalOrganizerId)
        elem.valid = false;
      console.log(elem.valid);
      if ($scope.eventInfo.TicketList.length == 0)
        elem.valid = false;
      console.log(elem.valid);
      console.log("Start tickets");
      $scope.eventInfo.TicketList.forEach(function (ticket, i, arr) {
        var hideDatesValid = ((ticket.useUntilDate == 1) && ticket.localHideUntilDate && (ticket.localHideUntilDate >= $scope.minDate)) ||
            ((ticket.useAfterDate == 1) && ticket.localHideAfterDate && (ticket.localHideAfterDate >= $scope.minDate));

        console.log(hideDatesValid);
        console.log(ticket.localSaleStartDate);
        console.log($scope.minDate);
        console.log(ticket.T_name);

        if (!ticket.T_name ||
            (ticket.T_name.trim().length == 0) ||
            (ticket.Qty_Available <= 0) ||
            (ticket.localSaleStartDate && (ticket.localSaleStartDate < $scope.minDate)) ||
            (ticket.localSaleEndDate && (ticket.localSaleEndDate < $scope.minDate)) ||
            ((ticket.T_AutoSechduleType == 1) && !hideDatesValid))
          elem.valid = false;
        console.log(elem.valid);
      });
      return elem;
    }

    $scope.checkDateInfo = function () {
      var result = {
        valid: true,
        notselected: $scope.eventInfo.DateInfo.IsNewDate,
        weekdays: ($scope.eventInfo.DateInfo.Frequency == 'Weekly') && ($scope.eventInfo.DateInfo.Weekdays.length == 0),
        timezone: !$scope.eventInfo.TimeZone,
      };
      result.valid = !result.notselected && !result.weekdays && !result.timezone;
      return result;
    }

    $scope.checkTempDateInfo = function () {
      var result = {
        valid: true,
        notselected: $scope.tempDateInfo.IsNewDate,
        weekdays: ($scope.tempDateInfo.occurence == 'Weekly') && ($scope.tempDateInfo.Weekdays.length == 0),
        timezone: !$scope.tempDateInfo.TimeZone,
        singleDates: ($scope.tempDateInfo.occurence == 'Single') && (!$scope.MainForm.singleStartDate.$valid || !$scope.MainForm.singleEndDate.$valid),
        multiDates: ($scope.tempDateInfo.occurence == 'Monthly') && (!$scope.MainForm.multiStartDate.$valid || !$scope.MainForm.multiEndDate.$valid),
      };
      result.valid = !result.notselected && !result.weekdays && !result.timezone && !result.singleDates && !result.multiDates;
      return result;
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
      $http.post('/imageAPI/DeleteImage', data).then(function (response) {
      });
    }

    $scope.uploadBGImage = function (event) {
      var files = event.target.files;

      $scope.UploadFiles(files, function (images) {
        if (images && (images.length > 0)) {
          var oldimage = null;
          if ($scope.eventInfo.BGImage.ImageUrl)
            oldimage = $scope.eventInfo.BGImage;
          $scope.eventInfo.BGImage = images[0];
          if (oldimage)
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
        if (images && (images.length > 0)) {
          Array.prototype.push.apply($scope.eventInfo.EventImages, images);
        }
      });
    }

    $scope.uploadOrganizerImage = function (event) {
      var files = event.target.files;

      $scope.UploadFiles(files, function (images) {
        if (images && (images.length > 0)) {
          var oldimage = null;
          if ($scope.eventInfo.CurrentOrganizer.Image && ($scope.eventInfo.CurrentOrganizer.Image.ImageUrl))
            oldimage = $scope.eventInfo.CurrentOrganizer.Image;
          $scope.eventInfo.CurrentOrganizer.Image = images[0];
          if (oldimage)
            $scope.DeleteFile(oldimage);
        }
      });
    }

    $scope.deleteOrganizerImage = function () {
      $scope.clearImage($scope.eventInfo.CurrentOrganizer.Image);
    }

    $scope.clearImage = function (image) {
      if (image) {
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
        return number;
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

createEventApp.directive('positiveValidation', function () {
  return {
    restrict: 'A',
    require: 'ngModel',
    link: function ($scope, $element, $attrs, ngModel) {
      ngModel.$validators.positive = function (modelValue) {
        var val = parseInt(modelValue);
        return isNaN(val) ? false : val > 0 ? true : false;
      };
    }
  };
});

createEventApp.directive('dragDropElements', function ($compile) {
  return {
    restrict: 'A',
    transclude: true,
    template: '<div class="something" ng-transclude> This is my directive content</div>',

    link: function (scope, element, attrs) {

      scope.handleDragObjReference1;
      scope.handleDragObjReference2;
      scope.handleDragObjReference3;

      scope.element = element;

      scope.element.on('dragstart', function (event) {

        console.log("Index: ", scope.$parent.$index);
        dragSrcEl = element;
        test = element;
        console.log(dragSrcEl);
        console.log($(test).attr("ng-class"));
        scope.handleDragObjReference1 = false;
        scope.handleDragObjReference2 = true;
        scope.handleDragObjReference3 = false;

        event.originalEvent.dataTransfer.effectAllowed = 'move';
        event.originalEvent.dataTransfer.setData('text/html', element.html());
      });

      scope.element.on('dragover', function (event) {


        if (event.preventDefault) {
          event.preventDefault();
        }

        event.originalEvent.dataTransfer.dropEffect = 'move';
        return false;
      });

      scope.element.on('drop', function (event) {

        var dragElem = scope.eventInfo.TicketList[$(dragSrcEl).attr("ng-class")];
        var srcElem = scope.eventInfo.TicketList[$(element).attr("ng-class")];

        scope.eventInfo.TicketList[$(dragSrcEl).attr("ng-class")] = srcElem;
        scope.eventInfo.TicketList[$(element).attr("ng-class")] = dragElem;


        if (event.stopPropagation) {
          event.stopPropagation();
        }
        scope.$apply();

        return false;

      });
    }
  };
});


/* Variable drag drop Directive*/
createEventApp.directive('variableDragDropElements', function ($compile) {
    return {
        restrict: 'A',
        transclude: true,
        template: '<div class="something" ng-transclude> This is my directive content</div>',

        link: function (scope, element, attrs) {

            scope.handleDragObjReference1;
            scope.handleDragObjReference2;
            scope.handleDragObjReference3;

            scope.element = element;

            scope.element.on('dragstart', function (event) {

                console.log("Index: ", scope.$parent.$index);
                dragSrcEl = element;
                test = element;
                console.log(dragSrcEl);
                console.log($(test).attr("ng-class"));
                scope.handleDragObjReference1 = false;
                scope.handleDragObjReference2 = true;
                scope.handleDragObjReference3 = false;

                event.originalEvent.dataTransfer.effectAllowed = 'move';
                event.originalEvent.dataTransfer.setData('text/html', element.html());
            });

            scope.element.on('dragover', function (event) {


                if (event.preventDefault) {
                    event.preventDefault();
                }

                event.originalEvent.dataTransfer.dropEffect = 'move';
                return false;
            });

            scope.element.on('drop', function (event) {

                var dragElem = scope.eventInfo.VariableChargesList[$(dragSrcEl).attr("ng-class")];
                var srcElem = scope.eventInfo.VariableChargesList[$(element).attr("ng-class")];

                scope.eventInfo.VariableChargesList[$(dragSrcEl).attr("ng-class")] = srcElem;
                scope.eventInfo.VariableChargesList[$(element).attr("ng-class")] = dragElem;


                if (event.stopPropagation) {
                    event.stopPropagation();
                }
                scope.$apply();

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

function isEmptyOrSpaces(str) {
  return str === null || str.match(/^ *$/) !== null;
}
