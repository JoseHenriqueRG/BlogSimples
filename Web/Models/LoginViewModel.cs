using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O login é obrigatório.")]
        [Display(Name = "Login")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [Display(Name = "Lembrar Senha")]
        public bool RememberMe { get; set; }
    }
}
