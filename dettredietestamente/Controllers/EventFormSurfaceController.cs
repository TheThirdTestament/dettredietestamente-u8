﻿using DttInfo.ViewModels;
using reCAPTCHA.MVC;
using System.Net.Mail;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace DttInfo.Controllers
{
    public class EventFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("eventForm", new ContactMessage());
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult HandleFormSubmit(ContactMessage model, bool captchaValid)
        {
            //if (!captchaValid)
            //{
            //    ModelState.AddModelError("_FORM", "You did not type the verification word correctly. Please try again.");
            //}
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }

            MailMessage message = new MailMessage();
            message.To.Add("mail@dettredietestamente.info");
            message.CC.Add("jan@langekaer.dk");
            message.CC.Add("jesarbov@gmail.com");
            message.Subject = "Tilmelding til arrangement via dettredietestamente.info: " + model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("jesarbov@gmail.com", "yapxnkiyjdmgskcj");
                smtp.EnableSsl = true;

                // send mail
                smtp.Send(message);
                TempData["success"] = true;
            }

            // Get the GuildUdi of the cuttent page
            GuidUdi currentPageUdi = new GuidUdi(CurrentPage.ContentType.ItemType.ToString(), CurrentPage.Key);
            // Create the new content type
            IContent msg = Services.ContentService.CreateContent(model.Subject, currentPageUdi, "message");
            msg.SetValue("messageName", model.Name);
            msg.SetValue("email", model.Email);
            msg.SetValue("subject", model.Subject);
            msg.SetValue("messageText", model.Message);
            Services.ContentService.Save(msg);

            return RedirectToCurrentUmbracoPage();
        }
    }
}