var itembasedbookingApp = angular.module("umbraco");
itembasedbookingApp.requires.push('500tech.simple-calendar');

itembasedbookingApp.controller("UBookable.MemberBookingCalender", function ($scope, $filter, $http, $routeParams, dialogService) {
    var thisMemberId = $routeParams.id;
    getBookingsById(thisMemberId);

    function getBookingsById(memberId) {
        $http({
            url: "/umbraco/api/booking/getallbookingsbymemberid",
            method: "GET",
            params: { memberKey: memberId }
        }).then(function successCallback(response) {
            console.log(response);
            $scope.BookingData = response;
            initCal(response);
        });
    }
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
            data["nodeId"] = thisMemberId;
            data["BookingData"] = $scope.BookingData;
            dialogService.open({
                template: '/App_Plugins/MemberBookingCalender/templates/booking-list-view.html',
                show: true,
                dialogData: data,
                closeCallback: function (value) {
                    getBookingsById(thisMemberId);
                }
            });
        },
        dateClick: function (data) {
            data["nodeId"] = thisMemberId;
            data["BookingData"] = $scope.BookingData;
            dialogService.open({
                template: '/App_Plugins/MemberBookingCalender/templates/booking-list-view.html',
                show: true,
                dialogData: data,
                closeCallback: function (value) {
                    getBookingsById(memberId);
                }
            });
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