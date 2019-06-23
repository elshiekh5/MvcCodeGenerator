using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of CountriesModel.
/// </summary>
namespace Portfolio.Models
{
    //[Table("Countries")]
    public class CountriesModel
    {

		[Display(Name = "CountryID")]
		public int? CountryID { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Code")]
		public string Code { get; set; }

		[DataType(DataType.MultilineText)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "EnglishName")]
		public string EnglishName { get; set; }

		[DataType(DataType.MultilineText)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "ArabicName")]
		public string ArabicName { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "TIMEZONE_H")]
		public int TIMEZONE_H { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "TIMEZONE_M")]
		public int TIMEZONE_M { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Reg_ID")]
		public int Reg_ID { get; set; }

		[DataType(DataType.MultilineText)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "SimpleArName")]
		public string SimpleArName { get; set; }

		[DataType(DataType.MultilineText)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "SimpleEnName")]
		public string SimpleEnName { get; set; }

    }
}
