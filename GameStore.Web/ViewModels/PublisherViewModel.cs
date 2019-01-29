using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        
        public string Description { get; set; }

        [Display(Name = "Website")]
        public string HomePage { get; set; }
    }
}