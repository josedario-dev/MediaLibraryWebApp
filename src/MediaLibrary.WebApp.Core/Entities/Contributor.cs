using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Core.Entities
{
    public class Contributor
    {
        public int Id { get; set; }

        public string? NickName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Biography { get; set; }

        public string? PhotoPath { get; set; }

        public DateTime RegisteredAt { get; set; }

        public string AccountId { get; set; }
    }
}
