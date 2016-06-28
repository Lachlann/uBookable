var eventslistapp =  angular.module('umbraco')
    .controller('NodeBookingListViewEditorDialog.Controller',
    function ($scope, $filter, $http, notificationsService) {
        $scope.showBookings = true;
        $scope.showAddNew = false;
        $scope.autoApproved = true;
        $scope.hideCancelled = true;
        $scope.hideApproved = false;
        $scope.memberSearching = null;
        $scope.modalAlerts = [];
        var pruneAlerts = function () {
            var thisMoment = moment();
            for (var i = 0, length = $scope.modalAlerts.length; i < length; i++) {
                var thisAlert = $scope.modalAlerts[i];
                if (thisAlert !== undefined && !thisAlert.Persist && thisMoment.diff(thisAlert.Time) > thisAlert.EndofLife) {
                    $scope.modalAlerts.splice(i, 1);
                    $scope.$apply();
                 }
            }
        }
        $scope.alertReviewInterval =  setInterval(pruneAlerts, 1000);
        $scope.$on("$destroy",function handleDestroyEvent() {clearInterval($scope.alertReviewInterval);});


        function getTimeSlots(nodeID, daySelected) {
            $http({
                url: "/umbraco/api/booking/gettimeslotsbynodeid",
                method: "GET",
                params: { nodeId: nodeID, dayRequest: daySelected.toISOString() }
            }).then(
                function successCallback(response) {
                    console.log(response);
                    $scope.DateOptions = response.data
                },
                function errorCallback(response) {
                    var alert = {
                        Title: "Failed to retireive time slots for this date",
                        Text: "Please check this bookable has valid settings populated. Please contact your system admin",
                        Type: "danger",
                        Time: moment(),
                        EndofLife: 5000,
                        Persist: true
                    };
                    $scope.modalAlerts.push(alert);
                    $scope.$apply();
                }
            );
        }

        var daySelected = new Date($scope.dialogData.year, $scope.dialogData.month, $scope.dialogData.day);
        console.log(daySelected);
        $scope.DayKey = moment(daySelected).format('YYYYMMDD');
        var todayData = $filter('filter')($scope.dialogData.BookingData.data, { Date: $scope.DayKey })
        var todaysBookings = (todayData.length > 0) ? todaysBookings = todayData[0].Bookings : [];
        $scope.model = {
            DateTitle: moment(daySelected).format('MMMM Do YYYY'),
            Bookings: todaysBookings
        }
        getTimeSlots($scope.dialogData.nodeId, daySelected);


    	$scope.getByDayKey = function (date) {
    	    return function (day) {
    	        return day.Date === date;
    	    }
    	}
    	
    	$scope.$watch('memberSearch', function (val) {
    	    console.log(val);
    	    if (val !== undefined && val.length > 3) {
    	        $scope.memberSearching = true;
    	        $http({
    	            url: "/umbraco/backoffice/UmbracoApi/Member/GetPagedResults",
    	            method: "GET",
    	            params: { pageNumber: 1, pageSize: 20, orderBy:"Name",orderDirection:"Ascending", filter:val }
    	        }).then(
                function (response) {
    	            console.log(response);
    	            $scope.memberResults = response.data.items;
    	            $scope.memberSearching = false;
                });
    	    }
    	})
    	$scope.chooseMember = function (member) {
    	    console.log(member);
    	    $scope.chosenMember = member;
    	}
    	$scope.cancelMemberChoice = function () {
    	    $scope.chosenMember = null;
    	}
    	$scope.submitNewBooking = function () {
    	    var bookingName = $scope.chosenMember.username;
    	    var memberId = $scope.chosenMember.id;
    	    var timeslotsArray = $scope.timeslots.split('#');
    	    var start = timeslotsArray[0].split(':');
    	    var end = timeslotsArray[1].split(':');
    	    var startTime = new Date(daySelected.setHours(start[0], start[1]));
    	    var endTime = new Date(daySelected.setHours(end[0], end[1]));

    	    uBookable.SaveBooking($scope.dialogData.nodeId, memberId, startTime, endTime, $scope.autoApproved).done(function (booking) {
    	        booking.Name = bookingName; // bit of a hack but saves hving to do it on the server.
    	        $scope.model.Bookings.push(booking);
    	        
    	        getTimeSlots($scope.dialogData.nodeId, daySelected);
    	        $scope.memberResults = "";
    	        $scope.chosenMember = null;
    	        $scope.timeslots = null;
    	        // notificationsService.success("New booking added", "The new booking has been successfully added");
    	        var alert = {
    	            Title: "New booking added",
    	            Text: "The new booking has been successfully added",
    	            Type: "success",
    	            Time: moment(),
    	            EndofLife: 5000,
    	            Persist: false
    	        };
    	        $scope.modalAlerts.push(alert);
    	        $scope.$apply();
    	    }).fail(function (xhr, textStatus) {
    	        console.log(xhr.status);
    	        if (xhr.status === 422) {

    	            var alert = {
    	                Title: "Booking failed",
    	                Text: "There are no more free spaces in this timeslot",
    	                Type: "danger",
    	                Time: moment(),
    	                EndofLife: 5000,
    	                Persist: false
    	            };
    	            $scope.modalAlerts.push(alert);
    	            $scope.$apply();
    	            //notificationsService.error("Booking failed", "There are no more free spaces in this timeslot");
    	        } else {
    	            var alert = {
    	                Title: "Booking failed",
    	                Text: xhr.status,
    	                Type: "danger",
    	                Time: moment(),
    	                EndofLife: 5000,
    	                Persist: false
    	            };
    	            $scope.modalAlerts.push(alert);
    	            $scope.$apply();
    	            //notificationsService.error("Booking failed, status: " + xhr.status, textStatus);
    	        }
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
                        var alert = {
                            Title: "Booking approved",
                            Text: "This event has been approved",
                            Type: "success",
                            Time: moment(),
                            EndofLife: 5000,
                            Persist: false
                        };
                        $scope.modalAlerts.push(alert);
            

                    //notificationsService.success("Booking approved", "This event has been approved");
    	        },
                function errorCallback(response) {
                    var alert = {
                        Title: "Booking approval failed",
                        Text: "Something went wrong, your event has not been approved.",
                        Type: "danger",
                        Time: moment(),
                        EndofLife: 5000,
                        Persist: false
                    };
                    $scope.modalAlerts.push(alert);
        
                    //notificationsService.error("Booking approval failed", "Something went wrong, your event has not been approved.");
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

                    var alert = {
                        Title: "Booking cancelled",
                        Text: "This booking has been canceled",
                        Type: "success",
                        Time: moment(),
                        EndofLife: 5000,
                        Persist: false
                    };
                    $scope.modalAlerts.push(alert);
                   

                    //notificationsService.success("Booking approved", "This booking has been canceled");
                },
                function errorCallback(response) {

                    var alert = {
                        Title: "Booking cancelation failed",
                        Text: "Something went wrong, your event has not been cancelled.",
                        Type: "danger",
                        Time: moment(),
                        EndofLife: 5000,
                        Persist: false
                    };
                    $scope.modalAlerts.push(alert);
                   
                   // notificationsService.error("Booking cancelation failed", "Something went wrong, your event has not been approved.");
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
                   
                        var alert = {
                            Title: "Booking deleted",
                            Text: "This booking has been deleted permenantly",
                            Type: "success",
                            Time: moment(),
                            EndofLife: 5000,
                            Persist: false
                        };
                        $scope.modalAlerts.push(alert);
            

                       // notificationsService.success("Booking deleted", "This booking has been deleted permenantly");
                    },
                    function errorCallback(response) {

                        var alert = {
                            Title: "Booking deletion failed",
                            Text: "Something went wrong, your event has not been deleted.",
                            Type: "danger",
                            Time: moment(),
                            EndofLife: 5000,
                            Persist: false
                        };
                        $scope.modalAlerts.push(alert);
                  
                        //notificationsService.error("Booking deletion failed", "Something went wrong, your event has not been approved.");
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