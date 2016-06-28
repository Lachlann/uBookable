var duplicateNodeCntrl = angular.module("umbraco");


duplicateNodeCntrl.controller("DuplicateNode", function ($scope, $filter, $http, $routeParams, editorState, contentResource, notificationsService, navigationService, appState, $timeout) {
    var selectedNode = appState.getMenuState("currentNode");
    $scope.nodeId = selectedNode.id;
    $scope.timesToDuplicate = 1;
    $scope.Name = selectedNode.name;


    $scope.duplicate = function () {

        if ($scope.timesToDuplicate <= 20) {
            recursiveCopy($scope.timesToDuplicate, selectedNode.parentId, selectedNode.id);
        }

    }

    function recursiveCopy(remainingCopies, parent, copynode) {
        var remain = remainingCopies - 1;
        contentResource.copy({ parentId: parent, id: copynode })
           .then(function (response) {
               var pathArray = response.split(',');

               if ($scope.loremNames) {
                   contentResource.getById(pathArray[pathArray.length - 1])
                      .then(function (content) {
                          content.name = loremIpsum[remainingCopies];
                          contentResource.save(content, false, [])
                            .then(function (content) {
                                if ($scope.autoPublish) {
                                    contentResource.publishById(pathArray[pathArray.length - 1]);
                                }
                            });
                      });
               }else if($scope.autoPublish){
                   contentResource.publishById(pathArray[pathArray.length - 1]);
               }
               if (remain === 0) {
                   $timeout(function () { navigationService.reloadNode($scope.currentNode.parent()); }, 1000);
                   
                   notificationsService.success("Nodes duplicated", "Your new nodes are ready, you may have to refresh your browser.");
               } else {
                   recursiveCopy(remain, parent, copynode)
               }
           }, function (err) {
               notificationsService.error("Error duplicating nodes", "There has been a problem duplicating your nodes.");
           });
    }

});