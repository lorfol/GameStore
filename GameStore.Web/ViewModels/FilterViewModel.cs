using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business;
using GameStore.Infrastructure.Business.Enums;
using GameStore.Services.Interfaces;

namespace GameStore.Web.ViewModels
{
    public class FilterViewModel
    {
        private IGenreManager _genreManager;
        private IPublisherManager _publisherManager;
        private IPlatformManager _platformManager;

        private MultiSelectList genres;
        private MultiSelectList publishers;
        private MultiSelectList platforms;

        public FilterViewModel()
        {
            SelectedGenres = new List<int>();
            SelectedPlatforms = new List<int>();
            SelectedPublishers = new List<int>();

            AllGenres = new MultiSelectList(new List<string>(), "Id", "Name", "Category");
            AllPlatforms = new MultiSelectList(new List<string>(), "Id", "Type");
            ;
            AllPublishers = new MultiSelectList(new List<string>(), "Id", "CompanyName");

            //FilterByOptions = new SelectList();
            //WhenPublished = new SelectList();
        }

        public FilterViewModel(IGenreManager genreManager, IPlatformManager platformTypeManager,
            IPublisherManager publisherManager)
        {
            _genreManager = genreManager;
            _platformManager = platformTypeManager;
            _publisherManager = publisherManager;

            SelectedGenres = new List<int>();
            SelectedPlatforms = new List<int>();
            SelectedPublishers = new List<int>();

            AllGenres = new MultiSelectList(_genreManager.GetAllGenres().OrderBy(g => g.Category), "Id", "Name",
                "Category");
            AllPlatforms = new MultiSelectList(_platformManager.GetAllPlatforms().OrderBy(g => g.Type), "Id", "Type");
            ;
            AllPublishers = new MultiSelectList(_publisherManager.GetAllPublishers().OrderBy(i => i.CompanyName), "Id",
                "CompanyName");

            ItemsPerPage = new SelectList(new List<string> { "2", "10", "20", "50", "100", "All" });
            //FilterByOptions = new SelectList();
            //WhenPublished = new SelectList();
        }

        [Display(Name = "Genres")]
        public IEnumerable<int> SelectedGenres { get; set; }

        [Display(Name = "Platforms")]
        public IEnumerable<int> SelectedPlatforms { get; set; }

        [Display(Name = "Publishers")]
        public IEnumerable<int> SelectedPublishers { get; set; }

        public MultiSelectList AllGenres
        {
            get => genres;
            set => genres = value;
        }

        public MultiSelectList AllPlatforms
        {
            get => platforms;
            set => platforms = value;
        }

        public MultiSelectList AllPublishers
        {
            get => publishers;
            set => publishers = value;
        }

        [Display(Name = "Price From")]
        [Range(0.00, double.MaxValue, ErrorMessage = "from 0.00")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price To")]
        public decimal? PriceTo { get; set; }

        [Display(Name = "Publication year")]
        public WhenPublishedOprionsEnum WhenPublished { get; set; }

        [Display(Name = "Filter by...")]
        public FilterByOptionsEnum FilterByOptions { get; set; }

        [Display(Name = "Game Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 chars")]
        public string FilterByName { get; set; }

        [Display(Name = "Items on page")]
        public SelectList ItemsPerPage { get; set; }
    }

    public class FilterOutputModel
    {
        [Display(Name = "Price From"), Range(0.00, double.MaxValue, ErrorMessage = "from 0.00")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price To")]
        public decimal? PriceTo { get; set; }

        [Display(Name = "Genres")]
        public IEnumerable<int> SelectedGenres { get; set; }

        [Display(Name = "Platforms")]
        public IEnumerable<int> SelectedPlatforms { get; set; }

        [Display(Name = "Publishers")]
        public IEnumerable<int> SelectedPublishers { get; set; }

        [Display(Name = "Filter by...")]
        public FilterByOptionsEnum? OrderByOptions { get; set; }

        [Display(Name = "Publication year")]
        public WhenPublishedOprionsEnum? WhenPublished { get; set; }

        [Display(Name = "Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 chars")]
        public string FilterByName { get; set; }

        [Display(Name = "Items on page")]
        public string ItemsPerPage { get; set; }
    }
}