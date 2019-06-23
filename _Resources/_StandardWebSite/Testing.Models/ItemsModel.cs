using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
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
        [DefaultValue(1)]
        public int CategoryID { get; set; }

		[DataType(DataType.Upload)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "PhotoFile")]
        [DefaultValue("PhotoFile")]
        public string PhotoFile { get; set; }

		[DataType(DataType.Url)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Url")]
        [DefaultValue("http://www.yahoo.com")]
        public string Url { get; set; }

		[Required]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "Type")]
        [DefaultValue(1)]
        public int Type { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "IsMain")]
        [DefaultValue(true)]
        public bool IsMain { get; set; }

		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "YoutubeCode")]
        [DefaultValue("YoutubeCode")]
        public string YoutubeCode { get; set; }

        [Required]
        [Display(Name = "Title")]
        [DefaultValue("Testing data 06-06-2013")]
        public string Title { get; set; }

		[DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [DefaultValue("Description Testing data 06-06-2013")]
        public string Description { get; set; }

		[DataType(DataType.DateTime)]
		[Editable(false, AllowInitialValue = true)]
		[Display(Name = "DateInserted")]
        public DateTime DateInserted { get; set; }


    }
}
