var eventslistapp =  angular.module('umbraco')
    .controller('EventListViewEditorDialog.Controller',
    function ($scope, $filter, $http, notificationsService) {
        $scope.showBookings = true;
        $scope.showAddNew = false;
        $scope.autoApproved = true;
        $scope.hideCancelled = true;
        $scope.hideApproved = false;


        var daySelected = new Date($scope.dialogData.year, $scope.dialogData.month, $scope.dialogData.day);
        $scope.DayKey = moment(daySelected).format('YYYYMMDD');
        var todayData = $filter('filter')($scope.dialogData.BookingData.data, { Date: $scope.DayKey })
        var todaysBookings = (todayData.length > 0) ? todaysBookings = todayData[0].Bookings : [];
        $scope.model = {
            DateTitle: moment(daySelected).format('MMMM Do YYYY'),
            Bookings: todaysBookings
        }
        $http({
            url: "/umbraco/api/booking/gettimeslotsbynodeid",
            method: "GET",
            params: { nodeId: $scope.dialogData.nodeId, dayRequest: daySelected.getTime() }
        }).then(
            function successCallback(response) {
                console.log(response);
                $scope.DateOptions = response.data
            },
            function errorCallback(response) {
                notificationsService.error("Failed to retireive time slots for this date", "Please contact your system admin");
            }
        );

    	$scope.getByDayKey = function (date) {
    	    return function (day) {
    	        return day.Date === date;
    	    }
    	}
    	$scope.submitNewBooking = function () {
    	    var bookingName = $scope.newBookingName;
    	    var timeslotsArray = $scope.timeslots.split('#');
    	    var start = timeslotsArray[0].split(':');
    	    var end = timeslotsArray[1].split(':');
    	    var startTime = new Date(daySelected.setHours(start[0], start[1]));
    	    var endTime = new Date(daySelected.setHours(end[0], end[1]));

    	    uBookable.SaveBookingAndBooker($scope.dialogData.nodeId, startTime, endTime, $scope.newBookingName, $scope.autoApproved).done(function (booking) {
    	        booking.Name = bookingName; // bit of a hack but saves hving to do it on the server.
    	        $scope.model.Bookings.push(booking);
    	        notificationsService.success("New booking added", "The new booking has been successfully added");
    	    });
    	}
    	$scope.Approve = function (bookingId) {
    	    $http({
    	        url: "/umbraco/api/booking/approvebooking",
    	        method: "PUT",
    	        params: { bookingid: bookingId }
    	    }).then(
                function successCallback(response) {
                    var newlyapproved = $filter('filter')($scope.model.Bookings, { BookingID: response.data.BookingID });
                    newlyapproved[0].Approved = true;
                    notificationsService.success("Booking approved", "This event has been approved");
    	        },
                function errorCallback(response) {
                    notificationsService.error("Booking approval failed", "Something went wrong, your event has not been approved.");
                }
            );
    	}
    	$scope.Cancel = function (bookingId) {
    	    $http({
    	        url: "/umbraco/api/booking/cancelbooking",
    	        method: "PUT",
    	        params: { bookingid: bookingId }
    	    }).then(
                function successCallback(response) {
                    var newlycanceled = $filter('filter')($scope.model.Bookings, { BookingID: response.data.BookingID });
                    newlycanceled[0].Cancelled = true;
                    notificationsService.success("Booking approved", "This event has been canceled");
                },
                function errorCallback(response) {
                    notificationsService.error("Booking cancelation failed", "Something went wrong, your event has not been approved.");
                }
            );
    	}
    });

eventslistapp.filter('fixDate', function () {
    return function (input) {
        return moment(new Date(parseInt(input.replace('/Date(', '').replace(')/', ''), 10))).format('H:mm');
    }
});
eventslistapp.filter('toTransferFormat', function () {
    return function (input) {
        return new moment(Date(parseInt(input.replace('/Date(', '').replace(')/', ''), 10))).toString();
    }
});