using AutoMapper;
using Prikhodko.NewsWebsite.Config.MappingProfiles;

namespace Prikhodko.NewsWebsite.Web
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.AddProfile<NewsWebsiteMappingProfile>();
                c.AddProfile<NewsWebsiteWebMappingProfile>();
            });
        }
    }
}