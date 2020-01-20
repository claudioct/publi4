using System;
namespace Publi4.Web.Models.AccountViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Perfil { get; set; }
        public string Company { get; set; }
    }
}
