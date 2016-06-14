var eventslistapp =  angular.module('umbraco')
    .controller('EventListViewEditorDialog.Controller',
    function ($scope, $filter) {
        console.log($scope.dialogData.BookingData);
        var daySelected = new Date($scope.dialogData.year, $scope.dialogData.month, $scope.dialogData.day);
        $scope.DayKey = moment(daySelected).format('YYYYMMDD');
        console.log($scope.DayKey);
    	$scope.model = {
    		DateTitle: moment(daySelected).format('MMMM Do YYYY'),
    		Events: $scope.dialogData.BookingData.data
    	}
    	$scope.getByDayKey = function (date) {
    	    return function (day) {
    	        console.log(day);
    	        return day.Date === date;
    	    }
    	}

    	//$scope.fixDate = function (date) {
    	//    return function () {
    	//        return moment(new Date(parseInt(item.replace('/Date(', '').replace(')/', ''), 10))).format('H:mm');
    	//    }
    	//}
    });

eventslistapp.filter('fixDate', function () {
    return function (input) {
        return moment(new Date(parseInt(input.replace('/Date(', '').replace(')/', ''), 10))).format('H:mm');
    }
});