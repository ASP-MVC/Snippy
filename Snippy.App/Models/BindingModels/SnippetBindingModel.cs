namespace Snippy.App.Models.BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Snippy.Infrastructure.Mappings;
    using Snippy.Models;

    public class SnippetBindingModel : IMapTo<Snippet>, IMapFrom<Snippet>
    {
        public int Id { get; set; }

        [Required]
        [UIHint("SingleLineTextField")]
        public string Title { get; set; }

        [Required]
        [UIHint("MultiLineTextField")]
        public string Description { get; set; }

        [Required]
        [UIHint("MultiLineTextField")]
        public string Code { get; set; }

        [DisplayName("Language")]
        [UIHint("DropDownList")]
        public int ProgrammingLanguageId { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        [DisplayName("Labels")]
        [UIHint("SingleLineTextField")]
        public string LabelsAsText { get; set; }
    }
}