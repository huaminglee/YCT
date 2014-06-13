(function( $, undefined ) {

$.widget( "ui.muluGuanli", {
    _topNode: null,
    _tree: null,
    _chuangjianMuluDialog: null,
    options: {
		zhishiGuanli: null,
        muluXuanzeDialog: null
	},
	_create: function() {
		var self = this;

        self._chuangjianMuluDialog = self.element.find(".chuangjianMuluDialog");
        self._chuangjianMuluDialog.chuangjianMuluDialog({chuangjianHou: function(event, mulu){
            var selectedNode = self._tree.tree("getSelectedNode");
            selectedNode.treenode("empty");
            self.jiazaiZiMulu(selectedNode);
        }});

        self._xiugaiMuluDialog = self.element.find(".xiugaiMuluDialog");
        self._xiugaiMuluDialog.xiugaiMuluDialog({xiugaiHou: function(event, mulu){
            var selectedNode = self._tree.tree("getSelectedNode");
            selectedNode.treenode("option", "text", mulu.mingcheng);
        }});

        self._muluQuanxianDialog = self.element.find(".muluQuanxianDialog");
        self._muluQuanxianDialog.muluQuanxianDialog();

		self.element.find(".btnChuangjianMulu").eq(0).click(function(){
            var selectedNode = self._tree.tree("getSelectedNode");
            var muluId = selectedNode.treenode("option", "id");
            self._chuangjianMuluDialog.chuangjianMuluDialog("chuangjian", muluId);
            return false;
        });

        self.element.find(".btnShanchuMulu").eq(0).click(function(){
            if(confirm("确实要删除吗?"))
            {
                var selectedNode = self._tree.tree("getSelectedNode");
                self.shanchu(selectedNode);
            }
            return false;
        });

        self.element.find(".btnXiugaiMulu").eq(0).click(function(){
            var selectedNode = self._tree.tree("getSelectedNode");
            var muluId = selectedNode.treenode("option", "id");
            var mingcheng = selectedNode.treenode("option", "text");
            self._xiugaiMuluDialog.xiugaiMuluDialog("xiugai", muluId, mingcheng);
            return false;
        });

		self.element.find(".btnMuluQuanxian").eq(0).click(function(){
            var selectedNode = self._tree.tree("getSelectedNode");
            var muluId = selectedNode.treenode("option", "id");
            var mingcheng = selectedNode.treenode("option", "text");
            self._muluQuanxianDialog.muluQuanxianDialog("fenpei", muluId, mingcheng);
        });

        self.element.find(".btnShuaxin").eq(0).click(function(){
            var selectedNode = self._tree.tree("getSelectedNode");
            selectedNode.treenode("reload");
        });

        self.element.find(".btnYidong").eq(0).click(function(){
            self.options.muluXuanzeDialog.muluXuanzeDialog("xuanze", function(muluId){
                var selectedNode = self._tree.tree("getSelectedNode");
                var beiyidongdeMuluId = selectedNode.treenode("option", "id");
                $.get("ZhishiGianliController.aspx", {action: "YidongMulu", beiyidongdeMuluId: beiyidongdeMuluId, yidongdaodeMuluId: muluId}, function(data){
                    if(data.result == 0){
                        self._topNode.treenode("select");
                        selectedNode.treenode("remove");
                        alert(data.message);
                    }
                    else{
                        alert(data.message);
                    }
                });
            });
        });

		self._tree = self.element.find(".tree");
        $.get("ZhishiGianliController.aspx", {action: "GetDingjiMulu"}, function(data){
            if(data.result == 0){
                self._tree.tree({
		            data: [data.data],
		            dblclickOpen: true,
		            treenodeLoading: function(event, treenode){
                        self.jiazaiZiMulu(treenode);
		            },
                    treenodeSelected: function(event, treenode){
                        var muluId = treenode.treenode("option", "id");
                        self.options.zhishiGuanli.zhishiGuanli("jiazaiZhishi", muluId);
                    }
	            });

                var topnodes = self._tree.tree("getTopNodes");
                self._topNode = topnodes.eq(0);
                self._topNode.treenode("expand").treenode("select");
            }
            else{
                alert(data.message);
            }
        });
	},
	jiazaiZiMulu: function(treenode){
        var muluId = treenode.treenode("option", "id");
        $.get("ZhishiGianliController.aspx", {action: "GetZiMulu", muluId: muluId}, function(data){
            if(data.result == 0){
                if(data.data && data.data.length){
                    $.each(data.data, function(i, nodeData){
                        treenode.treenode("append", nodeData).treenode("expand");
                    });
                }
            }
            else{
                alert(data.message);
            }
        });
    },
    shanchu: function(treenode){
        var self = this;
        var muluId = treenode.treenode("option", "id");
        $.get("ZhishiGianliController.aspx", {action: "ShanchuMulu", muluId: muluId}, function(data){
            if(data.result == 0){
                treenode.treenode("remove");
                self._topNode.treenode("select");
                alert(data.message);
            }
            else{
                alert(data.message);
            }
        });
    },
    getXuanzeMuluId: function(){
        var selectedNode = this._tree.tree("getSelectedNode");
        var muluId = selectedNode.treenode("option", "id");
        return muluId;
    }
});

}( jQuery ) );

