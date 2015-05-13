using CurioExchange.Models;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CurioExchange.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ContactModel model)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo("info@levelupdigital.be");
            myMessage.From = new System.Net.Mail.MailAddress(model.Email, model.Name);
            myMessage.Subject = model.Subject;
            myMessage.Text = model.Message;
            myMessage.Html = model.Message;

            var credentials = new NetworkCredential(
                       ConfigurationManager.AppSettings["mailAccount"],
                       ConfigurationManager.AppSettings["mailPassword"]
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
                TempData["FeedbackMessage"] = "Email sent successfully.";
            }
            else
            {
                await Task.FromResult(0);
                TempData["ErrorMessage"] = "There was an issue with sending your email, please try again later.";
            }

            return RedirectToAction("Index");
        }
    }
}