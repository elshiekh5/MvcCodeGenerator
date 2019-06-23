using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Models;
using Testing.Business.Factors;
using News.Models;
using News.Business.Factors;
using TestingWebsite.Models;

namespace TestingWebsite.Controllers
{
    public class NewsController : Controller
    {

        #region -----------------Index-----------------
        //----------------------------------------------------------------------------------------------
        //
        // GET: /News/
        public ActionResult Index()
        {
              
            FlexigridViewModel flexigrid = new FlexigridViewModel("newsList", "/News/List", "json", "Title", "asc", true, "News Listing", true, 15, true, 750, 500);
            flexigrid.Columns = new List<FlexigridColumn>() 
            {
                new FlexigridColumn("Title","Title",180,true,"left",true,true),
             new FlexigridColumn("Description","Description",180,true,"left",false,false),
             new FlexigridColumn("Type","Type",130,true,"left",false,false),
             new FlexigridColumn("DateInserted","DateInserted",120,true,"left",true,false) 
            };
            flexigrid.Buttons = new List<FlexigridButton>() 
            {
               new FlexigridButton("Add","add","Add"),
             new FlexigridButton("Edit","edit","Edit"),
             new FlexigridButton("Delete","delete","Delete")
            };
            flexigrid.DialogBoxId = "NewsDialog";
            flexigrid.DialogBoxWidth=700;
            flexigrid.DialogBoxHeight=800;
            ViewData["flexigrid"] = flexigrid;
            ViewData["Categories"] = CategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.CategoryID.ToString() }).ToList();
            return View(new ItemsModel());

        }
        //----------------------------------------------------------------------------------------------
        #endregion

        #region -----------------Page-----------------
        //----------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Page(int id)
        {
            try
            {
                int pageSize = 15; int totalRecords = 0;
                List<ItemsModel> itemsList = ItemsFactor.GetPageByPage(id, pageSize, null, null, null, null, out totalRecords);
                if (itemsList.Count > 0)
                {
                    return CreateFlexiJson(itemsList, id, totalRecords);
                }
                else
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error");
                }
            }
            catch (Exception ex)
            {

            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error");
           
        }
        //----------------------------------------------------------------------------------------------
        #endregion
        
        #region -----------------List-----------------
        //----------------------------------------------------------------------------------------------
        public ActionResult List(int page, int rp, string sortname, string sortorder, string qtype, string query)
        {
            try
            {
                int pageSize = rp; int totalRecords = 0;
                List<ItemsModel> itemsList = ItemsFactor.GetPageByPage(page, pageSize, sortname, sortorder, qtype, query, out totalRecords);
                if (itemsList.Count > 0)
                {
                    return CreateFlexiJson(itemsList, page, totalRecords);
                }
                else
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error");
                }
            }
            catch (Exception ex)
            {

            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error");
            
        }
        //----------------------------------------------------------------------------------------------
        #endregion

        #region -----------------GetObject-----------------
        //----------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult GetObject(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemsModel itemsObj = ItemsFactor.GetObject(id);
                    if (itemsObj != null)
                    {
                        return Json(itemsObj, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error");

           
        }
        //----------------------------------------------------------------------------------------------
        #endregion
        
        #region -----------------Save-----------------
        //----------------------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult Save(ItemsModel itemsObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = ItemsFactor.Save(itemsObj);
                    if (result == true)
                    {
                        int pageSize = 15; int totalRecords = 0; int page = 1;
                        List<ItemsModel> itemsList = ItemsFactor.GetPageByPage(page, pageSize, null, null, null, null, out totalRecords);
                        return CreateFlexiJson(itemsList, page, totalRecords);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Saving operation faild");
            
        }
        //----------------------------------------------------------------------------------------------
        #endregion

        #region -----------------Delete-----------------
        //----------------------------------------------------------------------------------------------
        public ActionResult Delete(int[] ids)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemsFactor.DeleteGroupofobjects(ids);
                    //--------------------------------------
                    {
                        int pageSize = 15; int totalRecords = 0; int page = 1;
                        List<ItemsModel> itemsList = ItemsFactor.GetPageByPage(page, pageSize, null, null, null, null, out totalRecords);
                        return CreateFlexiJson(itemsList, page, totalRecords);
                    }
                    //--------------------------------------

                }
            }
            catch (Exception ex)
            {

            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error");
            
        }
        //----------------------------------------------------------------------------------------------
        #endregion

        #region -----------------CreateFlexiJson-----------------
        //----------------------------------------------------------------------------------------------
        private JsonResult CreateFlexiJson(IEnumerable<ItemsModel> items, int page, int total)
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
                                    id = x.ItemID,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { x.Title, x.Description, x.Type, x.DateInserted.ToShortDateString() }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------------
        #endregion
        
    }
}
