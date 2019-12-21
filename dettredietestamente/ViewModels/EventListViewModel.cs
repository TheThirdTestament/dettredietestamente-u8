using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace DttInfo.ViewModels
{
    public class EventListViewModel : ContentModel
    {
            public EventListViewModel(IPublishedContent content) : base(content)
            {
                IEnumerable<Event> Events = new List<Event>();
                IEnumerable<string> SubMenuItems = new List<string>();
            }
            public string Catagory { get; set; }
            public IEnumerable<Umbraco.Web.PublishedModels.Event> Events { get; set; }
            public IEnumerable<string> SubMenuItems { get; set; }

    }
}