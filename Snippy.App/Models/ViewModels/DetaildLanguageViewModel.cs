namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class DetaildLanguageViewModel : IMapFrom<ProgrammingLanguage>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SnippetViewModel> Snippets { get; set; }
    }
}