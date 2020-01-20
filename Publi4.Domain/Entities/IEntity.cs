using System;
using System.Collections.Generic;
using System.Text;

namespace Publi4.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
