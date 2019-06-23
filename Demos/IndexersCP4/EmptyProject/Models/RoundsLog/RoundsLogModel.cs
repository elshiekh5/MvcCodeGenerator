using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of RoundsLogModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("RoundsLog")]
    public class RoundsLogModel
    {

		[Display(Name = "RoundID")]
		public int? RoundID { get; set; }

		[Required]
		[Display(Name = "IndexerID")]
		public int IndexerID { get; set; }

		[Required]
		[Display(Name = "JournalCode")]
		public string JournalCode { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "RoundDate")]
		public DateTime RoundDate { get; set; }

		[Required]
		[Display(Name = "StatusID")]
		public int StatusID { get; set; }

		[Display(Name = "NextEvaRound")]
		public string NextEvaRound { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Notes")]
		public string Notes { get; set; }

    }
}
