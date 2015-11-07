namespace Snippy.App.Models.ViewModels
{
    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class ProgrammingLanguageViewModel : IMapFrom<ProgrammingLanguage>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}