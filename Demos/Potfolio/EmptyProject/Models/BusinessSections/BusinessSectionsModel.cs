using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of BusinessSectionsModel.
/// </summary>
namespace Portfolio.Models
{
    //[Table("BusinessSections")]
    public class BusinessSectionsModel
    {

		[Display(Name = "BusinessSectionID")]
		public int? BusinessSectionID { get; set; }

		[Display(Name = "Title")]
		public string Title { get; set; }

    }
}
