using GameStore.Domain.Core.DomainModels;
using GameStore.Infrastructure.Business.Enums;
using GameStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Infrastructure.Business.Filtering
{
    public class Sorter
    {
        public IEnumerable<Game> OrderByOptions(IFilterModel model, IEnumerable<Game> games)
        {
            if (model.OrderByOptions == null || model.OrderByOptions == String.Empty) return games;

            if (model.OrderByOptions == FilterByOptionsEnum.MostCommented.ToString())
            {
                var result = games.OrderByDescending(g => g.Comments.Count);

                return result;
            }

            if (model.OrderByOptions == FilterByOptionsEnum.MostPopular.ToString())
            {
                var result = games.OrderBy(g => g.CountOfViews);

                return result;
            }

            if (model.OrderByOptions == FilterByOptionsEnum.New.ToString())
            {
                var result = games.OrderBy(g => g.AddingDate);

                return result;
            }

            if (model.OrderByOptions == FilterByOptionsEnum.PriceASC.ToString())
            {
                var result = games.OrderBy(g => g.Price);

                return result;
            }

            if (model.OrderByOptions == FilterByOptionsEnum.PriceDESC.ToString())
            {
                var result = games.OrderByDescending(g => g.Price);

                return result;
            }

            return games;
        }
    }
}
