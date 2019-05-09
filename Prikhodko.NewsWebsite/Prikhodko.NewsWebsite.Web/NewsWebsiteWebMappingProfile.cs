using AutoMapper;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web
{
    public class NewsWebsiteWebMappingProfile : Profile
    {
        public NewsWebsiteWebMappingProfile()
        {
            MapPostViewModelToPostServiceModel();
            MapPostServiceModelToPostViewModel();
        }

        public void MapPostViewModelToPostServiceModel()
        {
            CreateMap<PostViewModel, PostServiceModel>()
                .ForMember(x => x.Category, c => c.MapFrom(src => src.Category))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }

        public void MapPostServiceModelToPostViewModel()
        {
            CreateMap<PostViewModel, PostServiceModel>()
                .ForMember(x => x.Category, c => c.MapFrom(src => src.Category))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}