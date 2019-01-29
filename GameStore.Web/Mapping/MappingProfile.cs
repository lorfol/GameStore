using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using AutoMapper;
using GameStore.Domain.Core.DomainModels;
using GameStore.Web.ViewModels;
using System.Linq;
using System.Web.Compilation;
using GameStore.Infrastructure.Business;

namespace GameStore.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() // TODO : more mapping for create models etc.
        {
            CreateMap<Game, GameViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Comments.Select(c => new CommentViewModel { Name = c.Name, Body = c.Body, Id = c.Id, ParentCommentId = c.CommentId })))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => string.Join(", ", src.Genres.Select(g => g.Name))))
                .ForMember(dest => dest.Platforms,
                    opt => opt.MapFrom(src => string.Join(", ", src.Platforms.Select(p => p.Type))))
                .ForMember(dest => dest.Publishers,
                    opt => opt.MapFrom(src => src.Publishers.Select(p => p.CompanyName)));

            CreateMap<Game, BasketItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnitsInStock, opt => opt.Equals(0))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => string.Join(", ", src.Genres.Select(g => g.Name))))
                .ForMember(dest => dest.Platforms,
                    opt => opt.MapFrom(src => string.Join(", ", src.Platforms.Select(p => p.Type))))
                .ForMember(dest => dest.Publishers,
                    opt => opt.MapFrom(src => src.Publishers.Select(p => p.CompanyName)));

            CreateMap<CreateGameViewModel, Game>().ReverseMap();
            //    .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.SelectedPlatforms.ToList()))
            //    .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.SelectedGenres.ToList()));

            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.ParentCommentId, opt => opt.MapFrom(src => src.CommentId))
                .ForMember(dest => dest.RepliesCollection, opt => opt.MapFrom(src => src.Replies));

            CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            CreateMap<Publisher, CreatePublisherViewModel>().ReverseMap();

            CreateMap<BasketItemViewModel, OrderDetails>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.UnitsInStock))
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id));

            CreateMap<FilterOutputModel, FilterModel>()
                .ForMember(dest => dest.OrderByOptions, opt => opt.MapFrom(src => src.OrderByOptions.Value.ToString()))
                .ForMember(dest => dest.WhenPublished, opt => opt.MapFrom(src => src.WhenPublished.Value.ToString()));
        }
    }
}