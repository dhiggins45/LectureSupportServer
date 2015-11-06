using AE.Net.Mail;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using LectureSupportServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LectureSupportServer.Controllers.Api
{
    public class EmailController : Controller
    {
        private List<Message> MetaDatalist = new List<Message>();
        private List<Message> rawDataList = new List<Message>();
        private List<FileModel> mailList = new List<FileModel>();
        private List<string> mailFromList = new List<string>();
        private GmailService gmailService;

        public ActionResult index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult>  getMail(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppAuthFlowMetaData()).
                    AuthorizeAsync(cancellationToken);

            if (result.Credential == null)
                return new RedirectResult(result.RedirectUri);

            gmailService = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = result.Credential,
                ApplicationName = "Lecture Support Server"
            });

            var messageFeed = gmailService.Users.Messages.List("me").Execute().Messages;
            int i = 0;

            foreach (var email in messageFeed)
            {
                
                if (i < 5)
                {
                    var r = new UsersResource.MessagesResource.GetRequest(gmailService, "me", email.Id);
                    r.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
                    var messg = r.Execute();
                    MetaDatalist.Add(messg);
                    i++;
                }
                if (i >= 5)
                {
                    break;
                }
            }


            // var mailBody = msg.Payload.Parts.Where(x => x.MimeType == "text/plain");
            //.ToString()).ToString();

            //var req = new UsersResource.MessagesResource.GetRequest(gmailService, "me", msg);


            /*var decodedMessage = FromBase64ForUrlString(msg.ToString());
            var ConvertedMessage = System.Text.Encoding.Default.GetString(decodedMessage);
            var convertMessageForHtml = ConvertedMessage;
            
    */


            //items.Id = msg.Id;
            foreach (var lbl in MetaDatalist)
            {
                var items = new FileModel();

                if (!(lbl.Payload.Headers.Where(x => x.Name == "Subject").First().Value == null))
                {
                    items.From = lbl.Payload.Headers.Where(x => x.Name == "From").First().Value;
                    items.Subject = lbl.Payload.Headers.Where(x => x.Name == "Subject").First().Value;

                    mailList.Add(items);
                }
            }

            return Json(mailList, JsonRequestBehavior.AllowGet);
        }
    }
}
