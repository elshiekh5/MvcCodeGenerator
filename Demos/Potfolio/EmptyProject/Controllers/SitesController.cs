using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomModels.FlexigridModels;
using Portfolio.Models;


namespace Portfolio
{
    public class SitesController : Controller
    {

        int pageSize = 25;

        #region -----------------Index-----------------
        //***********************************************************************************************
        //
        // GET: /Sites/
        public ActionResult Index()
        {
              
            FlexigridViewModel flexigrid = new FlexigridViewModel("SitesList","SitesForm","SitesPost", "/Sites/List", "json", "Name", "asc", true, "Sites Listing", true, pageSize, true, 950, 2000);
            flexigrid.Columns = new List<FlexigridColumn>() 
            {
				new FlexigridColumn("Name","Name",105,true,"left",false,false),
				new FlexigridColumn("Url","Url",105,true,"left",false,false),
				new FlexigridColumn("PhotoFile","PhotoFile",105,true,"left",false,false),
				new FlexigridColumn("LogoFile","LogoFile",105,true,"left",false,false),
				new FlexigridColumn("Brief","Brief",105,true,"left",false,false),
				new FlexigridColumn("Address","Address",105,true,"left",false,false),
				new FlexigridColumn("AgencyTypeID","AgencyTypeID",105,true,"left",false,false),
				new FlexigridColumn("BusinessSectionID","BusinessSectionID",105,true,"left",false,false),
				new FlexigridColumn("CountryID","CountryID",105,true,"left",false,false)
            };
            flexigrid.Buttons = new List<FlexigridButton>() 
            {
                new FlexigridButton("Add","fgButton","Add"),
                new FlexigridButton("Edit","fgButton","Edit"),
                new FlexigridButton("Delete","fgButton","Delete")
            };
            flexigrid.DialogBoxId = "SitesDialog";
            flexigrid.DialogBoxWidth=700;
            flexigrid.DialogBoxHeight=800;
            ViewData["flexigrid"] = flexigrid;
            
			ViewData["AgencyTypeID"] = AgencyTypesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.AgencyTypeID.ToString() }).ToList();
			ViewData["BusinessSectionID"] = BusinessSectionsFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.BusinessSectionID.ToString() }).ToList();
			ViewData["CountryID"] = CountriesFactor.Get().Select(x => new SelectListItem { Text = x.Code, Value = x.CountryID.ToString() }).ToList();
            //ViewData["Categories"] = CategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.CategoryID.ToString() }).ToList();
            return View(new SitesModel());

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
                List<SitesModel> SitesList = SitesFactor.GetPageByPage(page, pageSize, sortname, sortorder, qtype, query, out totalRecords);
                if (SitesList.Count > 0)
                {
                    return CreateFlexiJson(SitesList, page, totalRecords);
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
                SitesModel SitesObj = SitesFactor.GetObject(id);
                if (SitesObj == null)
                {
                    SitesObj = new SitesModel();
                }
                return Json(SitesObj, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(SitesModel SitesObj)
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
                bool result = SitesFactor.Create(SitesObj);
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
        public ActionResult Update(SitesModel SitesObj)
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
                bool result = SitesFactor.Update(SitesObj);
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
                SitesFactor.DeleteGroupofObjects(ids);
                return List(1,25, null, null, null, null);
            }
           catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        #region -----------------CreateFlexiJson-----------------
        //***********************************************************************************************
        private JsonResult CreateFlexiJson(IEnumerable<SitesModel> items, int page, int total)
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
                                    id = x.SiteID,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { 
                                        														x.Name,
														x.Url,
														x.PhotoFile,
														x.LogoFile,
														x.Brief,
														x.Address,
														x.AgencyTypeID,
														x.BusinessSectionID,
														x.CountryID
                                         }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }
        //***********************************************************************************************
        #endregion
        
    }
}
