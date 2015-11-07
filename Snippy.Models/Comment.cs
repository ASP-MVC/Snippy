namespace Snippy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public int SnippetId { get; set; }

        [Required]
        public virtual Snippet Snippet { get; set; }
    }
}