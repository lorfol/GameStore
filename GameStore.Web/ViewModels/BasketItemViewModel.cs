using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class BasketItemViewModel : GameViewModel
    {
        [Display(Name = "Count")]
        public override short UnitsInStock { get; set; }
    }
}