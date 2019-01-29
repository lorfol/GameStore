using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Interfaces
{
    public interface IFilterModel
    {
        decimal? PriceFrom { get; set; }

        decimal? PriceTo { get; set; }

        IEnumerable<int> SelectedGenres { get; set; }

        IEnumerable<int> SelectedPlatforms { get; set; }

        IEnumerable<int> SelectedPublishers { get; set; }

        string OrderByOptions { get; set; }

        string WhenPublished { get; set; }

        string FilterByName { get; set; }
    }
}
