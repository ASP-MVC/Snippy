namespace Snippy.App.Models.ViewModels
{
    using AutoMapper;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class LabelViewModel : IMapFrom<Label>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int SnippetsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Label, LabelViewModel>()
                .ForMember(x => x.SnippetsCount, opt => opt.MapFrom(x => x.Snippets.Count))
                .ReverseMap();
        }
    }
}