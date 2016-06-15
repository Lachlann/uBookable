var itembasedbookingApp = angular.module("umbraco");
itembasedbookingApp.requires.push('500tech.simple-calendar');

itembasedbookingApp.controller("UBookable.NodeBookingCalender", function ($scope, $filter, $http, $routeParams, dialogService) {
    var nodeId = $routeParams.id;
    $http({
        url: "/umbraco/api/booking/getallbookingsbynodeid",
        method: "GET",
        params: { nodeId: nodeId }
    }).then(function successCallback(response) {
        $scope.BookingData = response;
        initCal(response);
    });

    function initCal(eventData) {
        $scope.events = [];
        for (var i = 0, length = eventData.data.length; i < length ; i++)
        {
            var thisGroupCount = eventData.data[i].Count;
            for (var j = 0, lengthj = eventData.data[i].Bookings.length; j < lengthj ; j++) {
                eventData.data[i].Bookings[j]["Count"] = thisGroupCount;
                $scope.events.push($filter('ConvertResponseToCalData')(eventData.data[i].Bookings[j]));
            }
        }
    }


    $scope.calendarOptions = {
        defaultDate: new Date(),
        minDate: new Date([2000, 12, 31]),
        maxDate: new Date([2020, 12, 31]),
        dayNamesLength: 1, // How to display weekdays (1 for "M", 2 for "Mo", 3 for "Mon"; 9 will show full day names; default is 1)
        multiEventDates: false, // Set the calendar to render multiple events in the same day or only one event, default is false
        maxEventsPerDay: 3, // Set how many events should the calendar display before showing the 'More Events' message, default is 3;
        eventClick: function (data) {
            data["nodeId"] = nodeId;
            data["BookingData"] = $scope.BookingData;
            dialogService.open({
                template: '/App_Plugins/NodeBookingCalender/templates/event-list-view.html',
                show: true,
                dialogData: data,
                callback: function (value) {
                    
                }
            });
        },
        dateClick: function (data) {
        }
    };
    



});


itembasedbookingApp.filter('ConvertResponseToCalData', function ($filter) {
    return function (item) {
        return { title: item.Count+' bookings', date: $filter('FixSerialisedDate')(item.StartDate) };
    };
})

itembasedbookingApp.filter('FixSerialisedDate', function ($filter) {
    return function (item) {
        return new Date(parseInt(item.replace('/Date(', '').replace(')/', ''), 10));
    };
})