namespace Snippy.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Snippy.App.Models.ViewModels;
    using Snippy.Data.UnitOfWork;

    public class HomeController : BaseController
    {
        private const int TopFive = 5;

        public HomeController(ISnippyData data)
            : base(data)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var topFiveSnippetsByAddion =
                this.Data.Snippets.All()
                    .OrderByDescending(s => s.CreatedAt)
                    .Take(TopFive)
                    .ProjectTo<SnippetViewModel>()
                    .ToList();

            var topFiveCommentsByDate =
                this.Data.Comments.All()
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(TopFive)
                    .ProjectTo<CommentViewModel>()
                    .ToList();
            var topFiveLabelsBySnippetsCount =
                this.Data.Labels.All()
                    .OrderByDescending(l => l.Snippets.Count)
                    .Take(TopFive)
                    .ProjectTo<LabelViewModel>()
                    .ToList();
            var homeModel = new HomeViewModel
            {
                Snippets = topFiveSnippetsByAddion,
                Commnets = topFiveCommentsByDate,
                Labels = topFiveLabelsBySnippetsCount
            };
            return this.View(homeModel);
        }
    }
}