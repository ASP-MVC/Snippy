namespace Snippy.App.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using Snippy.App.Models.ViewModels;
    using Snippy.Data.UnitOfWork;

    public class LanguagesController : BaseController
    {
        public LanguagesController(ISnippyData data)
            : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var language = this.Data.ProgrammingLanguages.Find(id);
            if (language == null)
            {
                return this.HttpNotFound("Language no longer exists");
            }

            var viewModel = Mapper.Map<DetaildLanguageViewModel>(language);
            return this.View(viewModel);
        }
    }
}