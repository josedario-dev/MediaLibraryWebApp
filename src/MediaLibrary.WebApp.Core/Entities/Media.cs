using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Core.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int MediaType { get; set; }
        public int? ContributorId { get; set; }
        public string? FilePath { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Contributor Contributor { get; set; }
    }
}
