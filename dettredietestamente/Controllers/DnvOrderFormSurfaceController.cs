using DttInfo.ViewModels;
using reCAPTCHA.MVC;
using System;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace DttInfo.Controllers
{
    public class DnvOrderFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("dnvOrderForm", new DnvOrderInfo());
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult HandleFormSubmit(DnvOrderInfo model, bool captchaValid)
        {
            //if (!captchaValid)
            //{
            //    ModelState.AddModelError("_FORM", "You did not type the verification word correctly. Please try again.");
            //}
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }

            MailMessage message = new MailMessage();
            message.To.Add("dettredietestamente@gmail.com");
            message.CC.Add("jan@langekaer.dk");
            message.CC.Add("jesarbov@gmail.com");
            message.Subject = "Bestilling af DNV - dettredietestamente.info";
            message.From = new MailAddress(model.Email, model.FullName);
            message.Body = model.FullName + "<br />";
            message.Body += model.Address + "<br />";
            message.Body += model.ZipTown + "<br />";
            message.Body += "Tlf.: " + model.Phone + "<br />";
            message.Body += "Email: " + model.Email + "<br />";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;


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
            IContent msg = Services.ContentService.CreateContent((model.FullName + "" + DateTime.Now), currentPageUdi, "dnvOrder");
            msg.SetValue("fullName", model.FullName);
            msg.SetValue("address", model.Address);
            msg.SetValue("zipTown", model.ZipTown);
            msg.SetValue("phone", model.Phone);
            msg.SetValue("email", model.Email);
            Services.ContentService.Save(msg);

            return RedirectToCurrentUmbracoPage();
        }
    }
}