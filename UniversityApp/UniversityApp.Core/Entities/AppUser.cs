using System;
using Microsoft.AspNetCore.Identity;

namespace UniversityApp.Core.Entities
{
	public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}

