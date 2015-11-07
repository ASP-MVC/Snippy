namespace Snippy.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Snippy.Models;

    public class SnippyDbContext : IdentityDbContext<User>, ISnippyDbContext
    {
        public SnippyDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Label> Labels { get; set; }

        public IDbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public IDbSet<Snippet> Snippets { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static SnippyDbContext Create()
        {
            return new SnippyDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Snippets)
                .WithRequired(x => x.Author)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}