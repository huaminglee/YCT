(function ($, undefined) {

    $.widget("ui.xiugaiMuluQuanxianDialog", {
        options: {
            autoOpen: false
        },
        _create: function () {
            var self = this;
            this._form = self.element.find("form").eq(0);
            this._form.validate({
                sendForm: false,
                onBlur: true,
                onChange: true,
                eachValidField: function () {
                    $(this).closest('.control-group').removeClass('error');
                },
                eachInvalidField: function () {
                    $(this).closest('.control-group').addClass('error');
                },
                valid: function () {
                    var formValue = {};
                    formValue.action = "XiugaiDaanGuanliMuluQuanxian";
                    formValue.muluId = self._muluId;
                    formValue.quanxianId = self._quanxianId;
                    formValue.quanxianZhi = 0;
                    self._form.find(":checked").each(function (i, radio) {
                        formValue.quanxianZhi += parseInt($(this).val());
                    });
                    $.post("ZhishiGianliController.aspx", formValue, function (data) {
                        if (data.result == 0) {
                            self.element.dialog("close");
                            self._trigger("xiugaiHou", null, {});
                        }
                        else {
                            alert(data.message);
                        }
                    });
                }
            });
            this.element.dialog({ autoOpen: false, width: 650, modal: true });
            this.element.find(".btnClose").click(function () {
                self.element.dialog("close");
                return false;
            });
        },
        xiugai: function (muluId, quanxian) {
            this._muluId = muluId;
            this._quanxianId = quanxian.id;
            this._form.find(":checkbox").each(function () {
                var val = parseInt($(this).val());
                if ((val & quanxian.zhi) > 0) {
                    $(this).prop("checked", true); ;
                }
                else {
                    $(this).removeAttr("checked");
                }
            });
            this._form.setFormValue(quanxian);
            this.element.dialog("open");
        }
    });

} (jQuery));

