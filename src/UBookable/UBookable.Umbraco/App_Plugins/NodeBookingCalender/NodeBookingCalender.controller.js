var itembasedbookingApp = angular.module("umbraco");
app.requires.push('500tech.simple-calendar');
itembasedbookingApp.controller("UBookable.NodeBookingCalender", function ($scope, $http, $routeParams, dialogService) {
    var nodeId = $routeParams.id;
   /* $http({
        url: "/umbraco/api/booking/getallbookingsbynodeid",
        method: "GET",
        params: { nodeId: nodeId }
    }).then(function successCallback(response) {
        console.log(response);
        $scope.bookings = response.data;
    });*/
    $scope.calendarOptions = {
        defaultDate: new Date(),
        minDate: new Date(),
        maxDate: new Date([2020, 12, 31]),
        dayNamesLength: 1, // How to display weekdays (1 for "M", 2 for "Mo", 3 for "Mon"; 9 will show full day names; default is 1)
        multiEventDates: false, // Set the calendar to render multiple events in the same day or only one event, default is false
        maxEventsPerDay: 3, // Set how many events should the calendar display before showing the 'More Events' message, default is 3;
        eventClick: function (data) {
            console.log(data)
            dialogService.open({
                template: '/App_Plugins/NodeBookingCalender/templates/event-list-view.html',
                show: true,
                dialogData: data,
                callback: function (value) {
                    console.log(value);
                }
            });
        },
        dateClick: function (data) {
        dialogService.open({
            template: '/App_Plugins/NodeBookingCalender/templates/new-event-view.html',
            show: true,
            dialogData: data,
            callback: done
        });
    }
    };
    


    $scope.events = [
        { title: '4 bookings', date: new Date([2016, 6, 15]) },
        { title: '12 bookings', date: new Date([2016, 6, 16]) }
    ];
});

itembasedbookingApp.filter('FixSerialisedDate', function () {
    return function (item) {
        return new Date(parseInt(item.replace('/Date(', '').replace(')/', ''), 10));
    };
})