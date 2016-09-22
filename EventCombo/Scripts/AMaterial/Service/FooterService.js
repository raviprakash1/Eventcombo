eventComboApp.service("footerService", function ($http) {

    this.getCategory = function () {
        return $http.get("/NotificationAPI/GetCategory");
    };

    this.getSubCategory = function (category) {
        var response = $http({
            method: "get",
            url: "/NotificationAPI/GetSubCategory",
            params: {
                category: category
            }
        });
        return response;
    }

    this.sendContactMessage = function (contactMessageViewModel) {
        var response = $http({
            method: "post",
            url: "/NotificationAPI/SendContactEmail",
            data: JSON.stringify(contactMessageViewModel),
            dataType: "json"
        });
        return response;
    }

});