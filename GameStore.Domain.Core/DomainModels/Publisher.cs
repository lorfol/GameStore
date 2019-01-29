using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Core.DomainModels
{
    public class Publisher : BaseEntity
    {
        public Publisher()
        {
            Games = new List<Game>();
        }

        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [Column(TypeName = "Ntext")]
        public string Description { get; set; }

        [Column(TypeName = "Ntext")]
        public string HomePage { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
