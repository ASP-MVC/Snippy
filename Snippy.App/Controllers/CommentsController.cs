namespace Snippy.App.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using Snippy.App.Models.BindingModels;
    using Snippy.App.Models.ViewModels;
    using Snippy.Data.UnitOfWork;
    using Snippy.Models;

    public class CommentsController : BaseController
    {
        public CommentsController(ISnippyData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var comment = Mapper.Map<Comment>(model);
                comment.CreatedAt = DateTime.Now;
                comment.AuthorId = this.UserProfile.Id;
                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();
                var dbcomment = this.Data.Comments.Find(comment.Id);
                var viewModel = Mapper.Map<CommentViewModel>(dbcomment);
                return this.PartialView("DisplayTemplates/CommentForSnipperViewModel", new [] { viewModel });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid input");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var comment = this.Data.Comments.Find(id);
            if (comment == null)
            {
                return this.HttpNotFound("Comment no longer exists");
            }

            var viewModel = Mapper.Map<CommentViewModel>(comment);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CommentViewModel model)
        {
            if (model != null)
            {
                this.Data.Comments.Delete(model.Id);
                this.Data.SaveChanges();
            }
            return this.RedirectToAction(
                "Details",
                "Snippets",
                new { id = model.SnippetId });
        }
    }
}