using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Core.DomainModels
{
    public class Game : BaseEntity
    {
        public Game()
        {
            Comments = new List<Comment>();
            Genres = new List<Genre>();
            Platforms = new List<Platform>();
            Publishers = new List<Publisher>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short UnitsInStock { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Genre> Genres { get; set; }

        public virtual List<Platform> Platforms { get; set; }

        public virtual List<Publisher> Publishers { get; set; }

        public DateTime AddingDate { get; set; }

        public int CountOfViews { get; set; }
    }
}
