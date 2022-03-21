using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSeeker.WebAPI.Models
{
    [Table("UserExperiences")]
    public class UserExperiences
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [StringLength(50), Required]
        public string CompanyName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartYear { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndYear { get; set; }
        [StringLength(100), Required]
        public string ComapanyUrl { get; set; }
        [StringLength(50), Required]
        public string Designation { get; set; }
        [StringLength(50), Required]
        public string JobDescription { get; set; }
    }
}
