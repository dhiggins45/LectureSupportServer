using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using DotNetOpenAuth.OAuth2.Messages;

namespace LectureSupportServer.Controllers.Api
{
    public class Authenticate
    {
        const string clientID = "28442394418-7uqnmq187jlq53edfhhueebtb040grtk.apps.googleusercontent.com";
        const string clientSecret = "rgBZD9PjWvUAsU7IUjUcHnFy";
        const string redirectURI = "localhost/api/oauth2callback";

        AuthorizationServerDescription server = new AuthorizationServerDescription
        {
            AuthorizationEndpoint = new Uri("https://accounts.google.com/o/oauth2/auth"),
            TokenEndpoint = new Uri("https://accounts.google.com/o/oauth2/token"),
            ProtocolVersion = ProtocolVersion.V20,
        };

        List<string> scope = new List<string>
        {
          //  Google.Apis.Auth.OAuth2.  GoogleScope.ImapAndSmtp.Name,
          //  GoogleScope.EmailScope.Name
        };


    }
}
