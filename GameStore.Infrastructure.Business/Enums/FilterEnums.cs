using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.Interfaces;

namespace GameStore.Infrastructure.Business.Enums
{
    public enum WhenPublishedOprionsEnum
    {
        [Display(Name = "Last week")]
        LastWeek = 1,

        [Display(Name = "Last month")]
        LastMonth,

        [Display(Name = "Last year")]
        LastYear,

        [Display(Name = "Two years")]
        TwoYears,

        [Display(Name = "3 years and more")]
        ThreeYearsAndMore
    };

    public enum FilterByOptionsEnum
    {
        [Display(Name = "Most popular")]
        MostPopular = 1,

        [Display(Name = "Most commented")]
        MostCommented,

        [Display(Name = "Prise ASC")]
        PriceASC,

        [Display(Name = "Price DESC")]
        PriceDESC,

        [Display(Name = "New")]
        New
    };
}
