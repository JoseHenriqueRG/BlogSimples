using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Identities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
