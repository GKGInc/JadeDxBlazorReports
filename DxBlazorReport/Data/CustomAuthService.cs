﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DxBlazorReport.Data
{
    public class CustomAuthService
    {
        public Dictionary<string, ClaimsPrincipal> Users { get; set; }

        public CustomAuthService()
        {
            Users = new Dictionary<string, ClaimsPrincipal>();
        }
    }
}