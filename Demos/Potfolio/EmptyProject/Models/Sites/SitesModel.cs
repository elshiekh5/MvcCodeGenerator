using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of SitesModel.
/// </summary>
namespace Portfolio.Models
{
    //[Table("Sites")]
    public class SitesModel
    {

		[Display(Name = "SiteID")]
		public int? SiteID { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[DataType(DataType.Url)]
		[Display(Name = "Url")]
		public string Url { get; set; }

		//[DataType(DataType.Upload)]
		[Display(Name = "PhotoFile")]
		public string PhotoFile { get; set; }

		//[DataType(DataType.Upload)]
		[Display(Name = "LogoFile")]
		public string LogoFile { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Brief")]
		public string Brief { get; set; }

		[Display(Name = "Address")]
		public string Address { get; set; }

		[Required]
		[Display(Name = "AgencyTypeID")]
		public int AgencyTypeID { get; set; }

		[Required]
		[Display(Name = "BusinessSectionID")]
		public int BusinessSectionID { get; set; }

		[Required]
		[Display(Name = "CountryID")]
		public int CountryID { get; set; }

    }
}
