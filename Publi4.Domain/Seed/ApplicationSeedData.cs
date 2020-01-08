using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publi4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publi4.Domain.Seed
{
    public static class ApplicationSeedData
    {
        private static Publi4DbContext _context;
        private static UserManager<Publi4User> _userManager;
        private static RoleManager<Publi4Role> _roleManager;

        public static IHost Execute(IHost host)
        {
            using (var serviceScope = host
                .Services.GetService<IServiceScopeFactory>()
                .CreateScope())
            {
                Publi4DbContext context = serviceScope.ServiceProvider.GetService<Publi4DbContext>();
                UserManager<Publi4User> userManager = serviceScope.ServiceProvider.GetService<UserManager<Publi4User>>();
                RoleManager<Publi4Role> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Publi4Role>>();
                _context = context;
                _userManager = userManager;
                _roleManager = roleManager;

                //InitializeAppAsync().Wait();
                InitializeIdentityAsync().Wait();
            }

            return host;
        }
        /*
        public static async Task InitializeAppAsync()
        {
            if (_context.Tenants.Any() == false)
            {
                if (await _context.Tenants.AnyAsync(c => c.FantasyName == "Dojo Club SA.") == false)
                {
                    var tenant = new TenantEntity()
                    {
                        IdTenant = Guid.NewGuid(),
                        FantasyName = "Dojo Club SA.",
                        Cnpj = "57.276.394/0001-22",
                        Inactive = false,
                        CertificadoDigital = "NULL",
                        Isento = true,
                    };

                    _context.Tenants.Add(tenant);

                    var cadastroPj = new CadastroPJEntity() { IdCadastroPJ = Guid.NewGuid(), Nome = "Dojo Club", Cep = "24355-310" };
                    tenant.CadastroPJ = cadastroPj;

                    _context.CadastroPJ.Add(cadastroPj);

                    var atividade1 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 1", Descricao = "Descricao atividade 1" });
                    var atividade2 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 2", Descricao = "Descricao atividade 2" });
                    var atividade3 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 3", Descricao = "Descricao atividade 3" });

                    cadastroPj.IdAtividadePrincipal = atividade1.IdAtividadePJ;

                    var cadastroAtividade1 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade2 };
                    var cadastroAtividade2 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade3 };

                    _context.CadastroPJAtividadePJ.Add(cadastroAtividade1);
                    _context.CadastroPJAtividadePJ.Add(cadastroAtividade2);

                }

                if (await _context.Tenants.AnyAsync(c => c.FantasyName == "Pedala Ltda.") == false)
                {
                    var tenant = new TenantEntity()
                    {
                        IdTenant = Guid.NewGuid(),
                        FantasyName = "Pedala Ltda.",
                        Cnpj = "12.345.678/9012-34",
                        Inactive = false,
                        CertificadoDigital = "123456",
                        Isento = false,
                    };

                    _context.Tenants.Add(tenant);

                    var cadastroPj = new CadastroPJEntity() { IdCadastroPJ = Guid.NewGuid(), Nome = "Pedala", Cep = "20202-020" };
                    tenant.CadastroPJ = cadastroPj;

                    _context.CadastroPJ.Add(cadastroPj);

                    var atividade1 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 4", Descricao = "Descricao atividade 4" });
                    var atividade2 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 5", Descricao = "Descricao atividade 5" });
                    var atividade3 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 6", Descricao = "Descricao atividade 6" });

                    cadastroPj.IdAtividadePrincipal = atividade1.IdAtividadePJ;

                    var cadastroAtividade1 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade2 };
                    var cadastroAtividade2 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade3 };

                    _context.CadastroPJAtividadePJ.Add(cadastroAtividade1);
                    _context.CadastroPJAtividadePJ.Add(cadastroAtividade2);

                }

                if (await _context.Tenants.AnyAsync(c => c.FantasyName == "Los Desperados Ltda.") == false)
                {
                    var tenant = new TenantEntity()
                    {
                        IdTenant = Guid.NewGuid(),
                        FantasyName = "Los Desperados Ltda.",
                        Cnpj = "45.645.645/6456-45",
                        Inactive = false,
                        CertificadoDigital = "NULL",
                        Isento = true,
                    };

                    _context.Tenants.Add(tenant);

                    var cadastroPj = new CadastroPJEntity() { IdCadastroPJ = Guid.NewGuid(), Nome = "Los Desperados", Cep = "23232-323" };
                    tenant.CadastroPJ = cadastroPj;

                    _context.CadastroPJ.Add(cadastroPj);

                    var atividade1 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 7", Descricao = "Descricao atividade 7" });
                    var atividade2 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 8", Descricao = "Descricao atividade 8" });
                    var atividade3 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 9", Descricao = "Descricao atividade 9" });

                    cadastroPj.IdAtividadePrincipal = atividade1.IdAtividadePJ;

                    var cadastroAtividade1 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade2 };
                    var cadastroAtividade2 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade3 };

                    _context.CadastroPJAtividadePJ.Add(cadastroAtividade1);
                    _context.CadastroPJAtividadePJ.Add(cadastroAtividade2);

                }

                if (await _context.Tenants.AnyAsync(c => c.FantasyName == "3G Capital S/A") == false)
                {
                    var tenant = new TenantEntity()
                    {
                        IdTenant = Guid.NewGuid(),
                        FantasyName = "3G Capital S/A",
                        Cnpj = "21.212.121/2121-21",
                        Inactive = false,
                        CertificadoDigital = "NULL",
                        Isento = true,
                    };

                    var cadastroPj = new CadastroPJEntity() { IdCadastroPJ = Guid.NewGuid(), Nome = "3G Capital", Cep = "21212-121" };
                    tenant.CadastroPJ = cadastroPj;

                    var atividade1 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 10", Descricao = "Descricao atividade 10" });
                    var atividade2 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 11", Descricao = "Descricao atividade 11" });
                    var atividade3 = _cadastroPJRepository.FindOrCreateAtividade(new AtividadePJEntity() { Codigo = "Atividade 12", Descricao = "Descricao atividade 12" });

                    cadastroPj.IdAtividadePrincipal = atividade1.IdAtividadePJ;



                    var cadastroAtividade1 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade2 };
                    var cadastroAtividade2 = new CadastroPJAtividadePJEntity() { CadastroPJ = cadastroPj, AtividadePJ = atividade3 };

                    cadastroPj.AtividadesSecundarias = new List<CadastroPJAtividadePJEntity>() { cadastroAtividade1, cadastroAtividade2 };

                    _context.Tenants.Add(tenant);
                }

                _context.SaveChanges();
            }

            if (_context.TaxType.Any() == false)
            {
                //TaxType
                if (await _context.TaxType.AnyAsync(c => c.Name == "DAS") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "DAS" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "GPS") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "GPS" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "Outros – IR Retido na fonte") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "Outros – IR Retido na fonte" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "Outros – INSS Retido na fonte") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "Outros – INSS Retido na fonte" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "Outros – ISS Retido na fonte") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "Outros – ISS Retido na fonte" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "Outros – Recalculo de imposto") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "Outros – Recalculo de imposto" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "Outros – REFIS") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "Outros – REFIS" });
                }
                if (await _context.TaxType.AnyAsync(c => c.Name == "Outros – IRPJ Ganho de capital") == false)
                {
                    _context.Add(new TaxTypeEntity() { Name = "Outros – IRPJ Ganho de capital" });
                }

                _context.SaveChanges();
            }
        }
        */

        private static async Task InitializeIdentityAsync()
        {
            SeedRolesAsync().Wait();

            var email = "lecoct@gmail.com";
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var userCreate = new Publi4User()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(userCreate, "123456").Wait();
                _userManager.AddToRoleAsync(userCreate, "Administrador").Wait();

            }
            else
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, "123456");
                }
            }
        }

        private static async Task SeedRolesAsync()
        {
            string[] roles = new string[] { "Administrador", "Redator", "Cliente" };
            if (_roleManager.Roles.Any() == false)
            {
                foreach (var role in roles)
                {
                    if (await _roleManager.RoleExistsAsync(role) == false)
                    {
                        await _roleManager.CreateAsync(new Publi4Role(role));
                    }
                }
            }
        }
    }
}
