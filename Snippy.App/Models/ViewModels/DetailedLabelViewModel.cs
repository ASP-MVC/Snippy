namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class DetailedLabelViewModel : IMapFrom<Label>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public IEnumerable<SnippetViewModel> Snippets { get; set; } 
    }
}