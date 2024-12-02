using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Insira um endereço de e-mail válido.")]
        [Display(Name="E-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name="Senha")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "'Confirmação de Senha' e 'Senha' não coincidem.")]
        [Display(Name="Confirmação de Senha")]
        public string? ConfirmPassword { get; set; }
    }
}
