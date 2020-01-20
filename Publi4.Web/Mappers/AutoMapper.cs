using AutoMapper;
using Publi4.Domain.Entities;
using Publi4.Models.AccountViewModels;
using Publi4.Web.Models;
using Publi4.Web.Models.AccountViewModels;
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
                .ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<UserForCreationViewModel, Publi4User>()
                .ForMember(x => x.Company, opt => opt.Ignore());

            CreateMap<CompanyEntity, CompanyModel>()
                .ReverseMap();

            CreateMap<Publi4User, UserViewModel>()
                .ForMember(p4 => p4.Id, d => d.MapFrom(u => u.Id))
                .ForMember(p4 => p4.Email, d => d.MapFrom(u => u.Email))
                .ForMember(p4 => p4.FirstName, d => d.MapFrom(u => u.FirstName))
                .ForMember(p4 => p4.LastName, d => d.MapFrom(u => u.LastName))
                .ForMember(p4 => p4.Perfil, d => d.MapFrom(u => u.Roles.First().Role.Name))
                .ForMember(p4 => p4.Company, d => d.MapFrom(u => u.Company.Nome));
        }
    }
}
