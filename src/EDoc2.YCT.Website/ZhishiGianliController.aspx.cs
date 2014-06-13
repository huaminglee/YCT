using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using EDoc2.YCT.Website.Models;
using EDoc2.YCT.ZhishiGuanli;
using EDoc2.YCT.Core;
using EDoc2.Organization;
using EDoc2.Document;
using EDoc2.Website;

namespace EDoc2.YCT.Website
{
    public partial class ZhishiGianliController : System.Web.UI.Page
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
            else if (action.Equals("GetMemberSelect2", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetMemberSelect2();
            }
            else if (action.Equals("GetDingjiMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetDingjiMulu();
            }
            else if (action.Equals("GetZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetZhishi();
            }
            else if (action.Equals("GetLishiZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetLishiZhishi();
            }
            else if (action.Equals("GetLishiZhishiById", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetLishiZhishiById();
            }
            else if (action.Equals("GetMuluZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetMuluZhishi();
            }
            else if (action.Equals("ChuangjianMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ChuangjianMulu();
            }
            else if (action.Equals("XiugaiMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.XiugaiMulu();
            }
            else if (action.Equals("ShanchuMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ShanchuMulu();
            }
            else if (action.Equals("ChuangjianZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ChuangjianZhishi();
            }
            else if (action.Equals("XiugaiZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.XiugaiZhishi();
            }
            else if (action.Equals("ShanchuZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ShanchuZhishi();
            }
            else if (action.Equals("YidongZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.YidongZhishi();
            }
            else if (action.Equals("YidongMulu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.YidongMulu();
            }
            else if (action.Equals("TianjiaMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.TianjiaMuluQuanxian();
            }
            else if (action.Equals("GetMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetMuluQuanxian();
            }
            else if (action.Equals("XiugaiMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.XiugaiMuluQuanxian();
            }
            else if (action.Equals("ShanchuMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ShanchuMuluQuanxian();
            }
            else if (action.Equals("DeleteFile", StringComparison.InvariantCultureIgnoreCase))
            {
                this.DeleteFile();
            }
            else if (action.Equals("ChuangjianWenjianjia", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ChuangjianWenjianjia();
            }
            else if (action.Equals("SuodingZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.SuodingZhishi();
            }
            else if (action.Equals("JiesuoZhishi", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiesuoZhishi();
            }
            else if (action.Equals("Zhiding", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Zhiding();
            }
            else if (action.Equals("QuxiaoZhiding", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QuxiaoZhiding();
            }
            else if (action.Equals("GetDaanGuanliMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetDaanGuanliMuluQuanxian();
            }
            else if (action.Equals("TianjiaDaanGuanliMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.TianjiaDaanGuanliMuluQuanxian();
            }
            else if (action.Equals("XiugaiDaanGuanliMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.XiugaiDaanGuanliMuluQuanxian();
            }
            else if (action.Equals("ShanchuDaanGuanliMuluQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ShanchuDaanGuanliMuluQuanxian();
            }
            else if (action.Equals("GetDaanGuanliQuanxian", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetDaanGuanliQuanxian();
            }
        }

        #region DaanGuanliQuanxian
        //TODO: DaanGuanliQuanxian

        private void GetDaanGuanliMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                List<DaanGuanliQuanxian> daanGuanliQuanxianList = mulu.GetDaanGuanliQuanxianList();
                List<QuanxianModel> quanxianModels = daanGuanliQuanxianList.Select(x => new QuanxianModel(x, false)).ToList();
                List<Quanxian> jichengQuanxianList = mulu.GetJichengQuanxianList();
                quanxianModels.AddRange(jichengQuanxianList.Select(x => new QuanxianModel(x, true)));
                actoinResultModel.data = quanxianModels;
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void TianjiaDaanGuanliMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                string[] chengyuanIdSplit = Request["chengyuanId"].Split('_');
                int chengyuanId = int.Parse(chengyuanIdSplit[1]);
                ChengyuanLeixing chengyuancLeixing = ChengyuanLeixing.Bumen;
                if (chengyuanIdSplit[0] == "yonghuzu")
                {
                    chengyuancLeixing = ChengyuanLeixing.Yonghuzu;
                }
                else if (chengyuanIdSplit[0] == "bumen")
                {
                    chengyuancLeixing = ChengyuanLeixing.Bumen;
                }
                string chengyuanMingcheng = Request["chengyuanMingcheng"];
                QuanxianZhi quanxianZhi = (QuanxianZhi)int.Parse(Request["quanxianZhi"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                mulu.TianjiaDaanGuanliMuluQuanxian(chengyuanId, chengyuanMingcheng, chengyuancLeixing, quanxianZhi);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void XiugaiDaanGuanliMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                int quanxianId = int.Parse(Request["quanxianId"]);
                QuanxianZhi quanxianZhi = (QuanxianZhi)int.Parse(Request["quanxianZhi"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                DaanGuanliQuanxian daanGuanliQuanxian = mulu.GetDaanGuanliQuanxianList().Find(x => x.Id == quanxianId);
                daanGuanliQuanxian.Xiugai(quanxianZhi);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void ShanchuDaanGuanliMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                int quanxianId = int.Parse(Request["quanxianId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                DaanGuanliQuanxian daanGuanliQuanxian = mulu.GetDaanGuanliQuanxianList().Find(x => x.Id == quanxianId);
                daanGuanliQuanxian.Shanchu();
                actoinResultModel.message = "删除成功!";
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetDaanGuanliQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                //mulu.JiazaiDaanGuanliQuanxian();
                actoinResultModel.data = new DaanGuanliQuanxianModel(mulu);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }
        #endregion

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

        private void GetMemberSelect2()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                List<Select2Model<MemberSelect2Model>> models = new List<Select2Model<MemberSelect2Model>>();
                List<MemberSelect2Model> bumenSelect2Models = this.GetBumenSelect2Model();
                models.Add(new Select2Model<MemberSelect2Model> { text = "部门", children = bumenSelect2Models });

                List<MemberSelect2Model> yonghuzuSelect2Models = this.GetYonghuzuSelect2Model();
                models.Add(new Select2Model<MemberSelect2Model> { text = "用户组", children = yonghuzuSelect2Models });

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

        private List<MemberSelect2Model> GetBumenSelect2Model()
        {
#if DEBUG
            List<MemberSelect2Model> list = new List<MemberSelect2Model>();
            list.Add(new MemberSelect2Model { id = "bumen_1", text = "部门1" });
            list.Add(new MemberSelect2Model { id = "bumen_2", text = "部门2" });
            return list;
#else
            List<EDoc2DepartmentInfo> departmentInfos = this.GetChildDepartments(1);
            EDoc2DepartmentInfo topDept; 
            ApiManager.Api.OrgnizationManagement.GetDepartmentById(ApiManager.CurrentUserToken, 1, out topDept);
            departmentInfos.Add(topDept);
            return departmentInfos.Select(x => new MemberSelect2Model { id = "bumen_" + x.DeptId, text = x.DeptName }).ToList();
#endif
        }

        private List<EDoc2DepartmentInfo> GetChildDepartments(int parentId)
        {
            List<EDoc2DepartmentInfo> list = new List<EDoc2DepartmentInfo>();
            List<EDoc2DepartmentInfo> departmentInfos;
            ApiManager.Api.OrgnizationManagement.GetChildDepartments(ApiManager.CurrentUserToken, parentId, out departmentInfos);
            list.AddRange(departmentInfos);
            foreach (EDoc2DepartmentInfo deptInfo in departmentInfos)
            {
                List<EDoc2DepartmentInfo> children = this.GetChildDepartments(deptInfo.DeptId);
                list.AddRange(children);
            }
            return list;
        }

        private List<MemberSelect2Model> GetYonghuzuSelect2Model()
        {
#if DEBUG
            List<MemberSelect2Model> list = new List<MemberSelect2Model>();
            list.Add(new MemberSelect2Model { id = "yonghuzu_1", text = "用户组1" });
            list.Add(new MemberSelect2Model { id = "yonghuzu_2", text = "用户组2" });
            return list;
#else
            List<EDoc2UserGroupInfo> userGroupInfos;
            ApiManager.Api.OrgnizationManagement.GetChildUserGroups(ApiManager.CurrentUserToken, 0, out userGroupInfos);
            return userGroupInfos.Where(x => !x.Name.Equals("Everyone", StringComparison.InvariantCultureIgnoreCase) && !x.Name.Equals("Creators", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => new MemberSelect2Model { id = "yonghuzu_" + x.Id, text = x.Name }).ToList();
#endif
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
                    throw new Exception("找不到目录，可能目录已经被删除!");
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
                actoinResultModel.data = new ZhishiModel(zhishi);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetLishiZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                actoinResultModel.data = zhishi.GetLishiBanben().OrderByDescending(x => x.Banben).Select(x => new LishiZhishiModel(x));
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetLishiZhishiById()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                int lishiZhishiId = int.Parse(Request["lishiZhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                actoinResultModel.data = new ZhishiChakanModel(zhishi.GetLishiBanben().Find(x => x.Id == lishiZhishiId));
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
                bool baohanZiMulu = !string.IsNullOrEmpty(chaxunWenti) || !string.IsNullOrEmpty(chaxunDaan);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                List<Zhishi> zhishiList = mulu.ChaxunZhishiList(WebHelper.DangqianYonghuZhanghao, chaxunWenti, chaxunDaan, chaxunFujian, baohanZiMulu);
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

        private void ChuangjianMulu()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int fuMuluId = int.Parse(Request["fuMuluId"]);
                Mulu fuMulu = WebHelper.Zhishiku.GetMulu(fuMuluId);
                fuMulu.ChuangjianMulu(Request["mingcheng"], WebHelper.DangqianYonghuZhanghao);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void XiugaiMulu()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                mulu.Xiugai(Request["mingcheng"]);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void ShanchuMulu()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                mulu.Shanchu();
                actoinResultModel.message = "删除成功!";
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void ChuangjianZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                ChuangjianZhishiModel model = JsonConvert.DeserializeObject<ChuangjianZhishiModel>(Request["postJson"]);

                List<FujianXinxi> yijiDaanFujian = model.yijiDaanFujian == null ? null : model.yijiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> erjiDaanFujian = model.erjiDaanFujian == null ? null : model.erjiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> sanjiDaanFujian = model.sanjiDaanFujian == null ? null : model.sanjiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> sijiDaanFujian = model.sijiDaanFujian == null ? null : model.sijiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> wujiDaanFujian = model.wujiDaanFujian == null ? null : model.wujiDaanFujian.Select(x => x.Map()).ToList();
                Mulu mulu = WebHelper.Zhishiku.GetMulu(model.muluId);
                string yonghu = WebHelper.DangqianYonghuZhanghao;
                Zhishi zhishi = mulu.ChuangjianZhishi(model.wenti, model.shunxu, WebHelper.DangqianYonghuZhanghao,
                    model.yijiDaan, yijiDaanFujian, model.erjiDaan, erjiDaanFujian, model.sanjiDaan, sanjiDaanFujian,
                    model.sijiDaan, sijiDaanFujian, model.wujiDaan, wujiDaanFujian, yonghu);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void XiugaiZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                XiugaiZhishiModel model = JsonConvert.DeserializeObject<XiugaiZhishiModel>(Request["postJson"]);

                List<FujianXinxi> yijiDaanFujian = model.yijiDaanFujian == null ? null : model.yijiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> erjiDaanFujian = model.erjiDaanFujian == null ? null : model.erjiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> sanjiDaanFujian = model.sanjiDaanFujian == null ? null : model.sanjiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> sijiDaanFujian = model.sijiDaanFujian == null ? null : model.sijiDaanFujian.Select(x => x.Map()).ToList();
                List<FujianXinxi> wujiDaanFujian = model.wujiDaanFujian == null ? null : model.wujiDaanFujian.Select(x => x.Map()).ToList();
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(model.zhishiId);
                zhishi.Xiugai(model.wenti, model.shunxu,
                    model.yijiDaan, yijiDaanFujian, model.erjiDaan, erjiDaanFujian, model.sanjiDaan, sanjiDaanFujian,
                    model.sijiDaan, sijiDaanFujian, model.wujiDaan, wujiDaanFujian, true, WebHelper.DangqianYonghuZhanghao);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void ShanchuZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(int.Parse(Request["zhishiId"]));
                zhishi.Shanchu();
                actoinResultModel.message = "删除成功!";
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void YidongZhishi()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(int.Parse(Request["zhishiId"]));
                mulu.TianjiaZhishi(zhishi);
                actoinResultModel.message = "移动成功!";
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void YidongMulu()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int beiyidongdeMuluId = int.Parse(Request["beiyidongdeMuluId"]);
                Mulu beiyidongdeMulu = WebHelper.Zhishiku.GetMulu(beiyidongdeMuluId);
                int yidongdaodeMuluId = int.Parse(Request["yidongdaodeMuluId"]);
                Mulu yidongdaodeMulu = WebHelper.Zhishiku.GetMulu(yidongdaodeMuluId);
                yidongdaodeMulu.TianjiaMulu(beiyidongdeMulu);
                actoinResultModel.message = "移动成功!";
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void GetMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                List<Quanxian> quanxianList = mulu.GetQuanxianList();
                List<QuanxianModel> quanxianModels = quanxianList.Select(x => new QuanxianModel(x, false)).ToList();
                List<Quanxian> jichengQuanxianList = mulu.GetJichengQuanxianList();
                quanxianModels.AddRange(jichengQuanxianList.Select(x => new QuanxianModel(x, true)));
                actoinResultModel.data = quanxianModels;
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void TianjiaMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                string[] chengyuanIdSplit = Request["chengyuanId"].Split('_');
                int chengyuanId = int.Parse(chengyuanIdSplit[1]);
                ChengyuanLeixing chengyuancLeixing = ChengyuanLeixing.Bumen;
                if(chengyuanIdSplit[0] == "yonghuzu")
                {
                    chengyuancLeixing = ChengyuanLeixing.Yonghuzu;
                }
                else if(chengyuanIdSplit[0] == "bumen")
                {
                    chengyuancLeixing = ChengyuanLeixing.Bumen;
                }
                string chengyuanMingcheng = Request["chengyuanMingcheng"];
                QuanxianZhi quanxianZhi = (QuanxianZhi)int.Parse(Request["quanxianZhi"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                mulu.TianjiaQuanxian(chengyuanId, chengyuanMingcheng, chengyuancLeixing, quanxianZhi);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void XiugaiMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                int quanxianId = int.Parse(Request["quanxianId"]);
                QuanxianZhi quanxianZhi = (QuanxianZhi)int.Parse(Request["quanxianZhi"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                Quanxian quanxian = mulu.GetQuanxianList().Find(x => x.Id == quanxianId);
                quanxian.Xiugai(quanxianZhi);
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void ShanchuMuluQuanxian()
        {
            ActionResultModel actoinResultModel = new ActionResultModel();
            try
            {
                int muluId = int.Parse(Request["muluId"]);
                int quanxianId = int.Parse(Request["quanxianId"]);
                Mulu mulu = WebHelper.Zhishiku.GetMulu(muluId);
                Quanxian quanxian = mulu.GetQuanxianList().Find(x => x.Id == quanxianId);
                quanxian.Shanchu();
                actoinResultModel.message = "删除成功!";
            }
            catch (Exception ex)
            {
                actoinResultModel.result = ActionResult.Error;
                actoinResultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actoinResultModel));
        }

        private void DeleteFile()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string token;
                ApiManager.Api.OrgnizationManagement.Impersonate(2, "127.0.0.1", out token);
                int fileId = int.Parse(Request["wenjianId"]);
                int result = ApiManager.Api.DocumentManagement.DeleteFile(token, fileId, false);
                if (result != 0)
                {
                    throw new Exception("删除文件失败:" + result);
                }
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ChuangjianWenjianjia()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                int folderId = this.CreateFolder(WebHelper.ZhishikuWenjianjiaId, Guid.NewGuid().ToString());
                resultModel.data = folderId;
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void SuodingZhishi()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                zhishi.Suoding(WebHelper.DangqianYonghuZhanghao);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void JiesuoZhishi()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                zhishi.Jiesuo();
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Zhiding()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                zhishi.Zhiding();
                resultModel.message = "置顶成功";
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QuxiaoZhiding()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                int zhishiId = int.Parse(Request["zhishiId"]);
                Zhishi zhishi = WebHelper.Zhishiku.GetZhishi(zhishiId);
                zhishi.QuxiaoZhiding();
                resultModel.message = "已取消置顶";
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private IEDoc2Folder GetFolder(string token, int parentId, string folderName)
        {
            IEDoc2Folder folder = null;
            List<IEDoc2Folder> folderList = null;
            ApiManager.Api.DocumentManagement.GetChildFolderList(token, parentId, out folderList);

            foreach (IEDoc2Folder tempFolder in folderList)
            {
                if (tempFolder.FolderName == folderName)
                {
                    folder = tempFolder;
                    break;
                }
            }
            return folder;
        }

        private int CreateFolder(int parentId, string folderName)
        {
#if DEBUG
            return 1;
#endif
            string token;
            ApiManager.Api.OrgnizationManagement.Impersonate(2, "127.0.0.1", out token);
            int folderId = 0;
            IEDoc2Folder folder = GetFolder(token, parentId, folderName);
            if (folder != null)
            {
                folderId = folder.FolderId;
            }
            else
            {
                int result = ApiManager.Api.DocumentManagement.CreateFolder(token, parentId, folderName,
                        "", 0, 0, 0, "", "", 1, out folder);
                if (result != 0)
                {
                    throw new Exception(string.Format("创建文件夹失败，错误编号：{0}, parentId:{1}, folderName:{2}", result, parentId, folderName));
                }
                folderId = folder.FolderId;
            }
            return folderId;
        }
    }
};