(function( $, undefined ) {

$.widget( "ui.zhishiChaxunDialog", {
    options: {
		
	},
	_create: function() {
		var self = this;
        this._form = self.element.find("form").eq(0);
        
        this.element.dialog({autoOpen: false, width: 500, modal: true});
        this.element.find(".btn-primary").click(function(){
            self.element.dialog("close");
		    self._trigger("chaxunhou", null, self._form.getFormValue());
            return false;
        });
	},
    chaxun: function(muluId){
        this._muluId = muluId;
        this.element.dialog("open");
    },
    qingchu: function(){
        this._form.setFormValue({});
    },
    getChaxunXinxi: function(){
        var xinxi = this._form.getFormValue();
        return xinxi;
    }
});

}( jQuery ) );

