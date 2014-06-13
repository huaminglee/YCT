(function($, undefined) {
    $.widget("ui.xiugaiZhishiDialog", {
        options: {
            autoOpen: false,
            edoc2BaseUrl: null
        },
        _create: function() {
            var self = this;
            this._form = self.element.find("form").eq(0);
            this._form.validate({
                sendForm: false,
                onBlur: true,
                onChange: true,
                eachValidField: function() {
                    $(this).closest('.control-group').removeClass('error');
                    $(this).next('.help-inline').hide();
                },
                eachInvalidField: function() {
                    $(this).closest('.control-group').addClass('error');
                    $(this).next('.help-inline').show();
                },
                valid: function() {
                    var formValue = self._form.getFormValue();
                    formValue.zhishiId = self._zhishiId;
                    formValue.yijiDaanFujian = self._yijiDananFujianGrid.fujianGrid("getFujianData");
                    formValue.erjiDaanFujian = self._erjiDananFujianGrid.fujianGrid("getFujianData");
                    formValue.sanjiDaanFujian = self._sanjiDananFujianGrid.fujianGrid("getFujianData");
                    formValue.sijiDaanFujian = self._sijiDananFujianGrid.fujianGrid("getFujianData");
                    formValue.wujiDaanFujian = self._wujiDananFujianGrid.fujianGrid("getFujianData");
                    if (!formValue.shunxu) {
                        formValue.shunxu = null;
                    }
                    var postJson = $.toJSON(formValue);

                    $.post("ZhishiGianliController.aspx", { action: "XiugaiZhishi", postJson: postJson }, function(data) {
                        if (data.result == 0) {
                            self.element.dialog("close");
                            self._trigger("xiugaihou", null, {});
                        } else {
                            alert(data.message);
                        }
                    });
                }
            });

            self._yijiDananFujianGrid = this._form.find(".yijiDananFujianGrid").fujianGrid({ edoc2BaseUrl: this.options.edoc2BaseUrl });
            self._erjiDananFujianGrid = this._form.find(".erjiDananFujianGrid").fujianGrid({ edoc2BaseUrl: this.options.edoc2BaseUrl });
            self._sanjiDananFujianGrid = this._form.find(".sanjiDananFujianGrid").fujianGrid({ edoc2BaseUrl: this.options.edoc2BaseUrl });
            self._sijiDananFujianGrid = this._form.find(".sijiDananFujianGrid").fujianGrid({ edoc2BaseUrl: this.options.edoc2BaseUrl });
            self._wujiDananFujianGrid = this._form.find(".wujiDananFujianGrid").fujianGrid({ edoc2BaseUrl: this.options.edoc2BaseUrl });

            this.element.dialog({ autoOpen: false, width: 750, height: 450, modal: true, close: function() { self.jieshuXiugai(); } });
            this.element.find(".btnClose").click(function() {
                self.element.dialog("close");
                return false;
            });
        },
        jieshuXiugai: function() {
            if (!this._form.find(":submit").prop("disabled")) {
                $.post("ZhishiGianliController.aspx", { action: "JiesuoZhishi", zhishiId: this._zhishiId }, function(data) {
                    if (data.result != 0) {
                        alert(data.message);
                    }
                });
            }
        },
        kaishiXiugai: function() {
            $.post("ZhishiGianliController.aspx", { action: "SuodingZhishi", zhishiId: this._zhishiId }, function(data) {
                if (data.result != 0) {
                    alert(data.message);
                }
            });
        },
        xiugai: function(zhishiId) {
            var self = this;
            this._zhishiId = zhishiId;
            $.get("ZhishiGianliController.aspx", { action: "GetZhishi", zhishiId: zhishiId }, function(data) {
                if (data.result == 0) {
                    if (data.data.youYijiDaanGuanliQuanxian == true || data.data.youYijiDaanGuanliQuanxian == 'true') {
                        self._yijiDananFujianGrid.fujianGrid("setFujianData", data.data.yijiDaanFujian);
                        self.element.find(".yijiDaanContainer").show();
                    } else {
                        self.element.find(".yijiDaanContainer").hide();
                    }
                    if (data.data.youErjiDaanGuanliQuanxian == true || data.data.youErjiDaanGuanliQuanxian == 'true') {
                        self._erjiDananFujianGrid.fujianGrid("setFujianData", data.data.erjiDaanFujian);
                        self.element.find(".erjiDaanContainer").show();
                    } else {
                        self.element.find(".erjiDaanContainer").hide();
                    }
                    if (data.data.youSanjiDaanGuanliQuanxian == true || data.data.youSanjiDaanGuanliQuanxian == 'true') {
                        self._sanjiDananFujianGrid.fujianGrid("setFujianData", data.data.sanjiDaanFujian);
                        self.element.find(".sanjiDaanContainer").show();
                    } else {
                        self.element.find(".sanjiDaanContainer").hide();
                    }
                    if (data.data.youSijiDaanGuanliQuanxian == true || data.data.youSijiDaanGuanliQuanxian == 'true') {
                        self._sijiDananFujianGrid.fujianGrid("setFujianData", data.data.sijiDaanFujian);
                        self.element.find(".sijiDaanContainer").show();
                    } else {
                        self.element.find(".sijiDaanContainer").hide();
                    }
                    if (data.data.youWujiDaanGuanliQuanxian == true || data.data.youWujiDaanGuanliQuanxian == 'true') {
                        self._wujiDananFujianGrid.fujianGrid("setFujianData", data.data.wujiDaanFujian);
                        self.element.find(".wujiDaanContainer").show();
                    } else {
                        self.element.find(".wujiDaanContainer").hide();
                    }
                    self._form.setFormValue(data.data);
                    if (!data.data.kebianji) {
                        self._form.setFormReadOnly(true);
                        self._form.find(":submit, button").prop("disabled", true);
                        self.element.find(".zhishiZhuangtai").text("正在被" + data.data.bianjiren + "修改").show();
                    } else {
                        self.element.find(".zhishiZhuangtai").hide();
                        self._form.find(":submit, button").prop("disabled", false);
                        self._form.setFormReadOnly(false);
                        self.kaishiXiugai();
                    }
                    self._form.find(".fujian").wenjian("bangding");
                } else {
                    alert(data.message);
                }
            });
            this.element.dialog("open");
        }
    });

}(jQuery));