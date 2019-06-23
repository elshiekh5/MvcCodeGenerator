using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of IndexersOwnersModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("IndexersOwners")]
    public class IndexersOwnersModel
    {

		[Display(Name = "OwnerID")]
		public int? OwnerID { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

    }
}
