 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employer.WEBAPI.Models
{
    [Table("Employer")]
    public class EmployerDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       

        [StringLength(50), Required]
        public string Organization { get; set; }
        [StringLength(50), Required]
        public string OrganizationType { get; set; }
        [DataType(DataType.EmailAddress), Required]
        public string CompanyEmail { get; set; }
        [Required]

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string CompanyPhone { get; set; }
        [Required]
        public int NoOfEmployees { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartYear { get; set; }
        [StringLength(200), Required]
        public string About { get; set; }
        [DataType(DataType.EmailAddress), Required]
        public string CreatedBy { get; set; }
    }
}
