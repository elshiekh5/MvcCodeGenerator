using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobMarket.Models
{
    public class JobPost
    {
        // data store fields
        public const string ROOT = "Jobs"; 
        public const string JOB = "Job"; 
        public const string ID = "Id"; 
        public const string ROLE = "Role"; 
        public const string LOCATION = "Location"; 
        public const string JOBTYPE = "JobType"; 
        public const string PAYMENTRATE = "PaymentRate"; 
        public const string DESCRIPTION = "Description"; 
        public const string CONTACTNAME = "ContactName";
        public const string CONTACTPHONE = "ContactPhone";
        public const string MODIFIEDDATE = "ModifiedDate";
        
        public Guid Id { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string JobType { get; set; }

        public decimal PaymentRate { get; set; }

        [Required]
        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}