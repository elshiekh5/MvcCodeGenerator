using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomModels.FlexigridModels;
using IndexersCP4.Models;


namespace IndexersCP4
{
    public class RoundsLogController : Controller
    {

        int pageSize = 25;

        #region -----------------Index-----------------
        //***********************************************************************************************
        //
        // GET: /RoundsLog/
        public ActionResult Index()
        {
              
            FlexigridViewModel flexigrid = new FlexigridViewModel("RoundsLogList","RoundsLogForm","RoundsLogPost", "/RoundsLog/List", "json", "JournalCode", "asc", true, "RoundsLog Listing", true, pageSize, true, 950, 2000);
            flexigrid.Columns = new List<FlexigridColumn>() 
            {
				new FlexigridColumn("IndexerID","IndexerID",158,true,"left",false,false),
				new FlexigridColumn("JournalCode","JournalCode",158,true,"left",false,false),
				new FlexigridColumn("RoundDate","RoundDate",158,true,"left",false,false),
				new FlexigridColumn("StatusID","StatusID",158,true,"left",false,false),
				new FlexigridColumn("NextEvaRound","NextEvaRound",158,true,"left",false,false),
				new FlexigridColumn("Notes","Notes",158,true,"left",false,false)
            };
            flexigrid.Buttons = new List<FlexigridButton>() 
            {
                new FlexigridButton("Add","fgButton","Add"),
                new FlexigridButton("Edit","fgButton","Edit"),
                new FlexigridButton("Delete","fgButton","Delete")
            };
            flexigrid.DialogBoxId = "RoundsLogDialog";
            flexigrid.DialogBoxWidth=700;
            flexigrid.DialogBoxHeight=800;
            ViewData["flexigrid"] = flexigrid;
            
			ViewData["IndexerID"] = IndexersFactor.Get().Select(x => new SelectListItem { Text = x.Name, Value = x.IndexerID.ToString() }).ToList();
			ViewData["JournalCode"] = JournalsFactor.Get().Select(x => new SelectListItem { Text = x.JournalCode, Value = x.JournalCode.ToString() }).ToList();
			ViewData["StatusID"] = StatusFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.StatusID.ToString() }).ToList();
            //ViewData["Categories"] = CategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.CategoryID.ToString() }).ToList();
            return View(new RoundsLogModel());

        }
        //***********************************************************************************************
        #endregion

        #region -----------------List-----------------
        //***********************************************************************************************
        public ActionResult List(int page, int rp, string sortname, string sortorder, string qtype, string query)
        {
            try
            {
                int pageSize = rp; int totalRecords = 0;
                List<RoundsLogModel> RoundsLogList = RoundsLogFactor.GetPageByPage(page, pageSize, sortname, sortorder, qtype, query, out totalRecords);
                if (RoundsLogList.Count > 0)
                {
                    return CreateFlexiJson(RoundsLogList, page, totalRecords);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //***********************************************************************************************
        #endregion

        #region -----------------GetObject-----------------
        //***********************************************************************************************
        [HttpGet]
        public ActionResult GetObject(int id)
        {
            try
            {
                RoundsLogModel RoundsLogObj = RoundsLogFactor.GetObject(id);
                if (RoundsLogObj == null)
                {
                    RoundsLogObj = new RoundsLogModel();
                }
                return Json(RoundsLogObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
        //***********************************************************************************************
        #endregion
        
        #region -----------------Create-----------------
        //***********************************************************************************************
        [HttpPost]
        public ActionResult Create(RoundsLogModel RoundsLogObj)
        {
            //------------------------------------------
            //Check ModelState
            //------------------------------------------
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Invalid data");
            }
            //------------------------------------------
            try
            {
                bool result = RoundsLogFactor.Create(RoundsLogObj);
                if (result == true)
                {
                    return List(1,25, null, null, null, null);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Saving operation faild");
                }
            }
            catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        #region -----------------Update-----------------
        //***********************************************************************************************
        [HttpPost]
        public ActionResult Update(RoundsLogModel RoundsLogObj)
        {
            //------------------------------------------
            //Check ModelState
            //------------------------------------------
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Invalid data");
            }
            //------------------------------------------
            try
            {
                bool result = RoundsLogFactor.Update(RoundsLogObj);
                if (result == true)
                {
                    return List(1,25, null, null, null, null);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Saving operation faild");
                }
            }
            catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        #region -----------------Delete-----------------
        //***********************************************************************************************
        public ActionResult Delete(int[] ids)
        {
            //------------------------------------------
            //Check ModelState
            //------------------------------------------
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Invalid data");
            }
            //------------------------------------------
            try
            {
                RoundsLogFactor.DeleteGroupofObjects(ids);
                return List(1,25, null, null, null, null);
            }
           catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        #region -----------------CreateFlexiJson-----------------
        //***********************************************************************************************
        private JsonResult CreateFlexiJson(IEnumerable<RoundsLogModel> items, int page, int total)
        {
            return Json(
                    new
                    {
                        page,
                        total,
                        rows =
                            items
                            .Select(x =>
                                new
                                {
                                    id = x.RoundID,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { 
                                        														x.IndexerID,
														x.JournalCode,
														x.RoundDate.ToString("MM/dd/yyyy"),
														x.StatusID,
														x.NextEvaRound,
														x.Notes
                                         }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }
        //***********************************************************************************************
        #endregion
        
    }
}
