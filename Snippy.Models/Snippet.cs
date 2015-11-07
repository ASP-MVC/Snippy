namespace Snippy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Snippet
    {
        private ICollection<Label> labels;

        private ICollection<Comment> comments;

        public Snippet()
        {
            this.labels = new HashSet<Label>();
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        public int ProgrammingLanguageId { get; set; }

        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Label> Labels
        {
            get
            {
                return this.labels;
            }
            set
            {
                this.labels = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }
    }
}