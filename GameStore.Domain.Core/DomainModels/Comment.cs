using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Core.DomainModels
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            Replies = new List<Comment>();
        }

        public string Name { get; set; }

        public string Body { get; set; }

        public string Quote { get; set; }

        public bool IsQuote { get; set; }

        public int? CommentId { get; set; }

        public Comment ParentComment { get; set; }

        public virtual List<Comment> Replies { get; set; }

        public int? GameId { get; set; }

        public Game Game { get; set; }
    }
}
