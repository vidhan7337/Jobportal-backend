using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSeeker.WebAPI.Models
{
    [Table("Qualifications")]
    public class Qualifications
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [StringLength(50), Required]
        public string QualificationName { get; set; }
        [StringLength(50), Required]
        public string University { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime YearOfCompletion { get; set; }
        [StringLength(10), Required]
        public string GradeOrScore { get; set; }

    }
}
