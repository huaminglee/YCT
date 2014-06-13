function openUploadWnd(url, width, height) {
    return window.open(url, "_blank", "height=" + height + "px, width=" + width + "px, toolbar= no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
}

function showModalWnd(url, width, height) {
    return window.showModalDialog(url,null,"dialogWidth:"+width+"px;dialogHeight:"+height+"px;help:no;status:no");
}

jQuery(function($){
	$.datepicker.regional['zh-CN'] = {
	    closeText: '关闭',
		prevText: '&#x3c;上月',
		nextText: '下月&#x3e;',
		currentText: '今天',
		monthNames: ['一月','二月','三月','四月','五月','六月',
		'七月','八月','九月','十月','十一月','十二月'],
		monthNamesShort: ['一','二','三','四','五','六',
		'七','八','九','十','十一','十二'],
		dayNames: ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
		dayNamesShort: ['周日','周一','周二','周三','周四','周五','周六'],
		dayNamesMin: ['日','一','二','三','四','五','六'],
		dateFormat: 'yy-mm-dd', firstDay: 1,
		isRTL: false
		};
	$.datepicker.setDefaults($.datepicker.regional['zh-CN']);
});

(function($) {
    $.widget(
        "ui.wenjian", {
            options: {
                edoc2BaseUrl: "",
                wenjianId: null,
                wenjianMingcheng: null
            },
            _create: function(){
                var self = this;
                var element = this.element;
                if(this.options.wenjianId){
                    element.find(".wenjianLianjie").text(this.options.wenjianMingcheng).attr("href", this.options.edoc2BaseUrl+"/preview.aspx?key=yctZhishiku&fileId="+this.options.wenjianId).show();
                    element.find(".btnXiazai").attr("href", this.options.edoc2BaseUrl+"/Document/File_Download.aspx?key=yctZhishiku&file_Id="+this.options.wenjianId);
                    element.find(".btnXiazai, .btnShanchu").show();
                }
                else{
                    element.find(".wenjianLianjie, .btnXiazai, .btnShanchu").hide();
                }

                element.find(".btnShangchuan").click(function(){
                    $(this).attr("disabled", "disabled");
                    $.post("ZhishiGianliController.aspx?action=ChuangjianWenjianjia", null, function(data){
                        if(data.result == 0){
                            var url = self.options.edoc2BaseUrl + "/Document/File_UploadEx.aspx?key=yctZhishiku&folderid=" + data.data + "&multifile=false";
                            var win = openUploadWnd(url, 520, 360);
                            window.uploadCallback = function (filelist) {
                                if (filelist && filelist.length) {
                                    var wenjian = filelist[0];
                                    element.find(".wenjianId").val(wenjian.fileId);
                                    element.find(".wenjianMingcheng").val(wenjian.fileName);
                                    self.bangding();
                                }
                                win.close();
                            };
                        }
                        else{
                            alert(data.message);
                            win.close();
                        }
                    });
                    $(this).removeAttr("disabled");
                });
                
                element.find(".btnShanchu").click(function(){
                    $.post("ZhishiGianliController.aspx?action=DeleteFile", { wenjianId: self.options.wenjianId }, function (data) {
                        if (data.result == 0) {
                            element.find(".wenjianId").val("");
                            element.find(".wenjianMingcheng").val("");
                            self.bangding();
                        }
                        else{
                            alert(data.message);
                        }
                    });
                });
            },
            _setOption: function(key, value){
		        var self = this;
		        $.Widget.prototype._setOption.apply(self, arguments);
		        switch(key){
			        case "wenjianId": 
                        if(value){
                            self.element.find(".wenjianId").val(value);
                            self.element.find(".wenjianLianjie")
                                .attr("href", self.options.edoc2BaseUrl+"/preview.aspx?key=yctZhishiku&fileId="+value)
                                .show();
                            self.element.find(".btnXiazai").attr("href", self.options.edoc2BaseUrl+"/Document/File_Download.aspx?key=yctZhishiku&file_Id="+value).show();
                        }
                        else{
                            self.element.find(".wenjianLianjie, .btnXiazai, .btnShanchu").hide();
                            self.element.find(".wenjianId").val("");
                            self.element.find(".wenjianMingcheng").val("");
                        }
                        break;
			        case "wenjianMingcheng": 
                        self.element.find(".wenjianLianjie").text(value);
                        self.element.find(".wenjianMingcheng").val(value);
                        break;

		        }
	        },
            bangding: function(){
                this.options.wenjianId = this.element.find(".wenjianId").val();
                this.options.wenjianMingcheng = this.element.find(".wenjianMingcheng").val();
                if(this.options.wenjianId){
                    this.element.find(".wenjianLianjie").text(this.options.wenjianMingcheng).attr("href", this.options.edoc2BaseUrl+"/preview.aspx?key=yctZhishiku&fileId="+this.options.wenjianId).show();
                    this.element.find(".btnXiazai").attr("href", this.options.edoc2BaseUrl+"/Document/File_Download.aspx?key=yctZhishiku&file_Id="+this.options.wenjianId);
                    this.element.find(".btnXiazai, .btnShanchu").show();
                }
                else{
                    this.element.find(".wenjianLianjie, .btnXiazai, .btnShanchu").hide();
                }
            }
        }
    );

    $.widget(
        "ui.singleSelectUser", {
            options: {
            },
            _create: function(){
                var element = this.element;
                if($.debug){
                    element.find(".userName").removeAttr("readonly").val("qi");
                    element.find(".userAccount").show().val("qi");
                }
                else{
                    element.find(".userName").attr("readonly", "readonly");
                    element.find(".userAccount").hide();
                }
                element.find(":button").click(function(){
                    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:" + false + ",current: true}&deptTree={show:false}";
                        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
                        if (res != null && res.users != null) {
                            if(res.users.length > 0){
                                var user = res.users[res.users.length-1];
                                element.find(".userName").val(user._data.userRealName).focusout();
                                element.find(".userAccount").val(user._data.loginName);
                            }
                        }
                });
            }
        }
    );

    $.widget(
        "ui.multiSelectUser", {
            options: {
            },
            _create: function(){
                var element = this.element;
                var userAccount = element.find(".userAccount");
                var userName = element.find(".userName");
                if($.debug){
                    userAccount.show();
                    userName.removeAttr("readonly");
                }
                else{
                    userName.attr("readonly", "readonly");
                    userAccount.hide();
                }
                element.find(".selectButton").click(function(){
                    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:true,current: true}&deptTree={show:false}";
                    var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
                    if (res != null && res.users != null) {
                        if(res.users.length > 0){
                            $.each(res.users, function(i, user){
                                if(userName.val()){
                                    userName.val(userName.val() + "," + user._data.userRealName).focusout();
                                }
                                else{
                                    userName.val(user._data.userRealName).focusout();
                                }
                                if(userAccount.val()){
                                    userAccount.val(userAccount.val() + "," + user._data.loginName);
                                }
                                else{
                                    userAccount.val(user._data.loginName);
                                }
                            })
                        }
                    }
                });
                element.find(".resetButton").click(function(){
                    userName.val("");
                    userAccount.val("");
                });
            }
        }
    );
    
    $.widget(
        "ui.userEmailMultiSelect", {
            options: {
            },
            _create: function(){
                var element = this.element;
                element.find(":button").click(function(){
                    var url = edoc2BaseUrl + "/AppExt/Common/SelectOrgnization.aspx?userTree={show:true,multiSelect:" + true + ",current: true}&deptTree={show:false}";
                        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
                        var emailElement = element.find(".multiemail");
                        if (res != null && res.users != null) {
                            if(res.users.length > 0){
                                $.each(res.users, function(i, user){
                                    if(emailElement.val()){
                                        emailElement.val(emailElement.val() + ";" + user._data.email);
                                    }
                                    else{
                                        emailElement.val(user._data.email);
                                    }
                                })
                            }
                        }
                });
            }
        }
    );

    $.widget(
        "ui.fujianGrid", {
            options: {
                edoc2BaseUrl: null
            },
            _create: function(){
                var thiz = this;
                var element = this.element;
                this._fujianGrid = element.find(".datagrid").datagrid({
		            columns:[
			            {title: "名称", width: 300, field:"mingcheng"},
			            {title: "", width: 100, field:"edoc2Id", render: function(row, args){
                            var previewLink = thiz.options.edoc2BaseUrl+"/preview.aspx?key=yctZhishiku&fileId="+args.value;
                            var previewElement = $("<a target='_blank' href='#'>预览&nbsp;</a>").attr("href", previewLink);
                            var xiazaiLink = thiz.options.edoc2BaseUrl+"/Document/File_Download.aspx?key=yctZhishiku&file_Id="+args.value;
                            var xiazaiElement = $("<a target='_blank' href='#'>下载&nbsp;</a>").attr("href", xiazaiLink);
				            var deleteElement = $("<a href='#'>删除</a>").click(function(){
                                thiz._fujianGrid.datagrid("deleteRow", row);
                                return false;
                            });
                            return $("<span></span>")
                            .append(previewElement)
                            .append(xiazaiElement)
                            .append(deleteElement);
			            }}
		            ],
		            canSort: false,
		            singleSelect: true,
		            showNumberRow: false
	            });
                element.find(".btnShangchuan").click(function(){
                    $(this).attr("disabled", "disabled");
                    $.post("ZhishiGianliController.aspx?action=ChuangjianWenjianjia", null, function(data){
                        if(data.result == 0){
                            if($.debug){
                                thiz._fujianGrid.datagrid("appendRow", {edoc2Id: 1, mingcheng: "test.doc"});
                                return;
                            }
                            var url = thiz.options.edoc2BaseUrl + "/Document/File_UploadEx.aspx?key=yctZhishiku&folderid=" + data.data + "&multifile=true";
                            var win = openUploadWnd(url, 520, 360);
                            window.uploadCallback = function (filelist) {
                                if (filelist && filelist.length) {
                                    var fujianList = $.map(filelist, function(file){
                                        return {edoc2Id: file.fileId, mingcheng: file.fileName};
                                    })
                                    $.each(fujianList, function(i, fujian){
                                        thiz._fujianGrid.datagrid("appendRow", fujian);
                                    });
                                }
                                win.close();
                            };
                        }
                        else{
                            alert(data.message);
                            win.close();
                        }
                    });
                    $(this).removeAttr("disabled");
                });

                element.find(".btnGuanlianFujian").click(function(){
                    $(this).attr("disabled", "disabled");
                    $.post("ZhishiGianliController.aspx?action=ChuangjianWenjianjia", null, function(data){
                        if(data.result == 0){
                            if($.debug){
                                thiz._fujianGrid.datagrid("appendRow", {edoc2Id: 1, mingcheng: "test.doc"});
                                return;
                            }
                            var url = thiz.options.edoc2BaseUrl + "/AppExt/Common/SelectEdocFile.aspx";
                            var win = openUploadWnd(url, 620, 460);
                            window.uploadCallback = function (filelist) {
                                if (filelist && filelist.length) {
                                    var fujianList = $.map(filelist, function(file){
                                        return {edoc2Id: file.id, mingcheng: file.name};
                                    })
                                    $.each(fujianList, function(i, fujian){
                                        thiz._fujianGrid.datagrid("appendRow", fujian);
                                    });
                                }
                                win.close();
                            };
//                            var filelist = showModalWnd(url, 720, 460);
//                            alert(filelist)
//                            if (filelist && filelist.length) {
//                                var fujianList = $.map(filelist, function(file){
//                                    return {edoc2Id: file.id, mingcheng: file.name};
//                                })
//                                $.each(fujianList, function(i, fujian){
//                                    thiz._fujianGrid.datagrid("appendRow", fujian);
//                                });
//                            }
                        }
                        else{
                            alert(data.message);
                            win.close();
                        }
                    });
                    $(this).removeAttr("disabled");
                });
            },
            getFujianData: function(){
                return this._fujianGrid.datagrid("getRowsData");
            },
            setFujianData: function(data){
                this._fujianGrid.datagrid("option", "data", data);
            }
        }
    );
})(jQuery);

