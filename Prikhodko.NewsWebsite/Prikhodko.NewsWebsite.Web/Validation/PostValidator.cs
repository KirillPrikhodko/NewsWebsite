using System.Linq;
using FluentValidation;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web.Validation
{
    public class PostValidator : AbstractValidator<PostViewModel>
    {

        public PostValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Tags).NotEmpty(); //TODO: Add validation mechanism that lets a post have not more than 6 tags (create IEnumerable<TagViewModel> Validator?
            RuleFor(x => x.Title).NotEmpty().Matches(@"[A-aZ-z \d-\&'()*#\/+,./:;?@[\\\]_`|]+").MaximumLength(90);
        }
    }
}