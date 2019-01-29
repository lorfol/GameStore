using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Core.DomainModels
{
    public class Platform : BaseEntity
    {
        public Platform()
        {
            Games = new List<Game>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Type { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
