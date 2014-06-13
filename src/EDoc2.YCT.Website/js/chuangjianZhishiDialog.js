(function( $, undefined ) {

$.widget( "ui.chuangjianZhishiDialog", {
    options: {
		autoOpen: false,
        edoc2BaseUrl: null
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
                $(this).next('.help-inline').hide();
	        },
	        eachInvalidField : function() {
		        $(this).closest('.control-group').addClass('error');
                $(this).next('.help-inline').show();
	        },
            valid: function(){
                var formValue = self._form.getFormValue();
                formValue.muluId = self._muluId;
                formValue.yijiDaanFujian = self._yijiDananFujianGrid.fujianGrid("getFujianData");
                formValue.erjiDaanFujian = self._erjiDananFujianGrid.fujianGrid("getFujianData");
                formValue.sanjiDaanFujian = self._sanjiDananFujianGrid.fujianGrid("getFujianData");
                formValue.sijiDaanFujian = self._sijiDananFujianGrid.fujianGrid("getFujianData");
                formValue.wujiDaanFujian = self._wujiDananFujianGrid.fujianGrid("getFujianData");
                if(!formValue.shunxu){
                    formValue.shunxu = null;
                }
                var postJson = $.toJSON(formValue);

                $.post("ZhishiGianliController.aspx", {action: "ChuangjianZhishi", postJson: postJson}, function(data){
                    if(data.result == 0){
                        self.element.dialog("close");
		                self._trigger("chuangjianHou", null, {});
                    }
                    else{
                        alert(data.message);
                    }
                });
            }
        });
        
        self._yijiDananFujianGrid = this._form.find(".yijiDananFujianGrid").fujianGrid({edoc2BaseUrl: this.options.edoc2BaseUrl});
        self._erjiDananFujianGrid = this._form.find(".erjiDananFujianGrid").fujianGrid({edoc2BaseUrl: this.options.edoc2BaseUrl});
        self._sanjiDananFujianGrid = this._form.find(".sanjiDananFujianGrid").fujianGrid({edoc2BaseUrl: this.options.edoc2BaseUrl});
        self._sijiDananFujianGrid = this._form.find(".sijiDananFujianGrid").fujianGrid({edoc2BaseUrl: this.options.edoc2BaseUrl});
        self._wujiDananFujianGrid = this._form.find(".wujiDananFujianGrid").fujianGrid({edoc2BaseUrl: this.options.edoc2BaseUrl});

        this.element.dialog({autoOpen: false, width: 750, height: 450, modal: true});
        this.element.find(".btnClose").click(function(){
            self.element.dialog("close");
            return false;
        });
	},
    chuangjian: function (muluId) {
        var self = this;
        this._muluId = muluId;
        this._form[0].reset();
        $.get("ZhishiGianliController.aspx", { action: "GetDaanGuanliQuanxian", muluId: muluId }, function (data) {
            if (data.result == 0) {
                if (data.data.youYijiDaanGuanliQuanxian == true || data.data.youYijiDaanGuanliQuanxian == 'true') {
                    self.element.find(".yijiDaanContainer").show();
                } else {
                    self.element.find(".yijiDaanContainer").hide();
                }
                if (data.data.youErjiDaanGuanliQuanxian == true || data.data.youErjiDaanGuanliQuanxian == 'true') {
                    self.element.find(".erjiDaanContainer").show();
                } else {
                    self.element.find(".erjiDaanContainer").hide();
                }
                if (data.data.youSanjiDaanGuanliQuanxian == true || data.data.youSanjiDaanGuanliQuanxian == 'true') {
                    self.element.find(".sanjiDaanContainer").show();
                } else {
                    self.element.find(".sanjiDaanContainer").hide();
                }
                if (data.data.youSijiDaanGuanliQuanxian == true || data.data.youSijiDaanGuanliQuanxian == 'true') {
                    self.element.find(".sijiDaanContainer").show();
                } else {
                    self.element.find(".sijiDaanContainer").hide();
                }
                if (data.data.youWujiDaanGuanliQuanxian == true || data.data.youWujiDaanGuanliQuanxian == 'true') {
                    self.element.find(".wujiDaanContainer").show();
                } else {
                    self.element.find(".wujiDaanContainer").hide();
                }
            } else {
                alert(data.message);
            }
        });
        this._yijiDananFujianGrid.fujianGrid("setFujianData", []);
        this._erjiDananFujianGrid.fujianGrid("setFujianData", []);
        this._sanjiDananFujianGrid.fujianGrid("setFujianData", []);
        this._sijiDananFujianGrid.fujianGrid("setFujianData", []);
        this._wujiDananFujianGrid.fujianGrid("setFujianData", []);
        this.element.dialog("open");
    }
});

}( jQuery ) );

