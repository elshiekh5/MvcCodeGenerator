using System;

//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of ImagesWithBlocksModel.
/// </summary>
namespace AppCore
{
    //[Table("ImagesWithBlocks")]
    public class ImagesWithBlocksModel
    {

		public int? ID { get; set; }

		public int ImageID { get; set; }

		public string BlocKey { get; set; }

    }
}
