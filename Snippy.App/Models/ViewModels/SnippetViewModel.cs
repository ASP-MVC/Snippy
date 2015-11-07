namespace Snippy.App.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class SnippetViewModel : IMapFrom<Snippet>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public ProgrammingLanguageViewModel ProgrammingLanguage { get; set; }

        public DateTime CreatedAt { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public IEnumerable<LabelViewModel> Labels { get;set; }
        [UIHint("CommentForSnipperViewModel")]
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Snippet, SnippetViewModel>()
                .ForMember(x => x.AuthorUsername, opt => opt.MapFrom(x => x.Author.UserName))
                .ReverseMap();
        }
    }
}