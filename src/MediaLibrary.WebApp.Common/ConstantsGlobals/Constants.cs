using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.ConstantsGlobals
{
    public static class Constants
    {
        /// <summary>
        /// URLBase for chat
        /// </summary>
        public const string AIBaseURI = "http://127.0.0.1:5000";
        //public const string AIBaseURI = "https://MediaLibrary.azurewebsites.net";

        /// <summary>
        /// Supported file types
        /// </summary>
        public const string TextFileTypes = ".txt,.pdf,.csv,.md,.docx,.sql";

        /// <summary>
        /// Supported file types
        /// </summary>
        public const string AllFileTypes = "*.*";

        /// <summary>
        /// Document types
        /// </summary>
        public enum DocumentType
        {
            Sql = 1,
            Document = 2
        }

        /// <summary>
        /// Supported file types
        /// </summary>
        public const string FileExcelTypes = ".xlsx,.xls";

        /// <summary>
        /// Tamaño máximo permitido para almacenar archivos
        /// </summary>
        public static int MaxFileSize = 10 * 1024 * 1024;
        public static string MaxFileSizeString = "10 * 1024 * 1024";
        public static string AssistantScriptGraphicId = "asst_yP3X8gOP3cJItOHwFSO2SH2Y";
        public static string AssistantContextFileId = "asst_N0zMbpXUmKsfOVmnOPc51QsB";
        public static string NonDefinedClient = "0";
    }
}
