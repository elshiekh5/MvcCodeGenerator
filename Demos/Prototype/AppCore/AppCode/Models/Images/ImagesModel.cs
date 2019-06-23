using System;

//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of ImagesModel.
/// </summary>
namespace AppCore
{
    //[Table("Images")]
    public class ImagesModel
    {

		public int? ImageID { get; set; }

		public int CategoryID { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public int ImageWidth { get; set; }

		public int ImageHeight { get; set; }

		public long ImageSize { get; set; }

		public string ImageExtension { get; set; }

		public byte[] Data { get; set; }

		public int AppearingCount { get; set; }

		public string Identifire { get; set; }

		public string BlocKeys { get; set; }

    }
}
