﻿using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface ICommentRateService
    {
        void Add(CommentRateServiceModel item);
    }
}