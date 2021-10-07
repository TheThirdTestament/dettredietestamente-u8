using DttInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace dtt_info.Controllers
{
    public class EventsController : RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            if (RouteData.Values["eventname"] != null)
            {
                string eventName = RouteData.Values["eventname"].ToString().ToLower();
                
                //IPublishedContent event
                IPublishedContent eventModel = model.Content.Children.Where(x => x.UrlSegment.Replace(" ","-") == eventName).FirstOrDefault();
                return View("Event", eventModel);
            }

                // The model
                EventListViewModel viewModel = new EventListViewModel(model.Content);
            //viewModel.Events = Umbraco.TypedContentAtRoot().First().Descendants().Where(x => x.DocumentTypeAlias == "event").Select(x => (Event)x).ToList();

            //return View("Event", model.Content);

            // SubMenu

            var eventDropdownDataType = Services.DataTypeService.GetDataType("Event - Event type - Dropdown");
            //var eventTypes = Services.DataTypeService.GetDataType(eventDropdownDataType.Id).PreValuesAsDictionary.Select(x => x.Value).ToList();
            ValueListConfiguration eventTypes = (ValueListConfiguration)eventDropdownDataType.Configuration;
            

            IEnumerable<IPublishedContent> allEvents = model.Content.Children.Where(x => x.ContentType.Alias == "event").Select(x => (Event)x).ToList();

            List<string> subMenuItems = new List<string>();
            foreach (var item in eventTypes.Items)
            {
                if (item.Value != "0" && allEvents.Where(x => x.GetProperty("eventType").Value<string>() == item.Value).Any())
                {
                    subMenuItems.Add(item.Value);
                }
            }
            subMenuItems.Add("Afholdte");

            viewModel.SubMenuItems = subMenuItems;

            var upcomingEvents = model.Content.Children.Where(x => x.GetProperty("EndTime").Value<DateTime>() >= DateTime.Now).OrderBy(x => x.GetProperty("StartTime").Value<DateTime>());
            
            var pastEvents = model.Content.Children.Where(x => x.GetProperty("EndTime").Value<DateTime>() < DateTime.Now).OrderBy(x => x.GetProperty("eventType").Value<string>()).ThenByDescending(x => x.GetProperty("StartTime").Value<DateTime>());

            //viewModel.Catagory = "foredrag";
            //viewModel.Events = upcomingEvents.Where(x => x.GetProperty("eventType").Value<string>() == "Foredrag").Select(x => (Event)x).ToList();

            viewModel.Catagory = "";
            viewModel.Events = upcomingEvents.Select(x => (Event)x).OrderBy(x => x.EventType).ThenBy(x => x.StartTime).ToList();


            if (RouteData.Values["category"] != null)
            {
                string category = RouteData.Values["category"].ToString().ToLower();
                viewModel.Catagory = category;

                if (!subMenuItems.Contains(category, StringComparer.CurrentCultureIgnoreCase))
                {
                    throw new HttpException(404, "Not Found");
                }

                if (category != "afholdte")
                {
                    viewModel.Events = upcomingEvents.Where(x => x.GetProperty("eventType").Value<string>().ToLower() == category).Select(x => (Event)x).ToList();
                }
                else
                {
                    viewModel.Events = pastEvents.Select(x => (Event)x).ToList();
                }
            }
            return View("Events", viewModel);
        }
    }
}