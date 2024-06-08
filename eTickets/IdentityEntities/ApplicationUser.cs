﻿using Microsoft.AspNetCore.Identity;

namespace eTickets.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PersonName { get; set; }

    }
}
