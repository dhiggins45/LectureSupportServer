using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LectureSupportServer.Controllers
{
    public class AppAuthFlowMetaData: FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "28442394418-7uqnmq187jlq53edfhhueebtb040grtk.apps.googleusercontent.com",
                    ClientSecret = "rgBZD9PjWvUAsU7IUjUcHnFy"
                },
                Scopes = new String[] { GmailService.Scope.GmailReadonly },
                DataStore = new FileDataStore("LectureSupportServer.MVC")
            });

        public override string GetUserId(Controller controller)
        {
            return controller.User.Identity.GetUserName();
        }

        public override IAuthorizationCodeFlow Flow
        {
            get
            {
                return flow;
            }
        }
    }
}