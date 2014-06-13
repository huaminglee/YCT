(function ($, undefined) {
    $.widget("ui.houtaiDaanMuluQuanxianGuanli", {
        _create: function () {
            var self = this;

            this._grid = self.element.find(".quanxianGrid");
            this._grid.datagrid({
                columns: [
                    { title: "成员名称", width: 150, field: "chengyuanMingcheng" },
                    { title: "一级答案", width: 132, field: "youYijiDaanQuanxian" },
                    { title: "二级答案", width: 132, field: "youErjiDaanQuanxian" },
                    { title: "三级答案", width: 132, field: "youSanjiDaanQuanxian" },
                    { title: "四级答案", width: 132, field: "youSijiDaanQuanxian" },
                    { title: "五级答案", width: 132, field: "youWujiDaanQuanxian" },
                    { title: "继承权限", width: 99, field: "strShiJichengDe" }
                ],
                height: 300,
                canSort: false,
                singleSelect: true,
                showNumberRow: false,
                selectedRow: function (event, row) {
                    if (row.datarow("option", "data").shiJichengDe) {
                        self.element.find(".btnXiugai, .btnShanchu").attr("disabled", "disabled");
                    } else {
                        self.element.find(".btnXiugai, .btnShanchu").removeAttr("disabled");
                    }
                }
            });
//            this.element.dialog({ width: 730, height: 500, modal: true });

            this._tianjiaMuluQuanxianDialog = self.element.find(".tianjiaMuluQuanxianDialog").eq(0);
            this._tianjiaMuluQuanxianDialog.tianjiaMuluQuanxianDialog({
                tianjiaHou: function () {
                    self._shuaxinGrid();
                }
            });

            this._xiugaiMuluQuanxianDialog = self.element.find(".xiugaiMuluQuanxianDialog").eq(0);
            this._xiugaiMuluQuanxianDialog.xiugaiMuluQuanxianDialog({
                xiugaiHou: function () {
                    self._shuaxinGrid();
                }
            });
            this.element.find(".btnTianjia").click(function () {
                self._tianjiaMuluQuanxianDialog.tianjiaMuluQuanxianDialog("tianjia", self._muluId);
                return false;
            });
            this.element.find(".btnXiugai").click(function () {
                var selectedRows = self._grid.datagrid("getSelectedRow");
                var selectedQuanxian = selectedRows[0].datarow("option", "data");
                self._xiugaiMuluQuanxianDialog.xiugaiMuluQuanxianDialog("xiugai", self._muluId, selectedQuanxian);
                return false;
            });
            this.element.find(".btnShanchu").click(function () {
                if (confirm("确实要删除吗?")) {
                    var selectedRows = self._grid.datagrid("getSelectedRow");
                    var selectedQuanxianId = selectedRows[0].datarow("option", "data").id;
                    $.get("ZhishiGianliController.aspx", { action: "ShanchuDaanGuanliMuluQuanxian", muluId: self._muluId, quanxianId: selectedQuanxianId }, function (data) {
                        if (data.result == 0) {
                            alert(data.message);
                            self._shuaxinGrid();
                        } else {
                            alert(data.message);
                        }
                    });
                }
                return false;
            });
            this.element.find(".btnClose").click(function () {
                self.element.dialog("close");
                return false;
            });
        },
        _shuaxinGrid: function () {
            var self = this;
            $.get("ZhishiGianliController.aspx", { action: "GetDaanGuanliMuluQuanxian", muluId: this._muluId }, function (data) {
                if (data.result == 0) {
                    self.element.find(".btnXiugai, .btnShanchu").attr("disabled", "disabled");
                    self._grid.datagrid("option", "data", data.data);
                } else {
                    alert(data.message);
                }
            });
        },
        fenpei: function (muluId) {
            this._muluId = muluId;
            this._shuaxinGrid();
        }
    });

} (jQuery));