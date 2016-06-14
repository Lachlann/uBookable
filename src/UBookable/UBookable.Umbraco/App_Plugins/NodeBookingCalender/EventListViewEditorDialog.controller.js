angular.module('umbraco')
    .controller('EventListViewEditorDialog.Controller',
    function ($scope) {
    	console.log($scope.dialogData.year);
    	$scope.model = {
    		year : $scope.dialogData.year
    	}    	    	
    });