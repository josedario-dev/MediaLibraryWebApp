﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Shared.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
