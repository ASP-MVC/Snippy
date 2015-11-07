namespace Snippy.Data.UnitOfWork
{
    using Snippy.Data.Repositories;
    using Snippy.Models;

    public interface ISnippyData
    {
        IRepository<User> Users { get; }

        IRepository<Label> Labels { get; }

        IRepository<ProgrammingLanguage> ProgrammingLanguages { get; }

        IRepository<Snippet> Snippets { get; }

        IRepository<Comment> Comments { get; }

        int SaveChanges();
    }
}