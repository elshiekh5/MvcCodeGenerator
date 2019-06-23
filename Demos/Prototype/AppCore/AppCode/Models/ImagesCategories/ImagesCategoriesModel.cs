using System;

//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of ImagesCategoriesModel.
/// </summary>
namespace AppCore
{
    //[Table("ImagesCategories")]
    public class ImagesCategoriesModel
    {

		public int? CategoryID { get; set; }

		public string Identifire { get; set; }

		public string NameEn { get; set; }

		public string NameAr { get; set; }

    }
}
