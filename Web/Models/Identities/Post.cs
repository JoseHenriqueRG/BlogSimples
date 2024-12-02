using System.ComponentModel.DataAnnotations;

namespace Web.Models.Identities
{
    public class Post
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200)]
        [Display(Name = "Título")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        [Display(Name = "Conteúdo")]
        public string? Content { get; set; }

        public string? AuthorId { get; set; }

        [Display(Name = "Data")]
        public DateTimeOffset PublishDate { get; set; } = DateTimeOffset.Now;

        [Display(Name = "Autor")]
        public virtual User? Author { get; set; }
    }
}
