using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter commentary text")]
        [MinLength(2, ErrorMessage = "Too short commentary.")]
        [MaxLength(200, ErrorMessage = "Max size of commentary is 200 characters.")]
        public string Body { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? GameId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentCommentId { get; set; }
    }
}