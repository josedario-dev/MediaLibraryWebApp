using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace MediaLibrary.WebApp.Common.ConstantsGlobals
{
    public class FlaskResponse
    {
        [JsonProperty("chats")]
        public List<Chat> Chats { get; set; }

        // Other existing properties
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }

        [JsonProperty("url")]
        public string UploadUrl { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("source_documents")]
        public List<SourceDocument> SourceDocuments { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Chat
    {
        [JsonProperty("Fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("Chat_id")]
        public string ChatId { get; set; }

        [JsonProperty("Nombre_agente")]
        public string NombreAgente { get; set; }

        [JsonProperty("Nombre_visitante")]
        public string NombreVisitante { get; set; }
    }
}
