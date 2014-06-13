(function( $, undefined ) {

$.widget( "ui.lishiZhishiDialog", {
    options: {
		autoOpen: false
	},
	_create: function() {
		var self = this;
        this._zhishiChakanDialog = self.element.find(".zhishiChakanDialog").zhishiChakanDialog();
        this._grid = self.element.find(".lishiZhishiGrid");
        this._grid.datagrid({
		    columns:[
			    {title: "问题", width: 300, field:"wenti", render: function(row, args){
				    return $("<a target='_blank' href='#'>"+args.value+"</a>").click(function(){
                        self._zhishiChakanDialog.zhishiChakanDialog("chakanLishiZhishi", args.data.zhishiId, args.data.id);
                        return false;
                    });
			    }},
			    {title: "创建人", width: 80, field:"chuangjianren"},
			    {title: "创建时间", width: 80, field:"chuangjianShijian"},
			    {title: "版本", width: 80, field:"banbenhao"}
		    ],
		    canSort: false,
		    singleSelect: true,
		    showNumberRow: false
	    });
        this.element.dialog({autoOpen: false, width: 650, height: 400, modal: true});
	},
    _shuaxinGrid: function(){
        var self = this;
        $.get("ZhishiGianliController.aspx", {action: "GetLishiZhishi", zhishiId: this._zhishiId}, function(data){
            if(data.result == 0){
                self._grid.datagrid("option", "data", data.data);
            }
            else{
                alert(data.message);
            }
        });
    },
    chakan: function(zhishiId){
        var self = this;
        this._zhishiId = zhishiId;
        this._shuaxinGrid();
        this.element.dialog("open");
    }
});

}( jQuery ) );

