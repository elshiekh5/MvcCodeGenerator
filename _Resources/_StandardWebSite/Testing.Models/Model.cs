using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// The model class of ItemsModel.
/// </summary>
namespace Testing.Models
{
    [Table("News")]
    public class NewsModel
    {

        [Key]
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Title")]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "Brief")]
       // [Required(ErrorMessage = "Please Enter Short Desciption")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Brief { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [Editable(false, AllowInitialValue = true)]
        public DateTime DateAdded { get; set; }


        [Display(Name = "PhotoFile")]
        [DataType(DataType.Upload)]
        public string PhotoFile { get; set; }

        [Display(Name = "AttatchFile")]
        [DataType(DataType.Upload)]
        public string AttatchFile { get; set; }

        [Display(Name = "SourceUrl")]
        [DataType(DataType.Url)]
        public string SourceUrl { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public string Price { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "PostalCode")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }


        //[DataType(DataType.Currency)]
        //[DataType(DataType.DateTime)]
        //[DataType(DataType.EmailAddress)]
        //[DataType(DataType.ImageUrl)]
        //[DataType(DataType.MultilineText)]
        //[DataType(DataType.Password)]
        //[DataType(DataType.PhoneNumber)]
        //[DataType(DataType.PostalCode)]
        //[DataType(DataType.Text)]
        //[DataType(DataType.Time)]
        //[DataType(DataType.Upload)]
        //[DataType(DataType.Url)]


    }
}
