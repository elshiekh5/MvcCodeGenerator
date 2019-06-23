using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of LanguagesModel.
/// </summary>
namespace Test.Models
{
    //[Table("Languages")]
    public class LanguagesModel
    {

		[Required]
		[Display(Name = "LangID")]
		public int LangID { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Code")]
		public string Code { get; set; }

		[Display(Name = "LocalizationCode")]
		public string LocalizationCode { get; set; }

    }
}
