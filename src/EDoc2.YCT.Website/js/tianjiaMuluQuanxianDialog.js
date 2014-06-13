(function( $, undefined ) {

$.widget( "ui.tianjiaMuluQuanxianDialog", {
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
                var formValue = {};
                formValue.action = "TianjiaMuluQuanxian";
                formValue.muluId = self._muluId;
                var chengyuan = self._chengyuanSelect2.select2("data");
                if(!chengyuan){
                    alert("请选择成员!");
                    return ;
                }
                formValue.chengyuanId = chengyuan.id;
                formValue.chengyuanMingcheng = chengyuan.text;
                formValue.quanxianZhi = 0;
                self._form.find(":checked").each(function(){
                     formValue.quanxianZhi += parseInt($(this).val());
                });
                $.post("ZhishiGianliController.aspx", formValue, function(data){
                    if(data.result == 0){
                        self.element.dialog("close");
                        self._chengyuanSelect2.select2("val", "");
		                self._trigger("tianjiaHou", null, {});
                    }
                    else{
                        alert(data.message);
                    }
                });
            }
        });
        this._chengyuanSelect2 = this.element.find(".chengyuanSelect2").select2({width: "200px", data: []});
        $.get("ZhishiGianliController.aspx", {action: "GetMemberSelect2"}, function(data){
            if(data.result == 0){
                self._chengyuanSelect2.select2({width: "200px", data: data.data});
            }
            else{
                alert(data.message);
            }
        });
        this.element.dialog({autoOpen: false, width: 650, modal: true});
        this.element.find(".btnClose").click(function(){
            self.element.dialog("close");
            return false;
        });
	},
    tianjia: function(muluId){
        this._muluId = muluId;
        this.element.dialog("open");
    }
});

}( jQuery ) );

