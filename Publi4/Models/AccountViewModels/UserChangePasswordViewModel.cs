using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Publi4.Models.AccountViewModels
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class UserChangePasswordViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4)]
        [Display(Name = "Senha")]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4)]
        [Display(Name = "Login")]
        public string NewPassword { get; set; }
    }
}
