angular.module("umbraco")
       .controller("CodeGecko.Umbraco.Packages.MediaCopy.CopyController",
                    ["$scope", "$http", "umbRequestHelper", "dialogHelper", "mediaResource",
            function ($scope, $http, umbRequestHelper, dialogHelper, mediaResource) {

                var copyService = {
                    copy: function (args) {
                        var endpointUrl = umbRequestHelper.getApiUrl("MediaCopyApi", "Copy", args);

                        return $http.get(endpointUrl)
                                    .then(handleSuccess, handleFailure);
                    }
                };

                var dialogOptions = $scope.dialogOptions;
                $scope.dialogTreeEventHandler = $({});
                var node = dialogOptions.currentNode;

                function load() {
                    var copyMediaResource = angular.extend(mediaResource, copyService);
                }

                function nodeSelectHandler(ev, args) {
                    args.event.preventDefault();
                    args.event.stopPropagation();

                    eventsService.emit("editors.media.copyController.select", args);

                    if ($scope.target) {
                        //un-select if there's a current one selected
                        $scope.target.selected = false;
                    }

                    $scope.target = args.node;
                    $scope.target.selected = true;
                }

                var handleSuccess = function (data) {

                }

                var handleFailure = function (err) {
                    $scope.success = false;
                    $scope.error = err;
                }

                $scope.dialogTreeEventHandler.bind("treeNodeSelect", nodeSelectHandler);

                $scope.copy = function () {
                    mediaResource.copy({ newParent: $scope.target.id, id: node.id })
                        .then(function (path) {
                            $scope.error = false;
                            $scope.success = true;

                            //first we need to remove the node that launched the dialog

                            //get the currently edited node (if any)
                            var activeNode = appState.getTreeState("selectedNode");

                            //we need to do a double sync here: first sync to the moved content - but don't activate the node,
                            //then sync to the currenlty edited content (note: this might not be the content that was moved!!)

                            navigationService.syncTree({ tree: "media", path: path, forceReload: true, activate: false }).then(function (args) {
                                if (activeNode) {
                                    var activeNodePath = treeService.getPath(activeNode).join();
                                    //sync to this node now - depending on what was copied this might already be synced but might not be
                                    navigationService.syncTree({ tree: "media", path: activeNodePath, forceReload: false, activate: true });
                                }
                            });

                        }, handleFailure);
                };

                $scope.$on('$destroy', function () {
                    $scope.dialogTreeEventHandler.unbind("treeNodeSelect", nodeSelectHandler);
                });
            
                load();
}]);