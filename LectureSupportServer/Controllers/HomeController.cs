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

namespace LectureSupportServer.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private List<Message> MetaDatalist = new List<Message>();
        private List<Message> rawDataList = new List<Message>();
        private List<string> mailSubjectList = new List<string>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GmailAsync(CancellationToken cancellationToken)
        {
            ViewBag.Message = "Your drive page.";

            var result = await new AuthorizationCodeMvcApp(this, new AppAuthFlowMetaData()).
                    AuthorizeAsync(cancellationToken);

         //   if (result.Credential == null)
          //      return new RedirectResult(result.RedirectUri);

            var gmailService = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = result.Credential,
                ApplicationName = "Lecture Support Server"
            });


            var messageFeed = gmailService.Users.Messages.List("me").Execute().Messages;
               
            foreach (var email in messageFeed)
            {
                var r = new UsersResource.MessagesResource.GetRequest(gmailService, "me",email.Id);
                r.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
                var messg = r.Execute();
                MetaDatalist.Add(messg);
            }


            // var mailBody = msg.Payload.Parts.Where(x => x.MimeType == "text/plain");
            //.ToString()).ToString();

            //var req = new UsersResource.MessagesResource.GetRequest(gmailService, "me", msg);


            /*var decodedMessage = FromBase64ForUrlString(msg.ToString());
            var ConvertedMessage = System.Text.Encoding.Default.GetString(decodedMessage);
            var convertMessageForHtml = ConvertedMessage;
            
    */
            var items = new FileModel();

            //items.Id = msg.Id;
            foreach (var lbl in MetaDatalist)
            {
                if (!(lbl.Payload.Headers.Where(x => x.Name == "Subject").First().Value == null))
                {
                    mailSubjectList.Add(lbl.Payload.Headers.Where(x => x.Name == "Subject").First().Value);
                }
            }
           // items.downloadUrl = msg.InternalDate.Value.ToString();
                 
          
            return Json(mailSubjectList,JsonRequestBehavior.AllowGet);

        }

        public static byte[] FromBase64ForUrlString(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }
    }
}