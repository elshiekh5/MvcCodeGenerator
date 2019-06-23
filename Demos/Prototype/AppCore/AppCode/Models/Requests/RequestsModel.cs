using System;

//using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of RequestsModel.
/// </summary>
namespace AppCore
{
    //[Table("Requests")]
    public class RequestsModel
    {

		public int RequestID { get; set; }

		public DateTime RequestTime { get; set; }

    }
}
