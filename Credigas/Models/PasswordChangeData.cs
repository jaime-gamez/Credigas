namespace Credigas.Models
{
    using System;
    using Newtonsoft.Json;

    public class PasswordChangeData
    {
        //JsonProperty(PropertyName = "login")]
        [JsonProperty(PropertyName = "login")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "pwd")]
        public string Pwd { get; set; }

        [JsonProperty(PropertyName = "newpwd")]
        public string NewPwd { get; set; }

        [JsonProperty(PropertyName = "newpwd2")]
        public string NewPwd2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

    }
}
