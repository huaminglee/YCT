(function( $, undefined ) {

$.widget( "ui.zhishiGuanli", {
    _grid: null,
    options: {
        edoc2BaseUrl: null,
        zhishiDatagridHeight: null,
        muluXuanzeDialog: null
	},
	_create: function() {
		var self = this;
        self._chuangjianZhishiDialog = self.element.find(".chuangjianZhishiDialog");
        self._chuangjianZhishiDialog.chuangjianZhishiDialog({
            edoc2BaseUrl: this.options.edoc2BaseUrl,
            chuangjianHou: function(event, mulu){
                self._shuaxin(true);
        }});
        self._xiugaiZhishiDialog = self.element.find(".xiugaiZhishiDialog");
        self._xiugaiZhishiDialog.xiugaiZhishiDialog({
            edoc2BaseUrl: this.options.edoc2BaseUrl,
            xiugaihou: function(event, mulu){
                self._shuaxin();
        }});

		self.element.find(".btnChuangjian").click(function(){
            self._chuangjianZhishiDialog.chuangjianZhishiDialog("chuangjian", self._muluId);
            return false;
        });
        this._lishiZhishiDialog = self.element.find(".lishiZhishiDialog");
        this._lishiZhishiDialog.lishiZhishiDialog();
        this._grid = self.element.find(".datagrid");
        this._grid.datagrid({
		    columns:[
			    {title: "问题", width: 450, field:"wentiBiaoti", render: function(row, args){
				    return $("<a target='_blank' href='#'>"+args.value+"</a>").click(function(){
                        self._xiugaiZhishiDialog.xiugaiZhishiDialog("xiugai", args.data.id);
                        return false;
                    });
			    }},
			    {title: "创建人", width: 70, field:"chuangjianren"},
			    {title: "创建时间", width: 70, field:"chuangjianShijian"},
			    {title: "修改人", width: 70, field:"xiugairen"},
			    {title: "修改时间", width: 70, field:"xiugaiShijian"},
			    {title: "版本", width: 30, field:"banbenhao", render: function(row, args){
				    return $("<a target='_blank' href='#'>"+args.value+"</a>").click(function(){
                        self._lishiZhishiDialog.lishiZhishiDialog("chakan", args.data.id);
                        return false;
                    });
			    }}
//			    ,{title: "顺序", width: 30, field:"shunxu"}
		    ],
		    canSort: false,
		    singleSelect: true,
		    showNumberRow: false,
            height: this.options.zhishiDatagridHeight,
            selectedRow: function(){
                self.element.find(".btnShanchu, .btnYidong, .btnZhiding, .btnQuxiaoZhiding").removeAttr("disabled");
            }
	    });
        this._datagridPager = self.element.find(".datagridPager");
        this._datagridPager.pager({change: function(event, args){
            self._shuaxin();
        }});
		self.element.find(".btnShanchu").click(function(){
            if(confirm("确实要删除吗?")){
                var selectedRows = self._grid.datagrid("getSelectedRow");
                var zhishi = selectedRows[0].datarow("option", "data");
                $.get("ZhishiGianliController.aspx", {action: "ShanchuZhishi", zhishiId: zhishi.id}, function(data){
                    if(data.result == 0){
                        self._shuaxin();
                        alert(data.message);
                    }
                    else{
                        alert(data.message);
                    }
                });
            }
            return false;
        });
		self.element.find(".btnYidong").click(function(){
            self.options.muluXuanzeDialog.muluXuanzeDialog("xuanze", function(muluId){
                var selectedRows = self._grid.datagrid("getSelectedRow");
                var zhishi = selectedRows[0].datarow("option", "data");
                $.get("ZhishiGianliController.aspx", {action: "YidongZhishi", muluId: muluId, zhishiId: zhishi.id}, function(data){
                    if(data.result == 0){
                        self._shuaxin();
                        alert(data.message);
                    }
                    else{
                        alert(data.message);
                    }
                });
            });
            return false;
        });
        self.element.find(".btnZhiding").click(function(){
            var selectedRows = self._grid.datagrid("getSelectedRow");
            var zhishi = selectedRows[0].datarow("option", "data");
            $.get("ZhishiGianliController.aspx", {action: "Zhiding", zhishiId: zhishi.id}, function(data){
                if(data.result == 0){
                    self._shuaxin();
                    alert(data.message);
                }
                else{
                    alert(data.message);
                }
            });
            return false;
        });
        self.element.find(".btnQuxiaoZhiding").click(function(){
            var selectedRows = self._grid.datagrid("getSelectedRow");
            var zhishi = selectedRows[0].datarow("option", "data");
            $.get("ZhishiGianliController.aspx", {action: "QuxiaoZhiding", zhishiId: zhishi.id}, function(data){
                if(data.result == 0){
                    self._shuaxin();
                    alert(data.message);
                }
                else{
                    alert(data.message);
                }
            });
            return false;
        });
        this._chaxunPanel = self.element.find(".chaxunPanel").zhishiChaxunDialog({chaxunhou: function(event, args){
            self._shuaxin();
        }});
        self.element.find(".btnChaxun").click(function(){
            self._chaxunPanel.zhishiChaxunDialog("chaxun");    
        });
	},
	jiazaiZhishi: function(muluId){
        this._muluId = muluId;
        this._shuaxin(true);
    },
    _shuaxin: function(qingchuChaxun){
        if(qingchuChaxun){
            this._chaxunPanel.zhishiChaxunDialog("qingchu");
        }
        var chaxunXinxi = this._chaxunPanel.zhishiChaxunDialog("getChaxunXinxi");
		var self = this;
        var pageInfo = self._datagridPager.pager("getPageInfo");
        var args = {};
        args.action = "GetMuluZhishi";
        args.muluId = self._muluId;
        args.kaishiHang = pageInfo.start;
        args.jieshuHang = pageInfo.end;
        $.extend(args, chaxunXinxi);
        this.element.find(".btnShanchu, .btnYidong, .btnZhiding, .btnQuxiaoZhiding").attr("disabled", "disabled");
        $.get("ZhishiGianliController.aspx", args, function(data){
            if(data.result == 0){
                self._grid.datagrid("option", "data", data.data.grid);
                self._datagridPager.pager("setPageInfo", {start: data.data.kaishiHang, end: data.data.jieshuHang, count: data.data.zongHangshu, size: data.data.yedaxiao});
            }
            else{
                alert(data.message);
            }
        });
    }
});

}( jQuery ) );

