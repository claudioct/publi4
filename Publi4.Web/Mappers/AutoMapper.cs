using AutoMapper;
using Publi4.Domain.Entities;
using Publi4.Models.AccountViewModels;
using Publi4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Publi4User, UserForCreationViewModel>()
                .ForMember(x => x.Password, opt => opt.Ignore()).ReverseMap();
            CreateMap<CompanyEntity, CompanyModel>()
                .ReverseMap();

        }
    }
}
