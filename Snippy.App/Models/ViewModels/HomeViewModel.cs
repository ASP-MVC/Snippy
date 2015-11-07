namespace Snippy.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<SnippetViewModel> Snippets { get; set; } 
        public IEnumerable<CommentViewModel> Commnets { get; set; } 
        public IEnumerable<LabelViewModel> Labels { get; set; } 
    }
}