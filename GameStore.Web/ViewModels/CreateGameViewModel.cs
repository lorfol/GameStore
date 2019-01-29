using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class CreateGameViewModel
    {
        public CreateGameViewModel()
        {
            SelectedGenres = new List<int>();
            SelectedPlatforms = new List<int>();
            SelectedPublishers = new List<int>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MinLength(2,ErrorMessage = "Can't be less than 2"), MaxLength(50, ErrorMessage = "Name length 50 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field required")]
        [Display(Name = "Unique Key")]
        [MinLength(2, ErrorMessage = "Minimum 2 char's"), MaxLength(8, ErrorMessage = "Maximum 8 char's")]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$", ErrorMessage = "Shouldn't contains special char's or whitespaces")]
        public string Key { get; set; }

        [Required(ErrorMessage = "This field required")]
        public string Description { get; set; }

        [DisplayName("Price ($)")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price")]
        [Range(0.01, 9999.99, ErrorMessage = "From 0.01 to 9999.99")]
        [Required(ErrorMessage = "This field required")]
        public decimal Price { get; set; }

        [Display(Name = "Units in stock")]
        [Required(ErrorMessage = "This field required")]
        [Range(0, short.MaxValue, ErrorMessage = "From 0 to 9999")]
        public short UnitsInStock { get; set; }

        [Required(ErrorMessage = "This field required")]
        public bool Discontinued { get; set; }

        public MultiSelectList AllGenres { get; set; }

        [Display(Name = "Genres")]
        [Required(ErrorMessage = "Choose one or more")]
        public List<int> SelectedGenres { get; set; }

        public MultiSelectList AllPlatforms { get; set; }

        [Display(Name = "Platforms")]
        [Required(ErrorMessage = "Choose one or more")]
        public List<int> SelectedPlatforms { get; set; }

        public MultiSelectList AllPublishers { get; set; }

        [Display(Name = "Publishers")]
        [Required(ErrorMessage = "Choose one or more")]
        public List<int> SelectedPublishers { get; set; }
    }
}