namespace Snippy.App.Models.ViewModels
{
    using System;

    using AutoMapper;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public int SnippetId { get; set; }

        public string SnippetTitle { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.AuthorUsername, opt => opt.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.SnippetTitle, opt => opt.MapFrom(x => x.Snippet.Title))
                .ReverseMap();
        }
    }
}