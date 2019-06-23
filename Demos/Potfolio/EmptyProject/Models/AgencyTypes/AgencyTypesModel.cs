using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of AgencyTypesModel.
/// </summary>
namespace Portfolio.Models
{
    //[Table("AgencyTypes")]
    public class AgencyTypesModel
    {

		[Display(Name = "AgencyTypeID")]
		public int? AgencyTypeID { get; set; }

		[Display(Name = "Title")]
		public string Title { get; set; }

    }
}
