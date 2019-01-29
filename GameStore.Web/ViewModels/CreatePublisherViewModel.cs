using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class CreatePublisherViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Min two char's"), MaxLength(40, ErrorMessage = "Max 40 char's")]
        [Display(Name = "Company name")]
        [RegularExpression(@"^\S((?!\s\s).)(.*\S)*$", ErrorMessage = "Cant start/end with whitespace. Beware multiply whitespaces")]
        //[RegularExpression(@"^\S(.*\S)?$", ErrorMessage = "Cant start/end with whitespace")]
        //[RegularExpression(@"^((?!\s\s).)*$", ErrorMessage = "Beware multiply whitespaces")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company Description")]
        [RegularExpression(@"^\S((?!\s\s).)(.*\S)*$", ErrorMessage = "Cant start/end with whitespace. Beware multiply whitespaces")]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
            ErrorMessage = "Invalid website link")]
        [Display(Name = "Website")]
        public string HomePage { get; set; }
    }
}