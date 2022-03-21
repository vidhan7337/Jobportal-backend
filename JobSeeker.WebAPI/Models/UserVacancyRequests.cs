using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSeeker.WebAPI.Models
{
    [Table("UserVacancyRequests")]
    public class UserVacancyRequests
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int VacancyId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime AppliedDate { get; set; }
    }
}
