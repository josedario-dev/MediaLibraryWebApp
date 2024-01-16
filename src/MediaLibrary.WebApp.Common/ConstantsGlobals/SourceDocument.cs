using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.ConstantsGlobals
{

    public class SourceDocument
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("document_name")]
        public string DocumentName { get; set; }

        [JsonProperty("document_id")]
        public string DocumentId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
