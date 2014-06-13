(function($, undefined) {
    $.widget("ui.houtaiDananQuanxianMuluGuanli", {
        _topNode: null,
        _tree: null,
        options: {
            muluQuanxianDialog: null,
            muluXuanzeDialog: null
        },
        _create: function() {
            var self = this;

            self._muluQuanxianDialog = muluQuanxianDialog;
            self._muluQuanxianDialog.houtaiDaanMuluQuanxianGuanli();

            self.element.find(".btnShuaxin").eq(0).click(function() {
                var selectedNode = self._tree.tree("getSelectedNode");
                selectedNode.treenode("reload");
            });

            self.element.find(".btnYidong").eq(0).click(function() {
                self.options.muluXuanzeDialog.muluXuanzeDialog("xuanze", function(muluId) {
                    var selectedNode = self._tree.tree("getSelectedNode");
                    var beiyidongdeMuluId = selectedNode.treenode("option", "id");
                    $.get("ZhishiGianliController.aspx", { action: "YidongMulu", beiyidongdeMuluId: beiyidongdeMuluId, yidongdaodeMuluId: muluId }, function(data) {
                        if (data.result == 0) {
                            self._topNode.treenode("select");
                            selectedNode.treenode("remove");
                            alert(data.message);
                        } else {
                            alert(data.message);
                        }
                    });
                });
            });

            self._tree = self.element.find(".tree");
            $.get("ZhishiGianliController.aspx", { action: "GetDingjiMulu" }, function(data) {
                if (data.result == 0) {
                    self._tree.tree({
                        data: [data.data],
                        dblclickOpen: true,
                        treenodeLoading: function(event, treenode) {
                            self.jiazaiZiMulu(treenode);
                        },
                        treenodeSelected: function (event, treenode) {
                            var muluId = treenode.treenode("option", "id");
//                            var mingcheng = treenode.treenode("option", "text");
                            self._muluQuanxianDialog.houtaiDaanMuluQuanxianGuanli("fenpei", muluId);
                        }
                    });

                    var topnodes = self._tree.tree("getTopNodes");
                    self._topNode = topnodes.eq(0);
                    self._topNode.treenode("expand").treenode("select");
                } else {
                    alert(data.message);
                }
            });
        },
        jiazaiZiMulu: function(treenode) {
            var muluId = treenode.treenode("option", "id");
            $.get("ZhishiGianliController.aspx", { action: "GetZiMulu", muluId: muluId }, function(data) {
                if (data.result == 0) {
                    if (data.data && data.data.length) {
                        $.each(data.data, function(i, nodeData) {
                            treenode.treenode("append", nodeData).treenode("expand");
                        });
                    }
                } else {
                    alert(data.message);
                }
            });
        },
        getXuanzeMuluId: function() {
            var selectedNode = this._tree.tree("getSelectedNode");
            var muluId = selectedNode.treenode("option", "id");
            return muluId;
        }
    });

}(jQuery));