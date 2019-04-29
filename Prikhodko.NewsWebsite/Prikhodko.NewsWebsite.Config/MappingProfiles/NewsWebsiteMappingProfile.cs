using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Config.MappingProfiles
{
    public class NewsWebsiteMappingProfile : Profile
    {
        public NewsWebsiteMappingProfile()
        {
            MapPostToPostViewModel();
            MapPostViewModelToPost();
        }

        private void MapPostToPostViewModel()
        {
            CreateMap<Post, PostViewModel>()
                .ForPath(x => x.Category.Name, c => c.MapFrom(src => src.Category.Name))
                .ForPath(x => x.Author, c => c.MapFrom(src => src.Author.UserName))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Content,
                    c => c.MapFrom(src =>
                        src.Content)) //TODO: after the Content is configured as a separate entity, this will have to change
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Rates, c => c.MapFrom(src => src.Rates)) //TODO: this might not work
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapPostViewModelToPost()
        {
            CreateMap<PostViewModel, Post>()
                .ForPath(x => x.Category.Name, c => c.MapFrom(src => src.Category.Name))
                .ForPath(x => x.Author.UserName, c => c.MapFrom(src => src.Author))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Content,
                    c => c.MapFrom(src =>
                        src.Content)) //TODO: after the Content is configured as a separate entity, this will have to change
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Rates, c => c.MapFrom(src => src.Rates)) //TODO: this might not work
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}