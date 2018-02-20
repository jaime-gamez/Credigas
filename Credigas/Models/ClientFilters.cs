namespace Credigas.Models
{
    using System;
    using Newtonsoft.Json;

    public class ClientFilters
    {
        [JsonProperty(PropertyName = "StartDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "EndDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "DiaVisita")]
        public string VisitDay { get; set; }
    }
}
