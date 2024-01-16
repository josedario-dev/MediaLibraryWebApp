using MediaLibrary.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Shared.DTOs
{
    public class MediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int MediaType { get; set; }
        public string? FilePath { get; set; }
        public string? File { get; set; }
        public string? Extension { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? ContributorId { get; set; }
        public ContributorDto? Contributor { get; set; }
    }
}
