namespace Snippy.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Snippy.Data.UnitOfWork;
    using Snippy.Infrastructure.Caching;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private ISnippyData data;
        private ICacheService cache;

        public DropDownListPopulator(ISnippyData data, ICacheService cache)
        {
            this.data = data;
            this.cache = cache;
        }


        public IEnumerable<SelectListItem> GetLanguages()
        {
            var subCategories = this.cache.Get<IEnumerable<SelectListItem>>("languages",
                () =>
                {
                    return this.data.ProgrammingLanguages
                       .All()
                       .OrderBy(x => x.Name)
                       .Select(c => new SelectListItem()
                       {
                           Value = c.Id.ToString(),
                           Text = c.Name
                       }).ToList();
                });

            return subCategories;
        }
    }
}