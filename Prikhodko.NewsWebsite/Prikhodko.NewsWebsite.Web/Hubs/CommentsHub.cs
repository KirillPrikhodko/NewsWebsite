using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using Microsoft.AspNet.SignalR;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Web.Hubs
{
    public class CommentsHub : Hub
    {
        public async Task AddComment(CommentServiceModel comment)
        {

            await Clients.All.SendAsync();
        }
    }
}