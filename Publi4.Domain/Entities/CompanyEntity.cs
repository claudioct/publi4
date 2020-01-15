using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;
using System.Text;

namespace Publi4.Domain.Entities
{
    public class CompanyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdCompany { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [StringLength(16, MinimumLength = 3, ErrorMessageResourceName = "InvalidStringLengthNumeric", ErrorMessageResourceType = typeof(Domain.Resources.ModelBindingValidationMessages))]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
