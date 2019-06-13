using AutoMapper;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;
using System.Collections.Generic;

namespace Prikhodko.NewsWebsite.Web
{
    public class NewsWebsiteWebMappingProfile : Profile
    {
        public NewsWebsiteWebMappingProfile()
        {
            MapPostViewModelToPostServiceModel();
            MapPostServiceModelToPostViewModel();
            MapEditPostViewModelToPostServiceModel();
            MapPostServiceModelToEditPostViewModel();
        }

        public void MapPostViewModelToPostServiceModel()
        {
            CreateMap<PostViewModel, PostServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Category, c => c.MapFrom(src => src.Category))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.AuthorName, c => c.MapFrom(src => src.AuthorName))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Comments, c => c.MapFrom(src => src.Comments))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Created, c => c.MapFrom(src => src.Created))
                .ForAllOtherMembers(c => c.Ignore());
        }

        public void MapPostServiceModelToPostViewModel()
        {
            CreateMap<PostServiceModel, PostViewModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Category, c => c.MapFrom(src => src.Category))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.AuthorName, c => c.MapFrom(src => src.AuthorName))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Comments, c => c.MapFrom(src => src.Comments ?? new List<CommentServiceModel>() ))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Created, c => c.MapFrom(src => src.Created))
                .ForAllOtherMembers(c => c.Ignore());
        }

        public void MapEditPostViewModelToPostServiceModel()
        {
            CreateMap<EditPostViewModel, PostServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Category, c => c.MapFrom(src => src.Category))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForAllOtherMembers(c => c.Ignore());
        }

        public void MapPostServiceModelToEditPostViewModel()
        {
            CreateMap<EditPostViewModel, PostServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Category, c => c.MapFrom(src => src.Category))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}