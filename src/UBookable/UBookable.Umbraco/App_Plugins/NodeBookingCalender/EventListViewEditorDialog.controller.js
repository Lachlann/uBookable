var eventslistapp =  angular.module('umbraco')
    .controller('EventListViewEditorDialog.Controller',
    function ($scope, $filter, $http, notificationsService) {

        var daySelected = new Date($scope.dialogData.year, $scope.dialogData.month, $scope.dialogData.day);
        $scope.DayKey = moment(daySelected).format('YYYYMMDD');
        var todaysBookings = $filter('filter')($scope.dialogData.BookingData.data, { Date: $scope.DayKey });
    	$scope.model = {
    		DateTitle: moment(daySelected).format('MMMM Do YYYY'),
    		Bookings: todaysBookings
    	}
    	$scope.getByDayKey = function (date) {
    	    return function (day) {
    	        return day.Date === date;
    	    }
    	}

    	$scope.Approve = function (bookingId) {
    	    $http({
    	        url: "/umbraco/api/booking/approvebooking",
    	        method: "PUT",
    	        params: { bookingid: bookingId }
    	    }).then(
                function successCallback(response) {
                    var newlyapproved = $filter('filter')($scope.model.Bookings[0].Bookings, { BookingID: response.data.BookingID });
                    newlyapproved[0].Approved = true;
    	            notificationsService.success("Event approved", "This event has been approved");
    	        },
                function errorCallback(response) {
                    notificationsService.error("Event approval failed", "Something went wrong, your event has not been approved.");
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
                    var newlycanceled = $filter('filter')($scope.model.Bookings[0].Bookings, { BookingID: response.data.BookingID });
                    newlycanceled[0].Cancelled = true;
                    notificationsService.success("Event approved", "This event has been canceled");
                },
                function errorCallback(response) {
                    notificationsService.error("Event cancelation failed", "Something went wrong, your event has not been approved.");
                }
            );
    	}
    });



eventslistapp.filter('fixDate', function () {
    return function (input) {
        return moment(new Date(parseInt(input.replace('/Date(', '').replace(')/', ''), 10))).format('H:mm');
    }
});