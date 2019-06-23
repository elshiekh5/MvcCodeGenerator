using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of StatusModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("Status")]
    public class StatusModel
    {

		[Required]
		[Display(Name = "StatusID")]
		public int StatusID { get; set; }

		[Required]
		[Display(Name = "Title")]
		public string Title { get; set; }

    }
}
