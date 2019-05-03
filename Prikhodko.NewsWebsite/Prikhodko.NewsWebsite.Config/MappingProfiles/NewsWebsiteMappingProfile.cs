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
            MapApplicationIdentityUserToApplicationIdentityUserViewModel();
            MapApplicationIdentityUserViewModelToApplicationIdentityUser();
            MapUserToUserViewModel();
            MapUserViewModelToUser();
        }

        private void MapPostToPostViewModel()
        {
            CreateMap<Post, PostViewModel>()
                .ForPath(x => x.Category.Name, c => c.MapFrom(src => src.Category.Name))
                .ForPath(x => x.Author, c => c.MapFrom(src => src.Author.ApplicationIdentityUser.UserName))
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
                .ForPath(x => x.Author.ApplicationIdentityUser.UserName, c => c.MapFrom(src => src.Author))
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

        private void MapApplicationIdentityUserToApplicationIdentityUserViewModel()
        {
            CreateMap<ApplicationIdentityUser, ApplicationIdentityUserViewModel>()
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
            CreateMap<ApplicationIdentityUserViewModel, ApplicationIdentityUser>()
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
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.ApplicationIdentityUser, c => c.MapFrom(src => src.ApplicationIdentityUser))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.ProfileImagePath, c => c.MapFrom(src => src.ProfileImagePath))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapUserViewModelToUser()
        {
            CreateMap<UserViewModel, User>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.ApplicationIdentityUser, c => c.MapFrom(src => src.ApplicationIdentityUser))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}