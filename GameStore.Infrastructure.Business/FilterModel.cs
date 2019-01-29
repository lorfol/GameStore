using GameStore.Infrastructure.Business.Enums;
using GameStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Business
{
    public class FilterModel : IFilterModel
    {
        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public IEnumerable<int> SelectedGenres { get; set; }

        public IEnumerable<int> SelectedPlatforms { get; set; }

        public IEnumerable<int> SelectedPublishers { get; set; }

        public string OrderByOptions { get; set; }

        public string WhenPublished { get; set; }

        public string FilterByName { get; set; }
    }
}
