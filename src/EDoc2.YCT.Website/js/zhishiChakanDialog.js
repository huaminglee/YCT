(function( $, undefined ) {

$.widget( "ui.zhishiChakanDialog", {
    options: {
		autoOpen: false
	},
	_create: function() {
		var self = this;

        this.element.dialog({autoOpen: false, width: 650, height: 450, modal: true});
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
    _bangding: function(zhishi){
        this.element.find(".zhishiWenti").text(zhishi.wenti);
        this.element.find(".daanPanel").html(zhishi.daanHtml);
    
    },
    chakan: function(zhishiId){
        var self = this;
        this.element.dialog("open");
        $.post("ZhishiHuoquController.aspx", {action: "GetZhishi", zhishiId: zhishiId}, function(data){
            if(data.result == 0){
                var zhishi = data.data;
                self._bangding(zhishi);
            }
            else{
                alert(data.message);
            }
        });
    },
    chakanLishiZhishi: function(zhishiId, lishiZhishiId){
        var self = this;
        this.element.dialog("open");
        $.post("ZhishiGianliController.aspx", {action: "GetLishiZhishiById", zhishiId: zhishiId, lishiZhishiId: lishiZhishiId}, function(data){
            if(data.result == 0){
                var zhishi = data.data;
                self._bangding(zhishi);
            }
            else{
                alert(data.message);
            }
        });
    }
});

}( jQuery ) );

