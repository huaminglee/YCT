(function( $, undefined ) {

$.widget( "ui.chuangjianMuluDialog", {
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
                $.post("ZhishiGianliController.aspx", {action: "ChuangjianMulu", fuMuluId: self._fuMuluId, mingcheng: formValue.mingcheng}, function(data){
                    if(data.result == 0){
                        self.element.dialog("close");
                        self._form.setFormValue({mingcheng: ""});
		                self._trigger("chuangjianHou", null, {mingcheng: formValue.mingcheng});
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
    chuangjian: function(fuMuluId){
        this._fuMuluId = fuMuluId;
        this.element.dialog("open");
    }
});

}( jQuery ) );

