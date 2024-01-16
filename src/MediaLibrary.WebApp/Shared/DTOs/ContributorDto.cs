using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediaLibrary.WebApp.Shared.DTOs
{
    public class ContributorDto
    {
        public int Id { get; set; }
        public string? ProfileId { get; set; }
        public string NickName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Biography { get; set; }
        public string? PhotoPath { get; set; }
        public string? Photo { get; set; }
    }
}