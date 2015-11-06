using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Web.Mvc;
using System.Collections;
using ActiveUp.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;
using System.Threading.Tasks;
using Google.Apis.Services;

namespace LectureSupportServer.Controllers.Api
{
    public class MailRepositoryController : Controller
    {
        Imap4Client client = new Imap4Client();
        UserCredential credential;

        public async Task Authorize(string mailServer, int port , bool ssl, string login,string password)
        {
            if (ssl)
            {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        new ClientSecrets {
                             ClientId = "28442394418-7uqnmq187jlq53edfhhueebtb040grtk.apps.googleusercontent.com",
                              ClientSecret = "rgBZD9PjWvUAsU7IUjUcHnFy"
                        },
                        new[] { GmailService.Scope.GmailReadonly },
                        "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));

                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gmail Scraper",
                });

                client.ConnectSsl(mailServer);
                client.Login(login, password);

                //Client.ConnectSsl(mailServer, port);
                //Client.Login(login, password);
            }
            else
            {
                client.ConnectSsl(mailServer);
                client.Login(login, password);
            }   
        }

        public IEnumerable<Message> GetAllMails(string mailBox)
        {
            return GetMails(mailBox, "ALL").Cast<Message>();
        }

        public IEnumerable<Message> GetUnreadMails(string mailBox)
        {
            return GetMails(mailBox, "UNSEEN").Cast<Message>();
        }

        

        private MessageCollection GetMails(string mailBox,string searchPhrase)
        {
            Mailbox mails = client.SelectMailbox(mailBox);
            MessageCollection messages = mails.SearchParse(searchPhrase);
            return messages;
        }
    }

}
