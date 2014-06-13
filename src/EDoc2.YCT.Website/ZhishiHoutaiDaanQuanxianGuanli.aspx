<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhishiHoutaiDaanQuanxianGuanli.aspx.cs" Inherits="EDoc2.YCT.Website.ZhishiHoutaiDaanQuanxianGuanli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>后台答案管理权限</title>
        <link type="text/css" href='css/cupertino/jquery-ui-1.10.1.custom.min.css' rel="stylesheet" />
        <link type="text/css" href='css/cupertino/jquery-ui-datagrid.css' rel="stylesheet" />
        <link type="text/css" href="css/cupertino/jquery-ui-tree.css" rel="stylesheet" />
        <link type="text/css" href="css/select2.css" rel="stylesheet" />
        <link type="text/css" href="css/bootstrap.min.css" rel="stylesheet" />
        <link type="text/css" href='css/default.css' rel="stylesheet" />
        <script type="text/javascript" src="js/jquery-1.9.1.js"> </script>
        <script type="text/javascript" src='js/bootstrap.min.js'> </script>
        <script type="text/javascript" src="js/jquery-ui-1.10.1.custom.min.js"> </script>
        <script type="text/javascript" src='js/jquery-validate.min.js'> </script>
        <script type="text/javascript" src='js/jquery.quicksearch.js'> </script>
        <script type="text/javascript" src='js/jquery.ui.datagrid.js'> </script>
        <script type="text/javascript" src="js/jquery.ui.tree.js"> </script>
        <script type="text/javascript" src='js/jquery-extend.js'> </script>
        <script type="text/javascript" src='js/select2.js'> </script>
        <script type="text/javascript" src='js/select2_locale_zh-CN.js'> </script>
        <script type="text/javascript" src='js/jquery.json-2.3.js'> </script>
    
        <script type="text/javascript" src='js/houtaiDananQuanxianMuluGuanli.js'> </script>
        <script type="text/javascript" src='js/houtaiDaanMuluQuanxianGuanli.js'> </script>
        <script type="text/javascript" src='js/tianjiaHoutaiDaanMuluQuanxianDialog.js'> </script>
        <script type="text/javascript" src='js/xiugaiHoutaiDaanMuluQuanxianDialog.js'> </script>
        <script type="text/javascript" src='js/muluXuanzeDialog.js'> </script>
    </head>
    <body>
        <form id="form1" style="display: none;" runat="server">
        
        </form>
        <div style="display: none;" id="muluXuanzeDialog" title="目录选择">
            <div style="height: 200px;">
                <div class="muluTree"></div>
            </div>
            <hr />
            <button type="button" class="btn pull-right btnClose">关闭</button>
            <button type="button" class="btn pull-right btn-primary btnOk">确定</button>
        </div>
        <div class="container-fluid" style="padding-left: 0">
            <div class="row-fluid">
                <div class="span3" id="muluPanel">
                    <div id="treeContainer" style="float: left; overflow-x: auto; width: 200px;">
                        <div class="btn-toolbar btn-group" >
                            <a class="btn btnShuaxin" href="#">刷新</a>
                        </div>
                        <div class="tree"></div>
                    </div>
                    <div class="bg-mainBgM" style="float: left; width: 4px;">&nbsp;</div>
                </div>
                <div class="span9 muluQuanxianDialog" id="muluQuanxianDialog">
                    <div class="btn-toolbar btn-group" >
                        <button class="btn btnTianjia"><i class="icon-plus"></i>添加</button>
                        <button class="btn btnXiugai"><i class="icon-edit"></i>修改</button>
                        <button class="btn btnShanchu"><i class="icon-trash"></i>删除</button>
                    </div>
                    <div class="quanxianGrid"></div>
                    <hr />
                    <div class="tianjiaMuluQuanxianDialog" style="display: none" title="添加权限">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" >成员</label>
                                <div class="controls">
                                    <input type="hidden" class="chengyuanSelect2" style="width: 300px"/>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >一级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youYijiDaanQuanxian" value="1" checked />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >二级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youErjiDaanQuanxian" value="2" checked />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >三级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youSanjiDaanQuanxian" value="4" checked />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >四级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youSijiDaanQuanxian" value="8" checked />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >五级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youWujiDaanQuanxian" value="16" checked />
                                    </label>
                                </div>
                            </div>
                            <hr />
                            <button type="button" class="btn pull-right btnClose">关闭</button>
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                    <div class="xiugaiMuluQuanxianDialog" style="display: none" title="修改权限">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" >成员</label>
                                <div class="controls">
                                    <input type="text" name="chengyuanMingcheng" readonly="readonly" class="text" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >一级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youYijiDaanQuanxian" value="1"  />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >二级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youErjiDaanQuanxian" value="2"  />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >三级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youSanjiDaanQuanxian" value="4"  />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >四级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youSijiDaanQuanxian" value="8"  />
                                    </label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >五级答案</label>
                                <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="youWujiDaanQuanxian" value="16"  />
                                    </label>
                                </div>
                            </div>
                            <hr />
                            <button type="button" class="btn pull-right btnClose">关闭</button>
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </body>
</html>

<script language="javascript" type="text/javascript">
    var edoc2BaseUrl = "";
    var muluXuanzeDialog = $("#muluXuanzeDialog").muluXuanzeDialog();
    var muluQuanxianDialog = $("#muluQuanxianDialog").houtaiDaanMuluQuanxianGuanli();

    $("#muluQuanxianDialog").houtaiDananQuanxianMuluGuanli({ muluXuanzeDialog: muluXuanzeDialog });
    $("#muluPanel").houtaiDananQuanxianMuluGuanli({ muluQuanxianDialog: $("#muluQuanxianDialog"), muluXuanzeDialog: muluXuanzeDialog });

    function setTreeContainer() {
        var treeContainer = $('#treeContainer');
        var muluTreeWidth = $(".span3").width() - 6;
        //        var muluTreeHeight = $(".span3").height();
        treeContainer.width(muluTreeWidth);
        treeContainer.height(525);
    }

    $(function () {
        setTreeContainer();
    });
</script>