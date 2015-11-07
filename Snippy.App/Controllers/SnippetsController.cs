namespace Snippy.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Ninject.Infrastructure.Language;

    using Snippy.App.Models.BindingModels;
    using Snippy.App.Models.ViewModels;
    using Snippy.Data.UnitOfWork;
    using Snippy.Infrastructure.Populators;
    using Snippy.Models;

    public class SnippetsController : BaseController
    {
        private IDropDownListPopulator populator;

        public SnippetsController(ISnippyData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult All()
        {
            var allSnippets = 
                this.Data.Snippets.All()
                .OrderByDescending(s => s.CreatedAt)
                .ThenBy(s => s.Title)
                .ProjectTo<SnippetViewModel>()
                .ToList();

            return this.View(allSnippets);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("The snippet you are looking for has been removed");
            }

            var snippetViewModel = Mapper.Map<SnippetViewModel>(snippet);
            return this.View(snippetViewModel);
        }

        [HttpGet]
        public ActionResult My()
        {
            var loggedUserSnippets =
                this.Data.Snippets.All()
                    .OrderByDescending(s => s.CreatedAt)
                    .Where(s => s.AuthorId == this.UserProfile.Id)
                    .ProjectTo<SnippetViewModel>();
            return this.View(loggedUserSnippets);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var snippetBindinModel = new SnippetBindingModel();
            this.PopulateLangues(snippetBindinModel);
            return this.View(snippetBindinModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SnippetBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var snippet = Mapper.Map<Snippet>(model);
                snippet.AuthorId = this.UserProfile.Id;
                snippet.CreatedAt = DateTime.Now;
                var labels = this.GetLabels(model.LabelsAsText);
                this.PopulateLabelsToSnippet(snippet, labels);
                this.Data.Snippets.Add(snippet);
                snippet.ProgrammingLanguage =
                    this.Data.ProgrammingLanguages.All()
                        .FirstOrDefault(
                            x => x.Id == model.ProgrammingLanguageId);
                this.Data.SaveChanges();
                return this.RedirectToAction("Details", new { id = snippet.Id });
            }
            this.PopulateLangues(model);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("Snippet no longer exists");
            }
            var model = Mapper.Map<SnippetBindingModel>(snippet);
            this.PopulateLangues(model);
            var labelsText = string.Empty;
            foreach (var label in snippet.Labels)
            {
                labelsText += label.Text + ";";
            }
            model.LabelsAsText = labelsText;
            return this.View( model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SnippetBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var snippet = this.Data.Snippets.Find(model.Id);
                snippet.Title = model.Title;
                snippet.Code = model.Code;
                snippet.Description = model.Description;
                snippet.ProgrammingLanguageId = model.ProgrammingLanguageId;

                var labels = this.GetLabels(model.LabelsAsText);
                this.AttachNewLabels(snippet, labels);
                this.Data.Snippets.Add(snippet);
                snippet.ProgrammingLanguage =
                    this.Data.ProgrammingLanguages.All()
                        .FirstOrDefault(
                            x => x.Id == model.ProgrammingLanguageId);
                this.Data.SaveChanges();
                return this.RedirectToAction("Details", new { id = snippet.Id });
            }
            this.PopulateLangues(model);
            return this.View(model);
        }

        private void AttachNewLabels(Snippet snippet, string[] labels)
        {
            foreach (var labelText in labels)
            {
                if (!snippet.Labels.Any(l => l.Text == labelText))
                {
                    var label = new Label() { Text = labelText };
                    label.Snippets.Add(snippet);
                    this.Data.Labels.Add(label);
                    this.Data.SaveChanges();
                }
            }
        }

        private void PopulateLabelsToSnippet(Snippet snippet, string[] labels)
        {
            foreach (var labelText in labels)
            {
                if (!this.LabelExists(labelText))
                {
                    var label = new Label() { Text = labelText };
                    this.Data.Labels.Add(label);
                    this.Data.SaveChanges();
                    snippet.Labels.Add(label);
                }
                else
                {
                    var label = this.Data.Labels.All().FirstOrDefault(x => x.Text == labelText);
                    label.Snippets.Add(snippet);
                }
            }
        }

        private string[] GetLabels(string labelsAsText)
        {
            string[] labelsArr = Regex.Split(labelsAsText, @" *; *");
            labelsArr = labelsArr.Where(x => x != string.Empty).ToArray();
            return labelsArr;
        }

        private bool LabelExists(string text)
        {
            return this.Data.Labels.All().Any(l => l.Text == text);
        }

        private void PopulateLangues(SnippetBindingModel model)
        {
            model.Languages = this.populator.GetLanguages();
        }
    }
}