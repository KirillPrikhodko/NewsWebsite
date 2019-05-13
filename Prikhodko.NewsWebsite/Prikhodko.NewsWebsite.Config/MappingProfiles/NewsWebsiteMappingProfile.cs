using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Config.MappingProfiles
{
    public class NewsWebsiteMappingProfile : Profile
    {
        public NewsWebsiteMappingProfile()
        {
            MapPostToPostServiceModel();
            MapPostServiceModelToPost();
            MapApplicationIdentityUserToApplicationIdentityUserViewModel();
            MapApplicationIdentityUserViewModelToApplicationIdentityUser();
            MapUserToUserViewModel();
            MapUserViewModelToUser();
            MapCategoryToCategoryViewModel();
            MapCategoryViewModelToCategory();
            MapTagToTagViewModel();
            MapTagViewModelToTag();
            MapTagsAndStrings();
        }

        private void MapPostToPostServiceModel()
        {
            CreateMap<Post, PostServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForPath(x => x.Category, c => c.MapFrom(src => src.Category.Name))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.Content,
                    c => c.MapFrom(src =>
                        src.Content)) //TODO: after the Content is configured as a separate entity, this will have to change
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Rates, c => c.MapFrom(src => src.Rates))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapPostServiceModelToPost()
        {
            CreateMap<PostServiceModel, Post>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForPath(x => x.Category.Name, c => c.MapFrom(src => src.Category))
                .ForPath(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.Content,
                    c => c.MapFrom(src =>
                        src.Content)) //TODO: after the Content is configured as a separate entity, this will have to change
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Rates, c => c.MapFrom(src => src.Rates))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapTagsAndStrings()
        {
            CreateMap<Tag, string>().ConvertUsing(source => source.Name ?? string.Empty);
            CreateMap<string, Tag>()
                .ForMember(x => x.Name, c => c.MapFrom(src => src))
                .ForAllOtherMembers(c => c.Ignore());
            CreateMap<IEnumerable<Tag>, List<string>>();
            CreateMap<IEnumerable<string>, List<Tag>>();
        }
        
        private void MapApplicationIdentityUserToApplicationIdentityUserViewModel()
        {
            CreateMap<ApplicationIdentityUser, ApplicationIdentityUserServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.AccessFailedCount, c => c.MapFrom(src => src.AccessFailedCount))
                .ForMember(x => x.Claims, c => c.MapFrom(src => src.Claims)) //TODO: properly map collections
                .ForMember(x => x.Email, c => c.MapFrom(src => src.Email))
                .ForMember(x => x.EmailConfirmed, c => c.MapFrom(src => src.EmailConfirmed))
                .ForMember(x => x.LockoutEnabled, c => c.MapFrom(src => src.LockoutEnabled))
                .ForMember(x => x.LockoutEndDateUtc, c => c.MapFrom(src => src.LockoutEndDateUtc))
                .ForMember(x => x.Logins, c => c.MapFrom(src => src.Logins))
                .ForMember(x => x.PasswordHash, c => c.MapFrom(src => src.PasswordHash))
                .ForMember(x => x.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.PhoneNumberConfirmed, c => c.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(x => x.SecurityStamp, c => c.MapFrom(src => src.SecurityStamp))
                .ForMember(x => x.TwoFactorEnabled, c => c.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(x => x.User, c => c.MapFrom(x => x.User))
                .ForMember(x => x.Roles, c => c.MapFrom(src => src.Roles))
                .ForMember(x => x.UserName, c => c.MapFrom(src => src.UserName))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapApplicationIdentityUserViewModelToApplicationIdentityUser()
        {
            CreateMap<ApplicationIdentityUserServiceModel, ApplicationIdentityUser>()
                .ConstructUsing(c => new ApplicationIdentityUser())
                .ForMember(x => x.Id, c => c.Condition(src => !string.IsNullOrEmpty(src.Id)))
                .ForMember(x => x.AccessFailedCount, c => c.MapFrom(src => src.AccessFailedCount))
                .ForMember(x => x.Claims, c => c.MapFrom(src => src.Claims)) //TODO: properly map collections
                .ForMember(x => x.Email, c => c.MapFrom(src => src.Email))
                .ForMember(x => x.EmailConfirmed, c => c.MapFrom(src => src.EmailConfirmed))
                .ForMember(x => x.LockoutEnabled, c => c.MapFrom(src => src.LockoutEnabled))
                .ForMember(x => x.LockoutEndDateUtc, c => c.MapFrom(src => src.LockoutEndDateUtc))
                .ForMember(x => x.Logins, c => c.MapFrom(src => src.Logins))
                .ForMember(x => x.PasswordHash, c => c.MapFrom(src => src.PasswordHash))
                .ForMember(x => x.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.PhoneNumberConfirmed, c => c.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(x => x.SecurityStamp, c => c.MapFrom(src => src.SecurityStamp))
                .ForMember(x => x.TwoFactorEnabled, c => c.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(x => x.User, c => c.MapFrom(x => x.User))
                .ForMember(x => x.Roles, c => c.MapFrom(src => src.Roles))
                .ForMember(x => x.UserName, c => c.MapFrom(src => src.UserName))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapUserToUserViewModel()
        {
            CreateMap<User, UserServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.ApplicationIdentityUser, c => c.MapFrom(src => src.ApplicationIdentityUser))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.ProfileImagePath, c => c.MapFrom(src => src.ProfileImagePath))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapUserViewModelToUser()
        {
            CreateMap<UserServiceModel, User>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.ApplicationIdentityUser, c => c.MapFrom(src => src.ApplicationIdentityUser))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCategoryToCategoryViewModel()
        {
            CreateMap<Category, CategoryServiceModel>()
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCategoryViewModelToCategory()
        {
            CreateMap<CategoryServiceModel, Category>()
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }
        private void MapTagToTagViewModel()
        {
            CreateMap<Tag, TagServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapTagViewModelToTag()
        {
            CreateMap<TagServiceModel, Tag>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}