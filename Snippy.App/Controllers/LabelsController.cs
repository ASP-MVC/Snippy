namespace Snippy.App.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using Snippy.App.Models.ViewModels;
    using Snippy.Data.UnitOfWork;

    public class LabelsController : BaseController
    {
        public LabelsController(ISnippyData data)
            : base(data)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var label = this.Data.Labels.Find(id);
            if (label == null)
            {
                return this.HttpNotFound("Comment no longer exists");
            }

            var viewModel = Mapper.Map<DetailedLabelViewModel>(label);
            return this.View(viewModel);
        }
    }
}