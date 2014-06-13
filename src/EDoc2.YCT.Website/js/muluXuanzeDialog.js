(function( $, undefined ) {

$.widget( "ui.muluXuanzeDialog", {
    options: {
		
	},
	_create: function() {
        var thiz = this;
        this.element.dialog({autoOpen: false, width: 350, height: 350, modal: true});
        this.muluTree = $(".muluTree");
        this.jiazaiDingjiMulu();
        this.element.find(".btnOk").click(function(){
            if(!thiz._selectedMuluId){
                alert("请选择目录");
                return false;
            }
            thiz._xuanzehou(thiz._selectedMuluId);
            thiz.element.dialog("close");
            return false;
        });
        this.element.find(".btnClose").click(function(){
            thiz.element.dialog("close");
            return false;
        });
	},
    jiazaiDingjiMulu: function(){
        var thiz = this;
        $.get("ZhishiHuoquController.aspx", {action: "GetDingjiMulu"}, function(data){
            if(data.result == 0){
                thiz.muluTree.tree({
		            data: [data.data],
		            dblclickOpen: true,
		            treenodeLoading: function(event, treenode){
                        thiz.jiazaiZiMulu(treenode);
		            },
                    treenodeSelected: function(event, treenode){
                        var muluId = treenode.treenode("option", "id");
                        thiz._selectedMuluId = muluId;
                    }
	            });

                var topnodes = thiz.muluTree.tree("getTopNodes");
                thiz.topnode = topnodes.eq(0).treenode("expand").treenode("select");
            }
            else{
                alert(data.message);
            }
        });
    },
    jiazaiZiMulu: function(treenode){
        var muluId = treenode.treenode("option", "id");
        $.get("ZhishiHuoquController.aspx", {action: "GetZiMulu", muluId: muluId}, function(data){
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
    xuanze: function(xuanzehou){
        this.topnode.treenode("reload").treenode("select");
        this._xuanzehou = xuanzehou;
        this.element.dialog("open");
    }
});

}( jQuery ) );

