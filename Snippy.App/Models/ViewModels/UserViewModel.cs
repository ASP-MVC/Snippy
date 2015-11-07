namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public IEnumerable<SnippetViewModel> Snippets { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}