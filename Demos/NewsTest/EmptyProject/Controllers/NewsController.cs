using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomModels.FlexigridModels;
using Test.Models;


namespace Test
{
    public class NewsController : Controller
    {

        int pageSize = 25;

        #region -----------------Index-----------------
        //***********************************************************************************************
        //
        // GET: /News/
        public ActionResult Index()
        {
              
            FlexigridViewModel flexigrid = new FlexigridViewModel("NewsList","NewsForm","NewsPost", "/News/List", "json", "Title", "asc", true, "News Listing", true, pageSize, true, 950, 2000);
            flexigrid.Columns = new List<FlexigridColumn>() 
            {
				new FlexigridColumn("LangID","LangID",86,true,"left",false,false),
				new FlexigridColumn("Type","Type",86,true,"left",false,false),
				new FlexigridColumn("Title","Title",86,true,"left",false,false),
				new FlexigridColumn("Details","Details",86,true,"left",false,false),
				new FlexigridColumn("PhotoFile","PhotoFile",86,true,"left",false,false),
				new FlexigridColumn("PhotoName","PhotoName",86,true,"left",false,false),
				new FlexigridColumn("AttachFile","AttachFile",86,true,"left",false,false),
				new FlexigridColumn("AttachName","AttachName",86,true,"left",false,false),
				new FlexigridColumn("EndDate","EndDate",86,true,"left",false,false),
				new FlexigridColumn("InsertDate","InsertDate",86,true,"left",false,false),
				new FlexigridColumn("LastModfiedDate","LastModfiedDate",86,true,"left",false,false)
            };
            flexigrid.Buttons = new List<FlexigridButton>() 
            {
                new FlexigridButton("Add","fgButton","Add"),
                new FlexigridButton("Edit","fgButton","Edit"),
                new FlexigridButton("Delete","fgButton","Delete")
            };
            flexigrid.DialogBoxId = "NewsDialog";
            flexigrid.DialogBoxWidth=700;
            flexigrid.DialogBoxHeight=800;
            ViewData["flexigrid"] = flexigrid;
            
			ViewData["LangID"] = LanguagesFactor.Get().Select(x => new SelectListItem { Text = x.Name, Value = x.LangID.ToString() }).ToList();
            //ViewData["Categories"] = CategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.CategoryID.ToString() }).ToList();
            return View(new NewsModel());

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
                List<NewsModel> NewsList = NewsFactor.GetPageByPage(page, pageSize, sortname, sortorder, qtype, query, out totalRecords);
                if (NewsList.Count > 0)
                {
                    return CreateFlexiJson(NewsList, page, totalRecords);
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
                NewsModel NewsObj = NewsFactor.GetObject(id);
                if (NewsObj == null)
                {
                    NewsObj = new NewsModel();
                }
                return Json(NewsObj, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(NewsModel NewsObj)
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
                bool result = NewsFactor.Create(NewsObj);
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
        public ActionResult Update(NewsModel NewsObj)
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
                bool result = NewsFactor.Update(NewsObj);
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
                NewsFactor.DeleteGroupofObjects(ids);
                return List(1,25, null, null, null, null);
            }
           catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        #region -----------------CreateFlexiJson-----------------
        //***********************************************************************************************
        private JsonResult CreateFlexiJson(IEnumerable<NewsModel> items, int page, int total)
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
                                    id = x.NewsID,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { 
                                        														x.LangID,
														x.Type,
														x.Title,
														x.Details,
														x.PhotoFile,
														x.PhotoName,
														x.AttachFile,
														x.AttachName,
														x.EndDate.ToString("MM/dd/yyyy"),
														x.InsertDate.ToString("MM/dd/yyyy"),
														x.LastModfiedDate.ToString("MM/dd/yyyy")
                                         }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }
        //***********************************************************************************************
        #endregion
        
    }
}