jQuery.extend({
    rselect: /^(?:select)/i,
    rbutton: /^(?:button|submit)/i,
    rtextarea: /^(?:textarea)/i,
    rinput: /^(?:color|date|datetime|email|hidden|month|number|password|range|search|tel|text|time|url|week)$/i,
    rradio: /^(?:radio)$/i,
    rcheckbox: /^(?:checkbox)$/i,
    getFormValue: function (form) {
        return $(form).getFormValue();
    }
});

jQuery.fn.extend({
    getFormValue: function () {
        var valueArray = this.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        })
		.filter(function () {
		    return this.name &&
				(this.checked || $.rselect.test(this.nodeName) || $.rtextarea.test(this.nodeName) ||
					$.rinput.test(this.type) );
		})
		.map(function (i, elem) {
		    var val = jQuery(this).val();

		    return val == null ?
				null :
				jQuery.isArray(val) ?
					jQuery.map(val, function (val, i) {
					    return { name: elem.name, value: val.replace(/\r?\n/g, "\r\n"), type: elem.type };
					}) :
					{ name: elem.name, value: val.replace(/\r?\n/g, "\r\n"), type: elem.type };
		}).get();

        var valueObj = {};
        $.each(valueArray, function (i, value) {
            if($.rcheckbox.test(value.type)){
                if(!valueObj[value.name]){
                    valueObj[value.name] = [];
                }
                valueObj[value.name].push(value.value);
            }
            else{
                valueObj[value.name] = value.value;
            }
        });
        return valueObj;
    },
    setFormValue: function (obj) {
        this.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        })
        .filter(function () {
            return this.name &&
				($.rradio.test(this.type) || $.rselect.test(this.nodeName) || $.rtextarea.test(this.nodeName) ||
					$.rinput.test(this.type) || $.rcheckbox.test(this.type));
        })
        .each(function () {
            if($.rradio.test(this.type)){
                if((this.name in obj) && $(this).val() == obj[this.name].toString()){
                    $(this).prop("checked", true);
                }
            }
            else if($.rcheckbox.test(this.type)){
                if((this.name in obj) && $(this).val() == obj[this.name].toString()){
                    $(this).prop("checked", true);
                }
            }
            else{
                $(this).val(obj[this.name]);
            }
        });
        return this;
    },
    setFormReadOnly: function (readonly) {
        this.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        })
        .filter(function () {
            return this.name &&
				($.rradio.test(this.type) || $.rselect.test(this.nodeName) || $.rtextarea.test(this.nodeName) ||
					$.rinput.test(this.type) );
        })
        .each(function () {
            if ($.rradio.test(this.type) || $.rselect.test(this.nodeName)) {
                $(this).prop("disabled", readonly);
            }
            else {
                $(this).prop("readonly", readonly);
            }
        });
        return this;
    },
    validAndFocus: function () {
        var validValue = this.valid();
        if (!validValue) {
            this.validate().focusInvalid();
        }
        return validValue;
    },
    dateRange: function(){
        this.each(function(){
            var inputs = $(this).find("input");
            inputs.eq(0).datepicker({ changeMonth: true, changeYear: true, onSelect: function(selectedDate){
                inputs.eq(1).datepicker( "option", "minDate", selectedDate );
            }});
            inputs.eq(1).datepicker({ changeMonth: true, changeYear: true, onSelect: function(selectedDate){
                inputs.eq(0).datepicker( "option", "maxDate", selectedDate );
            }});
        }); 
    }
});

