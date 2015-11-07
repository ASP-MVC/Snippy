namespace Snippy.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class CommentBindingModel : IMapTo<Comment>
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        [Required]
        public int SnippetId { get; set; }
    }
}