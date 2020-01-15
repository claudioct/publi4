using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Publi4.Domain.Entities
{
    // Add profile data for application users by adding properties to the Publi4User class
    public class Publi4User : IdentityUser<Guid>
    {
        [NotMapped]
        public string Perfil { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Publi4Role : IdentityRole<Guid>
    {
        public Publi4Role() : base() { }

        public Publi4Role(string name) : base(name) { }
    }
}
