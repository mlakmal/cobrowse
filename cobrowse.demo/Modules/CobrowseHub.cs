using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace cobrowse.demo.Modules
{
    public class CobrowseHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.checkCobrowse(name, message);
        }

        public override Task OnConnected()
        {
            var task = base.OnConnected();
            var queryString = Context.Request.QueryString;
            if (!string.IsNullOrEmpty(queryString["userName"]))
            {
                Groups.Add(Context.ConnectionId, queryString["userName"]);
            }
            return task;
        }

        public override Task OnDisconnected()
        {
            try
            {
                var queryString = Context.Request.QueryString;
                if (!string.IsNullOrEmpty(queryString["userName"]))
                {
                    Groups.Remove(Context.ConnectionId, queryString["userName"]);
                }
            }
            catch (Exception ex)
            {

            }
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            var queryString = Context.Request.QueryString;
            if (!string.IsNullOrEmpty(queryString["userName"]))
            {
                Groups.Add(Context.ConnectionId, queryString["userName"]);
            }
            return base.OnReconnected();
        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            // your logic to fetch a user identifier goes here.

            // for example:

            /*var userId = MyCustomUserClass.FindUserId(request.User.Identity.Name);
            return userId.ToString();*/
            return "test";
        }
    }
}