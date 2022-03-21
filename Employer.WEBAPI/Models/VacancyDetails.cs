using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employer.WEBAPI.Models
{
    [Table("VacancyDetails")]
    public class VacancyDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string PublishedBy { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }
        [Required]
        public int NoOfVacancies { get; set; }
        [StringLength(50), Required]
        public string MinimumQualification { get; set; }
        [StringLength(200), Required]
        public string JobDescription { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime LastDate { get; set; }

        [Required]
        public int ExperienceRequired { get; set; }
        [Required]
        public decimal MinSalary { get; set; }
        [Required]
        public decimal MaxSalary { get; set; }




    }
}
