using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Core.DomainModels
{
    public class Genre : BaseEntity
    {
        public Genre()
        {
            Games = new List<Game>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        //public int? ParentGenreId { get; set; }

        //public virtual Genre SupGenre { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
