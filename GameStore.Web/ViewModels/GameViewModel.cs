using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            Comments = new List<CommentViewModel>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public virtual short UnitsInStock { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        [Required]
        public string Genres { get; set; }

        [Required]
        public string Platforms { get; set; }

        [Required]
        public List<string> Publishers { get; set; }

        public DateTime AddingDate { get; set; }

        public int CountOfViews { get; set; }
    }
}