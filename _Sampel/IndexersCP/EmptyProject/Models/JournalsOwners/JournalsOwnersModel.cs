using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of JournalsOwnersModel.
/// </summary>
namespace IndexersCP4.Models
{
    //[Table("JournalsOwners")]
    public class JournalsOwnersModel
    {

		[Display(Name = "JournalOwner")]
		public int? JournalOwner { get; set; }

		[Display(Name = "OwnerName")]
		public string OwnerName { get; set; }

    }
}
