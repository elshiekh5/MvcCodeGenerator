using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of IndexersWithJournalsModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("IndexersWithJournals")]
    public class IndexersWithJournalsModel
    {

		[Display(Name = "ID")]
		public int? ID { get; set; }

		[Required]
		[Display(Name = "IndexerID")]
		public int IndexerID { get; set; }

		[Required]
		[Display(Name = "JournalCode")]
		public string JournalCode { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "SubmissionDate")]
		public DateTime SubmissionDate { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "AcceptanceDate")]
		public DateTime AcceptanceDate { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "RejectionDate")]
		public DateTime RejectionDate { get; set; }

		[Display(Name = "NextEvaRound")]
		public string NextEvaRound { get; set; }

		[Display(Name = "NoofEvaRound")]
		public int NoofEvaRound { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Notes")]
		public string Notes { get; set; }

		[Required]
		[Display(Name = "StatusID")]
		public int StatusID { get; set; }

    }
}
