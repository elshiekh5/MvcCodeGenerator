using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobMarket.Models;

namespace JobMarket.Repositories
{
    public interface IJobsRepository
    {
        IEnumerable<JobPost> GetAll();

        //JobPost Get(Guid id);

        void Delete(Guid id);

        void Save(JobPost item);
        JobPost GetObject(Guid jobId);
    }
}
