using System;
using System.ComponentModel.DataAnnotations;

namespace Publi4.Web.Models.AccountViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
        
        public string Perfil { get; set; }
        
        [Display(Name = "Nome")]
        public string Company { get; set; }
    }
}
