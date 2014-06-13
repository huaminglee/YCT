<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhishiGianli.aspx.cs" Inherits="EDoc2.YCT.Website.ZhishiGianli" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>知识库</title>
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

        <script type="text/javascript" src='js/muluGuanli.js'> </script>
        <script type="text/javascript" src='js/zhishiGuanli.js'> </script>
        <script type="text/javascript" src='js/chuangjianMuluDialog.js'> </script>
        <script type="text/javascript" src='js/xiugaiMuluDialog.js'> </script>
        <script type="text/javascript" src='js/muluQuanxianDialog.js'> </script>
        <script type="text/javascript" src='js/tianjiaMuluQuanxianDialog.js'> </script>
        <script type="text/javascript" src='js/xiugaiMuluQuanxianDialog.js'> </script>
        <script type="text/javascript" src='js/chuangjianZhishiDialog.js'> </script>
        <script type="text/javascript" src='js/xiugaiZhishiDialog.js'> </script>
        <script type="text/javascript" src='js/zhishiChaxunDialog.js'> </script>
        <script type="text/javascript" src='js/lishiZhishiDialog.js'> </script>
        <script type="text/javascript" src='js/zhishiChakanDialog.js'> </script>
        <script type="text/javascript" src='js/muluXuanzeDialog.js'> </script>
    </head>
    <body>
        <form id="form1" style="display: none;" runat="server"></form>
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
                            <button class="btn btnChuangjianMulu"><i class="icon-plus"></i>创建</button>
                            <div class="btn-group">
                                <button class="btn dropdown-toggle" data-toggle="dropdown">更多<span class="caret"></span></button>
                                <ul class="dropdown-menu">
                                    <li><a tabindex="-1" class="btnMuluQuanxian" href="#">权限</a></li>
                                    <li><a tabindex="-1" class="btnXiugaiMulu" href="#">修改</a></li>
                                    <li><a tabindex="-1" class="btnShanchuMulu" href="#">删除</a></li>
                                    <li><a tabindex="-1" class="btnShuaxin" href="#">刷新</a></li>
                                    <li><a tabindex="-1" class="btnYidong" href="#">移动</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="tree"></div>
                    </div>
                    <div class="bg-mainBgM" style="float: left; width: 4px;">&nbsp;</div>
                    <div class="chuangjianMuluDialog" style="display: none" title="创建目录">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" >目录名称</label>
                                <div class="controls">
                                    <input type="text" name="mingcheng" data-required="true"/>
                                </div>
                            </div>
                            <hr />
                            <button type="button" class="btn pull-right btnClose">关闭</button>
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                    <div class="xiugaiMuluDialog" style="display: none" title="修改目录">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" >目录名称</label>
                                <div class="controls">
                                    <input type="text" name="mingcheng" data-required="true"/>
                                </div>
                            </div>
                            <hr />
                            <button type="button" class="btn pull-right btnClose">关闭</button>
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                    <div class="muluQuanxianDialog" style="display: none" title="目录权限">
                        <div class="btn-toolbar btn-group" >
                            <button class="btn btnTianjia"><i class="icon-plus"></i>添加</button>
                            <button class="btn btnXiugai"><i class="icon-edit"></i>修改</button>
                            <button class="btn btnShanchu"><i class="icon-trash"></i>删除</button>
                        </div>
                        <div class="quanxianGrid"></div>
                        <hr />
                        <button type="button" class="btn btn-primary pull-right btnClose">关闭</button>
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
                <div class="span9" id="zhishiPanel">
                    <div class="btn-toolbar btn-group">
                        <button class="btn btnChuangjian"><i class="icon-plus"></i>创建</button>
                        <button class="btn btnShanchu"><i class="icon-trash"></i>删除</button>
                        <button class="btn btnChaxun" ><i class="icon-search"></i>查询</button>
                        <button class="btn btnYidong" ><i class="icon-edit"></i>移动</button>
                        <button class="btn btnZhiding" ><i class="icon-thumbs-up"></i>置顶</button>
                        <button class="btn btnQuxiaoZhiding" ><i class="icon-thumbs-down"></i>取消置顶</button>
                    </div>
                    <div style="display: none" class="chaxunPanel" title="知识查询">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" >问题</label>
                                <div class="controls">
                                    <input type="text" name="chaxunWenti"/>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >答案</label>
                                <div class="controls">
                                    <input type="text" name="chaxunDaan"/>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" >附件</label>
                                <div class="controls">
                                    <input type="text" name="chaxunFujian"/>
                                </div>
                            </div>
                            <hr />
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                    <div class="datagrid"></div>
                    <div class="datagridPager pull-right" style="height: 30px">
                
                    </div>
            
                    <div class="lishiZhishiDialog" style="display: none" title="历史版本">
                        <div class="lishiZhishiGrid"></div>
                        <div class="zhishiChakanDialog" style="display: none" title="查看知识">

                            <h4 class="zhishiWenti"></h4>
                            <hr />
        
                            <div class="daanPanel">
            
                            </div>
        
                        </div>
                    </div>
                    <div class="chuangjianZhishiDialog" style="display: none" title="创建知识">
                        <form class="form-horizontal" >
                            <div class="control-group">
                                <label class="control-label" >问题</label>
                                <div class="controls">
                                    <input type="text" name="wenti" class="input-xxlarge" data-required="true"/>
                                </div>
                            </div>
                            <div class="control-group yijiDaanContainer">
                                <label class="control-label" >一级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="yijiDaan" ></textarea>
                                    <div class="yijiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                        
                            </div>
                            <div class="control-group erjiDaanContainer">
                                <label class="control-label" >二级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="erjiDaan" ></textarea>
                                    <div class="erjiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group sanjiDaanContainer">
                                <label class="control-label" >三级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="sanjiDaan" ></textarea>
                                    <div class="sanjiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group sijiDaanContainer">
                                <label class="control-label" >四级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="sijiDaan" ></textarea>
                                    <div class="sijiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group wujiDaanContainer">
                                <label class="control-label" >五级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="wujiDaan" ></textarea>
                                    <div class="wujiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="control-group">
                        <label class="control-label" >排序</label>
                        <div class="controls">
                            <input type="text" name="shunxu" data-pattern="^[0-9]*[1-9][0-9]*$"/>
                            <span class="help-inline" style="display: none">请输入整数</span>
                        </div>
                    </div>--%>
                            <hr />
                            <button type="button" class="btn pull-right btnClose">关闭</button>
                            <button type="submit" class="btn pull-right btn-primary">确定</button>
                        </form>
                    </div>
                    <div class="xiugaiZhishiDialog" style="display: none" title="修改知识">
                        <form class="form-horizontal" >
                            <div class="zhishiZhuangtai" style="color: Red; display: none; font-weight: bold;"></div>
                            <div class="control-group">
                                <label class="control-label" >问题</label>
                                <div class="controls">
                                    <input type="text" name="wenti" class="input-xxlarge" data-required="true"/>
                                </div>
                            </div>
                            <div class="control-group yijiDaanContainer">
                                <label class="control-label" >一级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="yijiDaan" ></textarea>
                                    <div class="yijiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                        
                            </div>
                            <div class="control-group erjiDaanContainer">
                                <label class="control-label" >二级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="erjiDaan" ></textarea>
                                    <div class="erjiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group sanjiDaanContainer">
                                <label class="control-label" >三级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="sanjiDaan" ></textarea>
                                    <div class="sanjiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group sijiDaanContainer">
                                <label class="control-label" >四级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="sijiDaan" ></textarea>
                                    <div class="sijiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="control-group wujiDaanContainer">
                                <label class="control-label" >五级答案</label>
                                <div class="controls">
                                    <textarea rows="3" class="input-xxlarge" name="wujiDaan" ></textarea>
                                    <div class="wujiDananFujianGrid">
                                        <div class="btn-group">
                                            <button type="button" class="btn btnShangchuan"><i class="icon-upload"></i>上传附件</button>
                                            <button type="button" class="btn btnGuanlianFujian"><i class="icon-plus"></i>关联附件</button>
                                        </div>
                                        <div class="datagrid"></div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="control-group">
                        <label class="control-label" >排序</label>
                        <div class="controls">
                            <input type="text" name="shunxu" data-pattern="^[0-9]*[1-9][0-9]*$" />
                            <span class="help-inline" style="display: none">请输入整数</span>
                        </div>
                    </div>--%>
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
    $("#zhishiPanel").zhishiGuanli({ edoc2BaseUrl: edoc2BaseUrl, zhishiDatagridHeight: getZhishiDatagridHeight(), muluXuanzeDialog: muluXuanzeDialog });
    $("#muluPanel").muluGuanli({ zhishiGuanli: $("#zhishiPanel"), muluXuanzeDialog: muluXuanzeDialog });

    $(window).resize(function () {
        $("#zhishiPanel .datagrid").datagrid("option", "height", getZhishiDatagridHeight());
    });

    function getZhishiDatagridHeight() {
        var bodyPaddingHeight = $(document.body).innerHeight() - $(document.body).height();
        return $(window).height() - ($("#zhishiPanel").height() - $("#zhishiPanel .datagrid").height()) - bodyPaddingHeight;
    }

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