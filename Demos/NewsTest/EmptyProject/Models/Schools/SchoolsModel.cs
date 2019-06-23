using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of SchoolsModel.
/// </summary>
namespace Test.Models
{
    //[Table("Schools")]
    public class SchoolsModel
    {

		[Required]
		[Display(Name = "SchoolID")]
		public int SchoolID { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Name")]
		public string Name { get; set; }

    }
}
