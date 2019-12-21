using DttInfo.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;

namespace DttInfo.API
{
    public class EventsController : UmbracoApiController
    {

        public IEnumerable<string> GetAllProducts()
        {
            return new[] { "Table", "Chair", "Desk", "Computer" };
        }

        public string GetAllEvents()
        {
            List<EventDTO> events = new List<EventDTO>();
            var model = Umbraco.ContentAtRoot().First().Descendants().Where(x => x.IsDocumentType("event")).Select(x => (Event)x).ToList();

            foreach (var item in model)
            {
                List<ImageDTO> imageList = new List<ImageDTO>();
                List<LinkDTO> videoLinkList = new List<LinkDTO>();
                List<TimeSlothDTO> timeSlothList = new List<TimeSlothDTO>();

                if (item.IntroImages != null && item.IntroImages.Count() > 1) { 
                    foreach (var image in item.IntroImages) {
                        imageList.Add(new ImageDTO { 
                            Id = image.Id,
                            Name= image.GetProperty("name").Value<string>(),
                            Url = image.GetProperty("url").Value<string>(),
                            Height = image.GetProperty("height").Value<int>(),
                            Width = image.GetProperty("width").Value<int>(),
                            Size = image.GetProperty("size").Value<int>(),
                        });
                    }
                }


                if (item.VideoLinks != null && item.VideoLinks.Count() > 1)
                {
                    // TODO Update to Umbraco 8
                    foreach (var link in item.VideoLinks)
                    {
                        //videoLinkList.Add(new LinkDTO
                        //{
                        //    Id = link.Id,
                        //    Caption = link.Caption,
                        //    Link= link.Link,
                        //    NewWindow = link.NewWindow
                        //});
                    }
                }

                if (item.Time != null && item.Time.Count() > 1)
                {
                    foreach (var timeSloth in item.Time)
                    {
                        timeSlothList.Add(new TimeSlothDTO
                        {
                            StartTime = timeSloth.GetProperty("startTime").Value<DateTime>(),
                            EndTime = timeSloth.GetProperty("endTime").Value<DateTime>(),
                        });
                    }
                }

                events.Add(new EventDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Title = item.GetProperty("title").Value().ToString(),
                    SubTitle = item.GetProperty("subTitle").Value().ToString(),
                    ShortDestription = item.GetProperty("shortDescription").Value().ToString(),
                    //Details = item.GetProperty("details").ToString(),
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Location = new LocationDTO { 
                        Id = item.Location2.Id, 
                        Title = item.Location2.GetProperty("title").Value().ToString(),
                        Building = item.Location2.GetProperty("building").Value().ToString(),
                        Address = item.Location2.GetProperty("address").Value().ToString(),
                        Zip = item.Location2.GetProperty("zip").Value().ToString(),
                        City = item.Location2.GetProperty("city").Value().ToString(),
                        Country = item.Location2.GetProperty("country").Value().ToString(),
                        Description= item.Location2.GetProperty("description").Value().ToString()
                    },
                    Registration = item.GetProperty("registration").Value().ToString(),
                    TimeSloths = timeSlothList,
                    EventType = item.EventType,
                    Images = imageList,
                    VideoLinks = videoLinkList
                });
            }
            return JsonConvert.SerializeObject(events);
        }
    }
}