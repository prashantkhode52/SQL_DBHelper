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
    }
}