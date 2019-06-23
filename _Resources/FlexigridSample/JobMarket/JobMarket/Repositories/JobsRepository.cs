using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using JobMarket.Models;

namespace JobMarket.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly string filePath;

        public JobsRepository()
        {
            this.filePath = HttpContext.Current.Server.MapPath("~/App_Data/job-posts.xml");
        }

        public void Save(JobPost jobPost)         
        {             
            if (jobPost == null)
            {
                throw new ArgumentNullException("jobPost");
            }

            if (!File.Exists(filePath))
            {
                return;
            }

            var existingJobs = XElement.Load(filePath);

            XElement existingJob = null;
            Guid id;

            if (!jobPost.Id.Equals(Guid.Empty))
            {
                id = jobPost.Id;

                existingJob = existingJobs.Elements(JobPost.JOB).SingleOrDefault(x => (Guid) x.Element(JobPost.ID) == id);
            }
            else
            {
                id = Guid.NewGuid();
            }

            var newJob = new XElement(JobPost.JOB, new XElement(JobPost.ID, id),
                                           new XElement(JobPost.ROLE, jobPost.Role),
                                           new XElement(JobPost.DESCRIPTION, jobPost.Description),
                                           new XElement(JobPost.LOCATION, jobPost.Location),
                                           new XElement(JobPost.JOBTYPE, jobPost.JobType),
                                           new XElement(JobPost.PAYMENTRATE, jobPost.PaymentRate),
                                           new XElement(JobPost.CONTACTNAME, jobPost.ContactName),
                                           new XElement(JobPost.CONTACTPHONE, jobPost.ContactPhone),
                                           new XElement(JobPost.MODIFIEDDATE, DateTime.Now)
                );

            if (existingJob != null)
            {
               existingJob.ReplaceWith(newJob);
               existingJobs.Save(filePath);
            }
            else
            {
                var jobs = new XElement(JobPost.ROOT);
                jobs.Add(existingJobs.Elements(JobPost.JOB));
                jobs.Add(newJob);
                jobs.Save(filePath);
            }
        }

        public JobPost GetObject(Guid jobId)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var existingJobs = XElement.Load(filePath);

            XElement existingJob = null;
            Guid id;

            id = jobId;

            existingJob = existingJobs.Elements(JobPost.JOB).SingleOrDefault(x => (Guid)x.Element(JobPost.ID) == id);

            JobPost job = new JobPost();
            if (existingJob != null)
            {
                job.Id = (Guid)existingJob.Element(JobPost.ID);
                job.Role = (string)existingJob.Element(JobPost.ROLE);
                job.Description = (string)existingJob.Element(JobPost.DESCRIPTION);
                job.JobType = (string)existingJob.Element(JobPost.JOBTYPE);
                job.Location = (string)existingJob.Element(JobPost.LOCATION);
                job.PaymentRate = (decimal)existingJob.Element(JobPost.PAYMENTRATE);
                job.ContactName = (string)existingJob.Element(JobPost.CONTACTNAME);
                job.ContactPhone = (string)existingJob.Element(JobPost.CONTACTPHONE);
                job.ModifiedDate = (DateTime)existingJob.Element(JobPost.MODIFIEDDATE);

            }
            return job;
        }

        public IEnumerable<JobPost> GetAll()
        {
            if (!File.Exists(filePath))
            {
                return new List<JobPost>();
            }

            var existingJobs = XElement.Load(filePath);
            var jobs = from jobElement in existingJobs.Elements(JobPost.JOB)
                        select new JobPost()
                                   {
                                       Id = (Guid)jobElement.Element(JobPost.ID),
                                       Role = (string) jobElement.Element(JobPost.ROLE),
                                       Description = (string) jobElement.Element(JobPost.DESCRIPTION),
                                       JobType = (string) jobElement.Element(JobPost.JOBTYPE),
                                       Location = (string) jobElement.Element(JobPost.LOCATION),
                                       PaymentRate = (decimal) jobElement.Element(JobPost.PAYMENTRATE),
                                       ContactName = (string) jobElement.Element(JobPost.CONTACTNAME),
                                       ContactPhone = (string) jobElement.Element(JobPost.CONTACTPHONE),
                                       ModifiedDate = (DateTime) jobElement.Element(JobPost.MODIFIEDDATE)
                                   };
            return jobs;
        }

        public void Delete(Guid id)         
        {             
            if (id == Guid.Empty || !File.Exists(filePath))
            {
                return;
            }
                            
            var jobs = XElement.Load(filePath);                  
                             
            var existingJob = jobs.Elements(JobPost.JOB).SingleOrDefault(x => (Guid)x.Element(JobPost.ID) == id);

            if (existingJob == null)
            {
                return;
            }

            existingJob.Remove();
            jobs.Save(filePath);
        }

        /*private void InitJobsStore()
        {
            var locations = new[] { "Dublin", "Dublin 2", "Cork", "Athlone", "Galway" };
            var jobTypes = new[] { "Contract", "Permanent" };
            var payments = new[] { 450m, 55.000m, 35.000m, 80.000m, 400m };
            var contactNames = new[] { "Sergey Kosik", "Tim Dunne", "John Kelleher", "Sandra Byrne", "Fergal O'Connel" };
            var contactPhones = new[] { "014342343", "014365789", "234658973", "019872345", "34569870" };
            var roles = new[] { "Deployment Engineer", "C#.Net Developer", "Test analyst", "Project Manager", "Senior IT Administrator" };
            var descriptions = new[] {
                "My client is looking for three Deployment Engineers that will working within the infrastructure and cloud solutions department. You'll be providing operational support and deploying open source configuration management tools such as Puppet - also exposure with Apache Hadoop which is the open source software framework.",
                "RealTime is seeking an experienced C#.Net Developer to work as part of a successful team in the design, building and supporting Business Applications in a dynamic and high volume environment.",
                "We are currently recruiting for a Test Analyst to join a project based team for a top company based in the City Centre. This is a great opportunity to build on your experience and be a integral part of the successful completion of the project.",
                "Thanks to the success experienced by our client's one of the world leaders in the gift experience market, which has seen them grow rapidly year on year since their set up in 2003 to over 800 staff worldwide. Our clients operate a success-oriented work environment fuelled by the talent, skills, and expertise of their employees who are the best in the industry. Employees are encouraged to develop new ideas, new approaches, and new abilities. The entrepreneurial atmosphere is part of what makes our client a refreshing, exciting and motivating place to work.",
                "The position of Senior IT Administrator offers a great opportunity for someone looking to develop their technical and personal skills in a customer focused area. The position would suit someone who has a solid technical administration background and experience of providing IT support in a multiple customer environment."
            };

            this.jobsStore = new Dictionary<Guid, JobPost>();

            int run = 1;
            for (int i = 0; i < 5; i++)
            {
                var jobPost = new JobPost
                {
                    Id = Guid.NewGuid(),
                    Role = roles[i % 5] + " " + run,
                    Description = descriptions[i % 5],
                    JobType = jobTypes[i % 2],
                    PaymentRate = payments[i % 5],
                    Location = locations[i % 5],
                    ContactName = contactNames[i % 5],
                    ContactPhone = contactPhones[i % 5],
                    ModifiedDate = DateTime.Now
                };

                this.jobsStore.Add(jobPost.Id, jobPost);
            }
        }*/
    }
}