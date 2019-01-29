using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            RepliesCollection = new List<CommentViewModel>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentCommentId { get; set; }

        public List<CommentViewModel> RepliesCollection { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Body { get; set; }

        public string Quote { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsQuote { get; set; }
    }
}