(function($){
    $.widget("ui.pager", {
            start: 0,
            end: 0,
            index: 1,
            size: 0,
            count: 0,
            pageCount: 0,
            options: {
		        change: null
	        },
	        _create: function(){
                this.lblInfo = $("<span></span>");
                this.lnkPrevious = $("<a href='#'></a>").text("上一页");
                this.lnkNext = $("<a href='#'></a>").text("下一页");
                this.txtGo = $("<input />");
                this.lblIndex = $("<span></span>");
                this.element.addClass("ui-widget ui-pager")
                 .append(this.lblInfo)
                 .append(this.lnkPrevious)
                 .append(this.lnkNext)
                 .append(this.txtGo)
                 .append(this.lblIndex);

                var thiz = this;
                this.lnkPrevious.click(function(){
                    if(thiz.index == 1){
                        alert("已经是第一页了");
                        return;
                    }
    	            thiz._change(thiz.index - 1);
                    return false;
    	        });
                this.lnkNext.click(function(){
                    if(thiz.index == thiz.pageCount){
                        alert("已经是最后一页了");
                        return;
                    }
    	            thiz._change(thiz.index + 1);
                    return false;
    	        });
                this.txtGo.change(function(){
    	            thiz._change(parseInt($(this).val()));
    	        });
    	        this._render();
	        },
	        _change: function(index){
                if(index <= 0){
                    alert("跳转页必须大于0");
                    return;
                }
                if(index > this.pageCount){
                    alert("跳转页不能大于最大页");
                    return;
                }
                var start = this.size * (index - 1);
                var end = start + this.size - 1;
                if(this.count < end){
                    end = this.count;
                }
	            this.index = index;
                this.start = start;
                this.end = end;
	            this._trigger("change", null, {start: start, end: end});
	        },
            _render: function(){
                var _start = this.start + 1;
                var _end = this.end + 1;
                if(_end > this.count){
                    _end = this.count;
                }
                if(this.count <= 0){
                    _start = 0;
                }
                this.lblInfo.text("{0}-{1}/{2}".replace("{0}", _start).replace("{1}", _end).replace("{2}", this.count));
                this.txtGo.val(this.index);
                this.lblIndex.text("/" + this.pageCount);
            },
            _getPageCount: function(){
                if(this.size <= 0){
                    return 0;
                }
                var pageCount = parseInt(this.count / this.size);
                if((this.count % this.size) > 0){
                    pageCount = pageCount + 1;
                }
                return pageCount;
            },
            setPageInfo: function(info){
                this.start = info.start;
                this.end = info.end;
                this.count = info.count;
                this.size = info.size;
                this.index = parseInt((this.end + 1) / this.size);
                if(((this.end + 1) % this.size) > 0){    
                    this.index++;
                }
                this.pageCount = this._getPageCount();
                this._render();
            },
            getPageInfo: function(){
                var info = {}
                info.start = this.start;
                info.end = this.end;
                info.count = this.count;
                info.size = this.size;
                return info;
            }
        }
    );
})(jQuery);

$.ajaxSetup ({
    cache: false
});

$(function(){
    $('.dropdown-menu a').click(function (e) {
      e.preventDefault()
    })
})
