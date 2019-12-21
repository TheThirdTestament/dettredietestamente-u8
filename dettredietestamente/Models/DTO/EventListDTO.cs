using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DttInfo.Models.DTO
{
    public class EventListDTO
    {
        [JsonProperty("products")]
        public EventDTO[] Events { get; set; }
    }
}