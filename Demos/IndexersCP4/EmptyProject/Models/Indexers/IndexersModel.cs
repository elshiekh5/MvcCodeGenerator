using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of IndexersModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("Indexers")]
    public class IndexersModel
    {

		[Display(Name = "IndexerID")]
		public int? IndexerID { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "OwnerID")]
		public int OwnerID { get; set; }

    }
}
