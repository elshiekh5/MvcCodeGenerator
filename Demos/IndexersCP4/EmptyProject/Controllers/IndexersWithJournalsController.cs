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
    public class IndexersWithJournalsController : Controller
    {

        int pageSize = 25;

        #region -----------------Index-----------------
        //***********************************************************************************************
        //
        // GET: /IndexersWithJournals/
        public ActionResult Index()
        {
              
            FlexigridViewModel flexigrid = new FlexigridViewModel("IndexersWithJournalsList","IndexersWithJournalsForm","IndexersWithJournalsPost", "/IndexersWithJournals/List", "json", "JournalCode", "asc", true, "IndexersWithJournals Listing", true, pageSize, true, 950, 2000);
            flexigrid.Columns = new List<FlexigridColumn>() 
            {
				new FlexigridColumn("IndexerID","IndexerID",105,true,"left",false,false),
				new FlexigridColumn("JournalCode","JournalCode",105,true,"left",false,false),
				new FlexigridColumn("SubmissionDate","SubmissionDate",105,true,"left",false,false),
				new FlexigridColumn("AcceptanceDate","AcceptanceDate",105,true,"left",false,false),
				new FlexigridColumn("RejectionDate","RejectionDate",105,true,"left",false,false),
				new FlexigridColumn("NextEvaRound","NextEvaRound",105,true,"left",false,false),
				new FlexigridColumn("NoofEvaRound","NoofEvaRound",105,true,"left",false,false),
				new FlexigridColumn("Notes","Notes",105,true,"left",false,false),
				new FlexigridColumn("StatusID","StatusID",105,true,"left",false,false)
            };
            flexigrid.Buttons = new List<FlexigridButton>() 
            {
                new FlexigridButton("Add","fgButton","Add"),
                new FlexigridButton("Edit","fgButton","Edit"),
                new FlexigridButton("Delete","fgButton","Delete")
            };
            flexigrid.DialogBoxId = "IndexersWithJournalsDialog";
            flexigrid.DialogBoxWidth=700;
            flexigrid.DialogBoxHeight=800;
            ViewData["flexigrid"] = flexigrid;
            
			ViewData["IndexerID"] = IndexersFactor.Get().Select(x => new SelectListItem { Text = x.Name, Value = x.IndexerID.ToString() }).ToList();
			ViewData["JournalCode"] = JournalsFactor.Get().Select(x => new SelectListItem { Text = x.JournalCode, Value = x.JournalCode.ToString() }).ToList();
			ViewData["StatusID"] = StatusFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.StatusID.ToString() }).ToList();
            //ViewData["Categories"] = CategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.CategoryID.ToString() }).ToList();
            return View(new IndexersWithJournalsModel());

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
                List<IndexersWithJournalsModel> IndexersWithJournalsList = IndexersWithJournalsFactor.GetPageByPage(page, pageSize, sortname, sortorder, qtype, query, out totalRecords);
                if (IndexersWithJournalsList.Count > 0)
                {
                    return CreateFlexiJson(IndexersWithJournalsList, page, totalRecords);
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
                IndexersWithJournalsModel IndexersWithJournalsObj = IndexersWithJournalsFactor.GetObject(id);
                if (IndexersWithJournalsObj == null)
                {
                    IndexersWithJournalsObj = new IndexersWithJournalsModel();
                }
                return Json(IndexersWithJournalsObj, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(IndexersWithJournalsModel IndexersWithJournalsObj)
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
                bool result = IndexersWithJournalsFactor.Create(IndexersWithJournalsObj);
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
        public ActionResult Update(IndexersWithJournalsModel IndexersWithJournalsObj)
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
                bool result = IndexersWithJournalsFactor.Update(IndexersWithJournalsObj);
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
                IndexersWithJournalsFactor.DeleteGroupofObjects(ids);
                return List(1,25, null, null, null, null);
            }
           catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        #region -----------------CreateFlexiJson-----------------
        //***********************************************************************************************
        private JsonResult CreateFlexiJson(IEnumerable<IndexersWithJournalsModel> items, int page, int total)
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
                                    id = x.ID,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { 
                                        														x.IndexerID,
														x.JournalCode,
														x.SubmissionDate.ToString("MM/dd/yyyy"),
														x.AcceptanceDate.ToString("MM/dd/yyyy"),
														x.RejectionDate.ToString("MM/dd/yyyy"),
														x.NextEvaRound,
														x.NoofEvaRound,
														x.Notes,
														x.StatusID
                                         }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }
        //***********************************************************************************************
        #endregion
        
    }
}
