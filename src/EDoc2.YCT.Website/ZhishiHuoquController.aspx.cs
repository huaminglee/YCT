using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDoc2.YCT.ZhishiGuanli;
using EDoc2.YCT.Website.Models;
using Newtonsoft.Json;

namespace EDoc2.YCT.Website
{
    public partial class ZhishiHuoquController : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }


            Response.ContentType = "application/json";

            if (action.Equals("GetZiMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetZiMulu();
            }
            else if (action.Equals("GetDingjiMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetDingjiMulu();
            }
            else if (action.Equals("GetMuluZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetMuluZhishi();
            }
            else if (action.Equals("GetZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetZhishi();
            }
        }

        private void GetDingjiMulu()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                Mulu mulu = WebHelper.Zhishiku.DingjiMulu;
                if (mulu != null)
                {
                    MuluTreeModel model = new MuluTreeModel(mulu);
                    actoinResultModel.data = model;
                }
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetZiMulu()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                if (mulu == null)
                {
                    throw new Exception("找不到目录，可能目录已被删除!");
                }
                List<Mulu> muluList = mulu.GetKeyulanMuluList(WebHelper.DangqianYonghuZhanghao);
                List<MuluTreeModel> models = muluList.Select(x => new MuluTreeModel(x)).ToList();
                actoinResultModel.data = models;
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                if (zhishi == null)
                {
                    throw new Exception("找不到知识，可能知识已经被删除!");
                }
                actoinResultModel.data = new ZhishiChakanModel(zhishi);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetMuluZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                int kaishiHang = int.Parse(Request["kaishiHang"]);
                if (kaishiHang <= 0)
                {
                    kaishiHang = 0;
                }
                int jieshuHang = int.Parse(Request["jieshuHang"]);
                if (jieshuHang <= 0)
                {
                    jieshuHang = 19;
                }
                string chaxunWenti = Request["chaxunWenti"];
                string chaxunDaan = Request["chaxunDaan"];
                string chaxunFujian = Request["chaxunFujian"];
                string chaxunWentiHuoDaan = Request["chaxunWentiHuoDaan"];
                List<Zhishi> zhishiList = null;
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);

                if (mulu == null)
                {
                    throw new Exception("找不到目录，可能目录已经被删除!");
                }
                if (string.IsNullOrEmpty(chaxunWentiHuoDaan))
                {
                    bool baohanZiMulu = !(string.IsNullOrEmpty(chaxunWenti) &&
                                          string.IsNullOrEmpty(chaxunDaan) &&
                                          string.IsNullOrEmpty(chaxunFujian));
                    zhishiList = mulu.ChaxunZhishiList(WebHelper.DangqianYonghuZhanghao, chaxunWenti, chaxunDaan,
                                                       chaxunFujian, baohanZiMulu);
                }
                else
                {
                    zhishiList = mulu.ChaxunZhishiList(WebHelper.DangqianYonghuZhanghao,
                                                       chaxunWentiHuoDaan.Split(new[] {' '},
                                                                                StringSplitOptions.RemoveEmptyEntries)
                                                                         .ToList(), true);
                }
                
                DataGridModel model = new DataGridModel();
                model.grid = zhishiList.Skip(kaishiHang).Take(20).Select(x => new ZhishiModel(x)).ToList();
                model.kaishiHang = kaishiHang;
                model.jieshuHang = jieshuHang;
                model.yedaxiao = 20;
                model.zongHangshu = zhishiList.Count;

                actoinResultModel.data = model;
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }
    }
}