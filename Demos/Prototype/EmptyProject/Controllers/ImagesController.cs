using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomModels.FlexigridModels;
using System.IO;
using AppCore;


namespace PrototypesPics
{
    public class ImagesController : Controller
    {

        int pageSize = 25;

        #region -----------------Index-----------------
        //***********************************************************************************************
        //
        // GET: /Images/
        public ActionResult Index()
        {
              
            FlexigridViewModel flexigrid = new FlexigridViewModel("ImagesList","ImagesForm","ImagesPost", "/Images/List", "json", "Name", "asc", true, "Images Listing", true, pageSize, true, 950, 2000);
            flexigrid.Columns = new List<FlexigridColumn>() 
            {
				new FlexigridColumn("CategoryID","CategoryID",86,true,"left",false,false),
				new FlexigridColumn("Name","Name",86,true,"left",false,false),
				new FlexigridColumn("Path","Path",86,true,"left",false,false),
				new FlexigridColumn("ImageWidth","ImageWidth",86,true,"left",false,false),
				new FlexigridColumn("ImageHeight","ImageHeight",86,true,"left",false,false),
				new FlexigridColumn("ImageSize","ImageSize",86,true,"left",false,false),
				new FlexigridColumn("ImageExtension","ImageExtension",86,true,"left",false,false),
				new FlexigridColumn("Data","Data",86,true,"left",false,false),
				new FlexigridColumn("AppearingCount","AppearingCount",86,true,"left",false,false),
				new FlexigridColumn("Identifire","Identifire",86,true,"left",false,false),
				new FlexigridColumn("BlocKeys","BlocKeys",86,true,"left",false,false)
            };
            flexigrid.Buttons = new List<FlexigridButton>() 
            {
                new FlexigridButton("Add","fgButton","Add"),
                new FlexigridButton("Edit","fgButton","Edit"),
                new FlexigridButton("Delete","fgButton","Delete")
            };
            flexigrid.DialogBoxId = "ImagesDialog";
            flexigrid.DialogBoxWidth=700;
            flexigrid.DialogBoxHeight=800;
            ViewData["flexigrid"] = flexigrid;
            
			ViewData["CategoryID"] = ImagesCategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Identifire, Value = x.CategoryID.ToString() }).ToList();
            //ViewData["Categories"] = CategoriesFactor.Get().Select(x => new SelectListItem { Text = x.Title, Value = x.CategoryID.ToString() }).ToList();
            return View(new ImagesModel());

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
                List<ImagesModel> ImagesList = ImagesFactor.GetPageByPage(page, pageSize, sortname, sortorder, qtype, query, out totalRecords);
                if (ImagesList.Count > 0)
                {
                    return CreateFlexiJson(ImagesList, page, totalRecords);
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
                ImagesModel ImagesObj = ImagesFactor.GetObject(id);
                if (ImagesObj == null)
                {
                    ImagesObj = new ImagesModel();
                }
                return Json(ImagesObj, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(ImagesModel ImagesObj)
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
                bool result = ImagesFactor.Create(ImagesObj);
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
        public ActionResult Update(ImagesModel ImagesObj)
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
                bool result = ImagesFactor.Update(ImagesObj);
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
                ImagesFactor.DeleteGroupofObjects(ids);
                return List(1,25, null, null, null, null);
            }
           catch (Exception ex)
            { return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message); }
            
        }
        //***********************************************************************************************
        #endregion

        public ActionResult Image(int imageId, int width, int height)
        {



            ImagesModel image = ImagesFactor.GetObject(imageId);
            MemoryStream ms = new MemoryStream(image.Data);
            byte[] imageContent = ThumbnailsManager.CreateThumb(ms, width, height, ThumbnailsManager.Quality);
            ////Image img = Image.FromStream(ms);
            //// return byte array to caller with image type
            //Random rnd = new Random();
            //int month = rnd.Next(1, 1000000);
            //Response.ContentType = "image/jpeg";
            //Response.AddHeader("Content-disposition", "attachment; filename=" + month + ".jpg");
            //Response.BinaryWrite(imageContent);



            return File(imageContent, "image/jpg");
        }

        #region -----------------CreateFlexiJson-----------------
        //***********************************************************************************************
        private JsonResult CreateFlexiJson(IEnumerable<ImagesModel> items, int page, int total)
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
                                    id = x.ImageID,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { 
                                        														x.CategoryID,
														x.Name,
														x.Path,
														x.ImageWidth,
														x.ImageHeight,
														x.ImageSize,
														x.ImageExtension,
														"<img src=\"/images/Image?imageId="+x.ImageID+"&width=100&height=100\" />",
														x.AppearingCount,
														x.Identifire,
														x.BlocKeys
                                         }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }
        //***********************************************************************************************
        #endregion
        
    }
}
