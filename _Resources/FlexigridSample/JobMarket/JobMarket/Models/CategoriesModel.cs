using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of CategoriesModel.
/// </summary>
namespace News.Models
{
    [Table("Categories")]
    public class CategoriesModel
    {

		[Key]
		[Required]
		[Display(Name = "CategoryID")]
		public int CategoryID { get; set; }

		[Display(Name = "Title")]
		public string Title { get; set; }

    }
}
