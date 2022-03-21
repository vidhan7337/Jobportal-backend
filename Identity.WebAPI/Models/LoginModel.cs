using System.ComponentModel.DataAnnotations;

namespace Identity.WebAPI.Models
{
    public class LoginModel
    {
        [StringLength(50), Required]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required, StringLength(50)]
        public string Password { get; set; }
    }
}


