using MasterSoft.OBEDataAcessLayer;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        SqlHelper objSQLHelper = new SqlHelper();
        // GET: Home
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            SqlParameter[] objParams = null;
            List<HomeModel> SessionList = new List<HomeModel>();
            try
            {
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@Id", 0);
                objParams[1] = new SqlParameter("@OrganizationId", Convert.ToInt32(Session["OrganizationId"]));
                ds = objSQLHelper.ExecuteDataSetSP("sptblMaster_Get", objParams);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SessionList.Add(new HomeModel()
                    {
                       Id = int.Parse(dr["Id"].ToString()),
                       Name = dr["Name"].ToString(),
                       
                    });
                }

            }
            catch (Exception ex)
            {
                string Msg, Desc;
                Msg = ex.Message;
                Desc = ex.StackTrace;
                if (Convert.ToInt32(Session["IsError"]) != 0)
                {
                    return RedirectToAction("CustomError", "Error", new { message = Msg, Desc = Desc });
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { message = Msg, Desc = Desc });
                }
            }
            //return Json(new { data = SessionList }, JsonRequestBehavior.AllowGet);
            return View();
        }

       public ActionResult SaveData(BloomCategoryModel ObjBloomCategory)
                {
                    object ret = 0;
                    try
                    {
                        SqlParameter[] objParams = null;
                        //Changing Parameters for each form.
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@BloomCategoryId", ObjBloomCategory.BloomCategoryId);
                        objParams[1] = new SqlParameter("@BloomCategoryName", ObjBloomCategory.BloomCategoryName);
                        objParams[2] = new SqlParameter("@BloomCategoryDescription", ObjBloomCategory.BloomCategoryDescription);
                        objParams[3] = new SqlParameter("@SequenceNo", ObjBloomCategory.SequenceNo);
                        objParams[4] = new SqlParameter("@FinalStatusId", ObjBloomCategory.FinalStatusId);
                        objParams[5] = new SqlParameter("@OrganizationId", Convert.ToInt32(Session["OrganizationId"]));
                        objParams[6] = new SqlParameter("@ModifiedBy", Session["USERID"]);
                        objParams[7] = new SqlParameter("@IPAddress", Session["ipAddress"]);
                        objParams[8] = new SqlParameter("@ActiveStatus", ObjBloomCategory.IsActive);
                        objParams[9] = new SqlParameter("@MACAddress", Session["macAddress"]);
                        objParams[10] = new SqlParameter("@BloomsTags", ObjBloomCategory.BloomsTags);

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("sptblBloomCategoryMaster_Insert_Update", objParams, true);
                        return Json(ret);
                    }
                    catch (Exception ex)
                    {
                        string Msg, Desc;
                        Msg = ex.Message;
                        Desc = ex.StackTrace;
                        if (Convert.ToInt32(Session["IsError"]) != 0)
                        {
                            return RedirectToAction("CustomError", "Error", new { message = Msg, Desc = Desc });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Error", new { message = Msg, Desc = Desc });
                        }
                    }
                }
    }
}
