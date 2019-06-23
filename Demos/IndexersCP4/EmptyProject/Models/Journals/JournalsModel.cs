using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of JournalsModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("Journals")]
    public class JournalsModel
    {

		[Required]
		[Display(Name = "JournalCode")]
		public string JournalCode { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "FullTitle")]
		public string FullTitle { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "ShortTitle")]
		public string ShortTitle { get; set; }

		[Required]
		[Display(Name = "JournalSubCode")]
		public string JournalSubCode { get; set; }

		[Required]
		[Display(Name = "JournalOwner")]
		public int JournalOwner { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Note")]
		public string Note { get; set; }

    }
}
