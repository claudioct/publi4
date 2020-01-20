using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Models.AccountViewModels
{
    public class UserForCreationViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [StringLength(256, MinimumLength = 3, ErrorMessageResourceName = "InvalidStringLength", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [StringLength(256, MinimumLength = 3, ErrorMessageResourceName = "InvalidStringLength", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [StringLength(16, MinimumLength = 4, ErrorMessageResourceName = "InvalidStringLengthNumeric", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
