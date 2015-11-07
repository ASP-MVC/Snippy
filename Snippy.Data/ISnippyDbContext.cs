namespace Snippy.Data
{
    using System.Data.Entity;

    using Snippy.Models;

    public interface ISnippyDbContext
    {
        IDbSet<User> Users { get; }

        IDbSet<Label> Labels { get; }

        IDbSet<ProgrammingLanguage> ProgrammingLanguages { get; }

        IDbSet<Snippet> Snippets { get; }

        IDbSet<Comment> Comments { get; }

        int SaveChanges();
    }
}