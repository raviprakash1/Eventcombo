eventComboApp.service("organizerService", function ($http) {

    this.getOrganizerEvent = function (isUE, oId) {
        var response = $http({
            method: "get",
            datatype: "data.json",
            url: "/OrganizerInfo/OrganizerEvent",
            params: {
                isUpcomingEvent: isUE,
                organizerId: oId
            }
        });
        return response;
    };
});
var dates = [
    'Today',
    'Tomorrow',
    'This Week',
    'This Weekend',
    'Next Week',
    'This Month',
    'Custom Date',
    'All Dates'
];