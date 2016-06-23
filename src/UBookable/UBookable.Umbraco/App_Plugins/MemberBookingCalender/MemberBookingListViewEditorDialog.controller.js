﻿var eventslistapp =  angular.module('umbraco')
    .controller('MemberBookingListViewEditorDialog.Controller',
    function ($scope, $filter, $http, notificationsService) {
        $scope.showBookings = true;
        $scope.showAddNew = false;
        $scope.autoApproved = true;
        $scope.hideCancelled = true;
        $scope.hideApproved = false;
        $scope.memberSearching = null;


        var daySelected = new Date($scope.dialogData.year, $scope.dialogData.month, $scope.dialogData.day);
        console.log(daySelected);
        $scope.DayKey = moment(daySelected).format('YYYYMMDD');
        var todayData = $filter('filter')($scope.dialogData.BookingData.data, { Date: $scope.DayKey })
        var todaysBookings = (todayData.length > 0) ? todaysBookings = todayData[0].Bookings : [];
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
                    notificationsService.success("Booking approved", "This booking has been canceled");
                },
                function errorCallback(response) {
                    notificationsService.error("Booking cancelation failed", "Something went wrong, your event has not been approved.");
                }
            );
    	}
    	$scope.Delete = function (bookingId) {
    	    var r = confirm("Are you sure you want to permanently delete this booking");
    	    if (r === true) {
    	        $http({
    	            url: "/umbraco/api/booking/deletebooking",
    	            method: "DELETE",
    	            params: { bookingid: bookingId }
    	        }).then(
                    function successCallback(response) {

                        var pos = $scope.model.Bookings.map(function (e) { return e.BookingID; }).indexOf(bookingId);
                        $scope.model.Bookings.splice(pos, 1);
                   
                        notificationsService.success("Booking deleted", "This booking has been deleted permenantly");
                    },
                    function errorCallback(response) {
                        notificationsService.error("Booking deletion failed", "Something went wrong, your event has not been approved.");
                    }
                );
    	    }

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