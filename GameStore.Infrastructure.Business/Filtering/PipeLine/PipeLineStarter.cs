using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Core.DomainModels;
using GameStore.Infrastructure.Business.Enums;
using GameStore.Services.Interfaces;
using LinqKit;

namespace GameStore.Infrastructure.Business.Filtering.PipeLine
{
    public class PipeLineStarter
    {
        public Expression<Func<Game, bool>> Start(IFilterModel model)
        {
            var predicate = PredicateBuilder.True<Game>();



            return FindByName(model, predicate);
        }

        private Expression<Func<Game, bool>> PipeEnd(Expression<Func<Game, bool>> exp)
        {
            return exp;
        }

        private Expression<Func<Game, bool>> FindByName(IFilterModel model, Expression<Func<Game, bool>> exp)
        {
            if (model.FilterByName != null && model.FilterByName != String.Empty)
            {
                exp = exp.And(g => g.Name.Contains(model.FilterByName));
            }

            return SortByPriceRange(model, exp);
        }

        private Expression<Func<Game, bool>> FindByRelations(IFilterModel model, Expression<Func<Game, bool>> exp)
        {
            if (model.SelectedGenres.Count() != 0) exp = exp.And(g => g.Genres.Any(gen => model.SelectedGenres.Contains(gen.Id)));
            if (model.SelectedPlatforms.Count() != 0) exp = exp.And(g => g.Platforms.Any(plat => model.SelectedPlatforms.Contains(plat.Id)));
            if (model.SelectedPublishers.Count() != 0) exp = exp.And(g => g.Publishers.Any(pub => model.SelectedPublishers.Contains(pub.Id)));


            return PipeEnd(exp);
        }

        private Expression<Func<Game, bool>> SortByPriceRange(IFilterModel model, Expression<Func<Game, bool>> exp)
        {
            if (model.PriceFrom == null) model.PriceFrom = 0;
            if (model.PriceTo == null) model.PriceTo = 0;

            if (model.PriceFrom == 0 && model.PriceTo == 0) return FindByRelations(model, exp);
            if (model.PriceFrom > model.PriceTo) return FindByRelations(model, exp);

            exp = exp.And(g => g.Price >= model.PriceFrom && g.Price <= model.PriceTo);

            return FindByRelations(model, exp);
        }

        private Expression<Func<Game, bool>> FindByPublishTime(IFilterModel model, Expression<Func<Game, bool>> exp) // todo: domain model edit
        {
            if (model.WhenPublished == null || model.WhenPublished == String.Empty) return exp;

            if (model.WhenPublished == WhenPublishedOprionsEnum.LastWeek.ToString())
            {
                return null;
            }

            if (model.WhenPublished == WhenPublishedOprionsEnum.LastMonth.ToString())
            {
                return null;
            }

            if (model.WhenPublished == WhenPublishedOprionsEnum.LastYear.ToString())
            {
                return null;
            }

            if (model.WhenPublished == WhenPublishedOprionsEnum.TwoYears.ToString())
            {
                return null;
            }

            if (model.WhenPublished == WhenPublishedOprionsEnum.ThreeYearsAndMore.ToString())
            {
                return null;
            }

            return null;
        }
    }
}