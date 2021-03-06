﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            MapApplicationIdentityUserToApplicationIdentityUserServiceModel();
            MapApplicationIdentityUserServiceModelToApplicationIdentityUser();
            MapUserToUserServiceModel();
            MapUserServiceModelToUser();
            MapCategoryToCategoryViewModel();
            MapCategoryViewModelToCategory();
            MapTagToTagServiceModel();
            MapTagServiceModelToTag();
            MapTagsAndStrings();
            MapPostRateToPostRateServiceModel();
            MapPostRateServiceModelToPostRate();
            MapCommentToCommentServiceModel();
            MapCommentServiceModelToComment();
            MapCommentRateToCommentRateServiceModel();
            MapCommentRateServiceModelToCommentRate();
        }

        private void MapPostToPostServiceModel()
        {
            CreateMap<Post, PostServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForPath(x => x.Category, c => c.MapFrom(src => src.Category.Name))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.AuthorName, c => c.MapFrom(src => src.Author.ApplicationIdentityUser.UserName))
                .ForMember(x => x.Content,
                    c => c.MapFrom(src =>
                        src.Content)) //TODO: after the Content is configured as a separate entity, this will have to change
                .ForMember(x => x.Description, c => c.MapFrom(src => src.Description))
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Rates, c => c.MapFrom(src => src.Rates))
                .ForMember(x => x.Comments, c => c.MapFrom(src => src.Comments))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => GetAvgPostRate(src)))
                .ForMember(x => x.Created, c => c.MapFrom(src => src.Created))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private double? GetAvgPostRate(Post post)
        {
            double? result = 0;
            var quantity = post.Rates.Count;
            for (int i = 0; i < quantity; i++)
            {
                result += post.Rates[i].Value;
            }

            return quantity == 0 ? null : result / quantity;
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
                .ForMember(x => x.Tags, c => c.MapFrom(src => src.Tags.Distinct().ToList()))
                .ForMember(x => x.Title, c => c.MapFrom(src => src.Title))
                .ForMember(x => x.Rates, c => c.MapFrom(src => src.Rates))
                .ForMember(x => x.Comments, c => c.MapFrom(src => src.Comments))
                .ForMember(x => x.Created, c => c.MapFrom(src => src.Created))
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
        
        private void MapApplicationIdentityUserToApplicationIdentityUserServiceModel()
        {
            CreateMap<ApplicationIdentityUser, ApplicationIdentityUserServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.IsEnabled, c => c.MapFrom(src => src.IsEnabled))
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
                //.ForMember(x => x.Roles, c => c.MapFrom(src => src.Claims.Where(claim => claim.ClaimType == ClaimTypes.Role).Select(r => r.ClaimValue)))
                .ForMember(x => x.UserName, c => c.MapFrom(src => src.UserName))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapApplicationIdentityUserServiceModelToApplicationIdentityUser()
        {
            CreateMap<ApplicationIdentityUserServiceModel, ApplicationIdentityUser>()
                .ConstructUsing(c => new ApplicationIdentityUser())
                .ForMember(x => x.Id, c => c.Condition(src => !string.IsNullOrEmpty(src.Id)))
                .ForMember(x => x.IsEnabled, c => c.MapFrom(src => src.IsEnabled))
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
                //.ForMember(x => x.Roles, c => c.MapFrom(src => src.Roles))
                .ForMember(x => x.UserName, c => c.MapFrom(src => src.UserName))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapUserToUserServiceModel()
        {
            CreateMap<User, UserServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.ApplicationIdentityUser, c => c.MapFrom(src => src.ApplicationIdentityUser))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.ProfileImagePath, c => c.MapFrom(src => src.ProfileImagePath))
                .ForMember(x => x.Posts, c => c.MapFrom(src => src.Posts))
                .ForMember(x => x.PostRates, c => c.MapFrom(src => src.PostRates))
                .ForMember(x => x.Comments, c => c.MapFrom(src => src.Comments))
                .ForMember(x => x.CommentRates, c => c.MapFrom(src => src.CommentRates))
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForMember(x => x.Country, c => c.MapFrom(src => src.Country))
                .ForMember(x => x.DateOfBirth, c => c.MapFrom(src => src.DateOfBirth))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapUserServiceModelToUser()
        {
            CreateMap<UserServiceModel, User>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.ApplicationIdentityUser, c => c.MapFrom(src => src.ApplicationIdentityUser))
                .ForMember(x => x.AvgRate, c => c.MapFrom(src => src.AvgRate))
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForMember(x => x.Country, c => c.MapFrom(src => src.Country))
                .ForMember(x => x.DateOfBirth, c => c.MapFrom(src => src.DateOfBirth))
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
        private void MapTagToTagServiceModel()
        {
            CreateMap<Tag, TagServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForMember(x => x.PostsCount, c => c.MapFrom(src => src.Posts.Count))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapTagServiceModelToTag()
        {
            CreateMap<TagServiceModel, Tag>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.Name, c => c.MapFrom(src => src.Name))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapPostRateToPostRateServiceModel()
        {
            CreateMap<PostRate, PostRateServiceModel>()
                .ForMember(x => x.Author, c => c.MapFrom(src => src.Author))
                .ForMember(x => x.PostId, c => c.MapFrom(src => src.PostId))
                .ForMember(x => x.Value, c => c.MapFrom(src => src.Value))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapPostRateServiceModelToPostRate()
        {
            CreateMap<PostRateServiceModel, PostRate>()
                .ForMember(x => x.Author, c => c.MapFrom(src => src.Author))
                .ForMember(x => x.PostId, c => c.MapFrom(src => src.PostId))
                .ForMember(x => x.Value, c => c.MapFrom(src => src.Value))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCommentToCommentServiceModel()
        {
            CreateMap<Comment, CommentServiceModel>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.PostId, c => c.MapFrom(src => src.PostId))
                .ForMember(x => x.AuthorName, c => c.MapFrom(src => src.Author.ApplicationIdentityUser.UserName))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.Author.Id))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForMember(x => x.Rating, c => c.MapFrom(src => src.Rates.Where(x => x.Value == true).Count() - src.Rates.Where(x => x.Value == false).Count()))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCommentServiceModelToComment()
        {
            CreateMap<CommentServiceModel, Comment>()
                .ForMember(x => x.Id, c => c.MapFrom(src => src.Id))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(x => x.PostId, c => c.MapFrom(src => src.PostId))
                .ForMember(x => x.Content, c => c.MapFrom(src => src.Content))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCommentRateToCommentRateServiceModel()
        {
            CreateMap<CommentRate, CommentRateServiceModel>()
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.Author.Id))
                .ForMember(x => x.Value, c => c.MapFrom(src => src.Value))
                .ForMember(x => x.CommentId, c => c.MapFrom(src => src.Comment.Id))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapCommentRateServiceModelToCommentRate()
        {
            CreateMap<CommentRateServiceModel, CommentRate>()
                .ForMember(x => x.Value, c => c.MapFrom(src => src.Value))
                .ForMember(x => x.CommentId, c => c.MapFrom(src => src.CommentId))
                .ForMember(x => x.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}