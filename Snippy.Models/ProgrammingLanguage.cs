namespace Snippy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProgrammingLanguage
    {
        private ICollection<Snippet> snippets;

        public ProgrammingLanguage()
        {
            this.snippets = new HashSet<Snippet>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets
        {
            get
            {
                return this.snippets;
            }
            set
            {
                this.snippets = value;
            }
        }
    }
}