using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of ItemsModel.
/// </summary>
namespace News.Models
{
    [Table("Items")]
    public class ItemsModel
    {

		[Key]
		[Required]
		[Display(Name = "ItemID")]
		public int ItemID { get; set; }

		[Required]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "CategoryID")]
		public int CategoryID { get; set; }

		[DataType(DataType.Upload)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "PhotoFile")]
		public string PhotoFile { get; set; }

		[DataType(DataType.Url)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Url")]
		public string Url { get; set; }

		[Required]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Type")]
		public int Type { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "IsMain")]
		public bool IsMain { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "YoutubeCode")]
		public string YoutubeCode { get; set; }

		[Display(Name = "Title")]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[DataType(DataType.DateTime)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "DateInserted")]
		public DateTime DateInserted { get; set; }

    }
}
