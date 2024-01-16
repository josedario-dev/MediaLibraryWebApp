using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.ConstantsGlobals
{
    public class Data
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "Question";

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public MultipartFormDataContent Content { get; set; }

        [JsonProperty("assistant_id")]
        public string AssistantId { get; set; }

        [JsonProperty("dynamicQuery")]
        public object[] DynamicQuery { get; set; }
    }
}
