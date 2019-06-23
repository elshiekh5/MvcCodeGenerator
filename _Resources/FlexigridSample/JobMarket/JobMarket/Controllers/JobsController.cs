using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using JobMarket.Repositories;
using JobMarket.Models;
using JobMarket.Extensions;
using News.Models;
using News.Business.Factors;

namespace JobMarket.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobsRepository jobRepository;

        public JobsController()
            : this(new JobsRepository())
        {
        }

        public JobsController(IJobsRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page, int rp, string sortname, string sortorder, string qtype, string query)
        {
            int pageSize = rp; int totalRecords = 0;
            List<ItemsModel> itemsList = ItemsFactor.GetPageByPage(page, pageSize, out totalRecords);
            return CreateFlexiJson(itemsList, page, totalRecords);

        }

        private JsonResult CreateFlexiJson(IEnumerable<ItemsModel> items, int page, int total)
        {
            int i = 0;
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
                                    id = ++i,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { x.Title, x.Description, x.Title, x.Title }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }

        
        private JsonResult CreateFlexiJson(IEnumerable<JobPost> items, int page, int total)
        {int i = 0;
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
                                    id = ++i,
                                    // either use GetPropertyList(x) method to get all columns 
                                    cell = new object[] { x.Role, x.Description, x.JobType, x.JobType }
                                })
                    }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Save(JobPost jobPost)
        {
            this.jobRepository.Save(jobPost);

            var jobs = this.jobRepository.GetAll().OrderByDescending(x => x.ModifiedDate).AsQueryable();

            return CreateFlexiJson(jobs.ToList(), 1, jobs.Count()); 
        }
        [HttpGet]
        public ActionResult GetObject(Guid id)
        {
            this.jobRepository.GetObject(id);

            JobPost job = this.jobRepository.GetObject(id);

            return Json(job, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult Delete(Guid[] ids)
        {
            foreach (var id in ids)
            {
                this.jobRepository.Delete(id);
            }

            var jobs = this.jobRepository.GetAll().OrderByDescending(x => x.ModifiedDate).AsQueryable();

            return CreateFlexiJson(jobs.ToList(), 1, jobs.Count());
        }

        

        /*------------------------------------------------------------------------------------
         private List<string> GetPropertyList(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            return properties.Select(property => property.GetValue(obj, null)).Select(o => o == null ? "" : o.ToString()).ToList();
        }
         */

        public ActionResult View1()
        { return View(); }
    }
}
