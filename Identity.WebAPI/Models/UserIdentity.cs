using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.WebAPI.Models
{
    [Table("UserIdentity")]
    public class UserIdentity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string FullName { get; set; }
        [StringLength(50), Required]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required, StringLength(50)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [Required]

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string Phone { get; set; }
        [StringLength(50), Required]
        public string UserType { get; set; }
        

    }
}
