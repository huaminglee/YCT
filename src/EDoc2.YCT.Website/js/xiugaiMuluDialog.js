(function( $, undefined ) {

$.widget( "ui.xiugaiMuluDialog", {
    options: {
		autoOpen: false
	},
	_create: function() {
		var self = this;
        this._form = self.element.find("form").eq(0);
        this._form.validate({
            sendForm : false,
            onBlur: true,
            onChange: true,
	        eachValidField : function() {
		        $(this).closest('.control-group').removeClass('error');
	        },
	        eachInvalidField : function() {
		        $(this).closest('.control-group').addClass('error');
	        },
            valid: function(){
                var formValue = self._form.getFormValue();
                $.post("ZhishiGianliController.aspx", {action: "XiugaiMulu", muluId: self._muluId, mingcheng: formValue.mingcheng}, function(data){
                    if(data.result == 0){
                        self.element.dialog("close");
                        self._form.setFormValue({mingcheng: ""});
		                self._trigger("xiugaiHou", null, {mingcheng: formValue.mingcheng});
                    }
                    else{
                        alert(data.message);
                    }
                });
            }
        });
        this.element.dialog({autoOpen: false, width: 500, modal: true});
        this.element.find(".btnClose").click(function(){
            self.element.dialog("close");
            return false;
        });
	},
    xiugai: function(muluId, mingcheng){
        this._muluId = muluId;
        this._form.setFormValue({mingcheng: mingcheng});
        this.element.dialog("open");
    }
});

}( jQuery ) );

