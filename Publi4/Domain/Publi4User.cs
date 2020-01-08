using Microsoft.AspNetCore.Identity;
using System;

namespace Publi4.Domain
{
    // Add profile data for application users by adding properties to the Publi4User class
    public class Publi4User : IdentityUser<Guid>
    {
    }

    public class Publi4Role : IdentityRole<Guid>
    {
        public Publi4Role() : base() { }

        public Publi4Role(string name) : base(name) { }
    }
}
