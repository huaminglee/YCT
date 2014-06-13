<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhishiHuoqu.aspx.cs" Inherits="EDoc2.YCT.Website.ZhishiHuoqu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>知识库</title>
        <link type="text/css" href='css/cupertino/jquery-ui-1.10.1.custom.min.css' rel="stylesheet" />
        <link type="text/css" href='css/multi-select.css' rel="stylesheet" />
        <link type="text/css" href='css/cupertino/jquery-ui-datagrid.css' rel="stylesheet" />
        <link type="text/css" href="css/cupertino/jquery-ui-tree.css" rel="stylesheet" />
        <link type="text/css" href="css/select2.css" rel="stylesheet" />
        <link type="text/css" href="css/bootstrap.min.css" rel="stylesheet" />
        <link type="text/css" href='css/default.css' rel="stylesheet" />
        <script type="text/javascript" src="js/jquery-1.9.1.js"> </script>
        <script type="text/javascript" src='js/bootstrap.min.js'> </script>
        <script type="text/javascript" src="js/jquery-ui-1.10.1.custom.min.js"> </script>
        <script type="text/javascript" src='js/jquery-validate.min.js'> </script>
        <script type="text/javascript" src='js/jquery.multi-select.js'> </script>
        <script type="text/javascript" src='js/jquery.quicksearch.js'> </script>
        <script type="text/javascript" src='js/jquery.ui.datagrid.js'> </script>
        <script type="text/javascript" src="js/jquery.ui.tree.js"> </script>
        <script type="text/javascript" src='js/jquery-extend.js'> </script>
        <style type="text/css">
            hr {
                border: 0;
                border-bottom: 1px solid #fff;
                border-top: 1px solid #eee;
                margin: 0px 0;
            }

            hr { border-color: #E1E1E8 }
        </style>
    </head>
    <body>
        <a style="color: blue"></a>
        <form id="form1" style="display: none;" runat="server"></form>
        <div class="container-fluid" style="padding-left: 0">
            <div class="row-fluid">
                <div class="span3">
                    <div id="muluPanel" style="float: left; overflow-x: auto; width: 200px;">
                        <div class="muluTree"></div>
                    </div>
                    <div class="bg-mainBgM" style="float: left; width: 4px;">&nbsp;</div>
                </div>
                <div class="span9" id="zhishiPanel">
                    <form id="zhishiChaxunForm">
                        <div class="btn-toolbar btn-group input-append">
                            <input name="chaxunWentiHuoDaan" type="text" />
                            <button class="btn btn-primary btnZhishiChaxun" type="submit"><i class="icon-white icon-search"></i>查询</button>
                            <button class="btn btnZhishiGaojiChaxun" type="button">高级搜索</button>
                        </div>
                    </form>
                    <div style="display: none" class="zhishiChaxunDialog" title="知识查询">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" style="float: left">问题</label>
                                <div style="float: left; width: 10px;">&nbsp;</div>
                                <input type="text" style="float: left" name="chaxunWenti"/>
                            </div>
                            <div class="control-group">
                                <label class="control-label" style="float: left">答案</label>
                                <div style="float: left; width: 10px;">&nbsp;</div>
                                <input type="text" style="float: left" name="chaxunDaan"/>
                            </div>
                            <div class="control-group">
                                <label class="control-label" style="float: left">附件</label>
                                <div style="float: left; width: 10px;">&nbsp;</div>
                                <input type="text" style="float: left" name="chaxunFujian"/>
                            </div>
                            <hr style="margin: 20px 0;" />
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                    <div class="zhishiDatagrid"></div>
                    <div class="zhishiDatagridPager pull-right" style="height: 30px">
                    </div>
                </div>
            </div>
        </div>
        <div class="zhishiChakanDialog" style="display: none;" title="查看知识">
            <h4 id="zhishiWenti"></h4>
            <hr />
            <div id="daanPanel">
            </div>
        </div>
    </body>
</html>

<script language="javascript" type="text/javascript">
    var muluTree = $(".muluTree");
    var zhishiDatagrid = $(".zhishiDatagrid");
    var zhishiDatagridPager = $(".zhishiDatagridPager");
    var btnZhishiGaojiChaxun = $(".btnZhishiGaojiChaxun");
    var btnZhishiChaxun = $(".btnZhishiChaxun");
    var zhishiChaxunDialog = $(".zhishiChaxunDialog");
    var zhishiChakanDialog = $(".zhishiChakanDialog").dialog({ autoOpen: false, modal: true, width: 650, height: 450 });

    zhishiDatagrid.datagrid({
        columns: [
            {
                title: "问题",
                width: 650,
                field: "wentiBiaoti",
                render: function(row, args) {
                    return $("<a target='_blank' href='#'>" + args.value + "</a>").click(function() {
                        chakanZhishi(args.data.id);
                        return false;
                    });
                }
            },
            { title: "修改人", width: 100, field: "xiugairen" },
            { title: "修改时间", width: 100, field: "xiugaiShijian" }
        ],
        canSort: false,
        singleSelect: true,
        showNumberRow: false,
        height: getZhishiDatagridHeight()
    });

    zhishiDatagridPager.pager({
        change: function() {
            jiazaiZhishi();
        }
    });

    zhishiChaxunDialog.dialog({ autoOpen: false, width: 400, modal: true })
        .find(".btn-primary").click(function() {
            jiazaiZhishi(true);
            zhishiChaxunDialog.dialog("close");
            return false;
        });

    btnZhishiGaojiChaxun.click(function() {
        zhishiChaxunDialog.dialog("open");
        return false;
    });

    btnZhishiChaxun.click(function() {
        jiazaiZhishi(false, true);
        return false;
    });

    $(window).resize(function() {
        $("#zhishiPanel .datagrid").datagrid("option", "height", getZhishiDatagridHeight());
    });

    function getZhishiDatagridHeight() {
        var bodyPaddingHeight = $(document.body).innerHeight() - $(document.body).height();
        return $(window).height() - ($("#zhishiPanel").height() - $("#zhishiPanel .datagrid").height()) - bodyPaddingHeight;
    }

    function jiazaiDingjiMulu() {
        $.get("ZhishiHuoquController.aspx", { action: "GetDingjiMulu" }, function(data) {
            if (data.result == 0) {
                muluTree.tree({
                    data: [data.data],
                    dblclickOpen: true,
                    treenodeLoading: function(event, treenode) {
                        jiazaiZiMulu(treenode);
                    },
                    treenodeSelected: function(event, treenode) {
                        jiazaiZhishi(true, true);
                    }
                });

                var topnodes = muluTree.tree("getTopNodes");
                topnodes.eq(0).treenode("expand").treenode("select");
            } else {
                alert(data.message);
            }
        });
    }

    function jiazaiZiMulu(treenode) {
        var muluId = treenode.treenode("option", "id");
        $.get("ZhishiHuoquController.aspx", { action: "GetZiMulu", muluId: muluId }, function(data) {
            if (data.result == 0) {
                if (data.data && data.data.length) {
                    $.each(data.data, function(i, nodeData) {
                        treenode.treenode("append", nodeData).treenode("expand");
                    });
                }
            } else {
                alert(data.message);
            }
        });
    }

    function jiazaiZhishi(qingchuChaxun, qingchuGaojiChaxun) {
        var selectedNode = muluTree.tree("getSelectedNode");

        var muluId = selectedNode.treenode("option", "id");
        if (qingchuChaxun) {
            $("#zhishiChaxunForm").setFormValue({});
        }
        if (qingchuGaojiChaxun) {
            zhishiChaxunDialog.find("form").setFormValue({});
        }
        var chaxunXinxi = zhishiChaxunDialog.find("form").getFormValue();
        var self = this;
        var pageInfo = zhishiDatagridPager.pager("getPageInfo");
        var args = {};
        args.action = "GetMuluZhishi";
        args.muluId = muluId;
        args.kaishiHang = pageInfo.start;
        args.jieshuHang = pageInfo.end;
        $.extend(args, chaxunXinxi);
        var zhishiChaxunFormValue = $("#zhishiChaxunForm").getFormValue();
        $.extend(args, zhishiChaxunFormValue);

        $.get("ZhishiHuoquController.aspx", args, function(data) {
            if (data.result == 0) {
                zhishiDatagrid.datagrid("option", "data", data.data.grid);
                zhishiDatagridPager.pager("setPageInfo", { start: data.data.kaishiHang, end: data.data.jieshuHang, count: data.data.zongHangshu, size: data.data.yedaxiao });
            } else {
                alert(data.message);
            }
        });
    }

    function chakanZhishi(zhishiId) {
        $("#daanPanel").html("");
        zhishiChakanDialog.dialog("open");
        $.post("ZhishiHuoquController.aspx", { action: "GetZhishi", zhishiId: zhishiId }, function(data) {
            if (data.result == 0) {
                var zhishi = data.data;
                $("#zhishiWenti").text(zhishi.wenti);
                $("#daanPanel").html(zhishi.daanHtml);
            } else {
                alert(data.message);
            }
        });
    }

    $("#zhishiChaxunForm").submit(function() {
        jiazaiZhishi(false, true);
        return false;
    });

    function setMuluPanel() {
        var rowFluidHeight = $(".row-fluid").height()-2;
        $(".span3").height(rowFluidHeight);

        var muluTreeWidth = $(".span3").width() - 4;
        var muluTreeHeight = $(".span3").height()-12;
        muluTree.width(muluTreeWidth);
        muluTree.height(muluTreeHeight);
    }

    jiazaiDingjiMulu();

    $(function() {
        setMuluPanel();
    });
</script>