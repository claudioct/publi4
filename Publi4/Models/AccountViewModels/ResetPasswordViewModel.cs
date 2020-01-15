using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [StringLength(16, MinimumLength = 4, ErrorMessageResourceName = "InvalidStringLengthNumeric", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
