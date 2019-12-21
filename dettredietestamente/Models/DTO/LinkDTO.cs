using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DttInfo.Models.DTO
{
    public class LinkDTO
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("newWindow")]
        public bool NewWindow { get; set; }
    }
}