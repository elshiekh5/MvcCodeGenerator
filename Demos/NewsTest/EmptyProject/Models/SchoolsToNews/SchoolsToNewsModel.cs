using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of SchoolsToNewsModel.
/// </summary>
namespace Test.Models
{
    //[Table("SchoolsToNews")]
    public class SchoolsToNewsModel
    {

		[Required]
		[Display(Name = "ShoolsToNewsID")]
		public int ShoolsToNewsID { get; set; }

		[Required]
		[Display(Name = "SchoolID")]
		public int SchoolID { get; set; }

		[Required]
		[Display(Name = "NewsID")]
		public int NewsID { get; set; }

    }
}
