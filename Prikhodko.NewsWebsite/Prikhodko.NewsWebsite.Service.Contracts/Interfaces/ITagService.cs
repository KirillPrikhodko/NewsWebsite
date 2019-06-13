using System.Collections.Generic;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface ITagService
    {
        IEnumerable<TagServiceModel> GetAll();
    }
}