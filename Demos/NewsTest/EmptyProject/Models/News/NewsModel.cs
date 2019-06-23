using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of NewsModel.
/// </summary>
namespace Test.Models
{
    //[Table("News")]
    public class NewsModel
    {

		[Display(Name = "NewsID")]
		public int? NewsID { get; set; }

		[Required]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "LangID")]
		public int LangID { get; set; }

		[Required]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Type")]
		public int Type { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Details")]
		public string Details { get; set; }

		//[DataType(DataType.Upload)]
		[Display(Name = "PhotoFile")]
		public string PhotoFile { get; set; }

		[Display(Name = "PhotoName")]
		public string PhotoName { get; set; }

		//[DataType(DataType.Upload)]
		[Display(Name = "AttachFile")]
		public string AttachFile { get; set; }

		[Display(Name = "AttachName")]
		public string AttachName { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "EndDate")]
		public DateTime EndDate { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "InsertDate")]
		public DateTime InsertDate { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "LastModfiedDate")]
		public DateTime LastModfiedDate { get; set; }

    }
}
