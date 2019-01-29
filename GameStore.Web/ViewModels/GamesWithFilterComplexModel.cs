using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.Core.DomainModels;
using PagedList;

namespace GameStore.Web.ViewModels
{
    public class GamesWithFilterComplexModel
    {
        public FilterOutputModel FilterOutput { get; set; }

        public FilterViewModel Filter{ get; set; }

        public IPagedList<GameViewModel> GameList{ get; set; }
    }
}