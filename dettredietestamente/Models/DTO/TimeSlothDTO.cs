using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DttInfo.Models.DTO
{
    public class TimeSlothDTO
    {
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }
                
        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

    }
}