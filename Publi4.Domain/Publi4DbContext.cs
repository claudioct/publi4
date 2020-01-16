using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Publi4.Domain;
using Publi4.Domain.Entities;
using Publi4.Domain.Seed;
using System;

namespace Publi4.Domain
{
    public class Publi4DbContext : IdentityDbContext<Publi4User, Publi4Role, Guid>
    {
        public DbSet<CompanyEntity> Companies { get; set; }

        public Publi4DbContext(DbContextOptions<Publi4DbContext> options)
            : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Publi4User>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            builder.Entity<Publi4Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            //Identity core table names
            builder.Entity<Publi4User>(entity =>
            {
                entity.ToTable(name: "Users").Property(e => e.Id).HasColumnName("UserId");
            });

            builder.Entity<Publi4Role>(entity =>
            {
                entity.ToTable("Roles").Property(e => e.Id).HasColumnName("RoleId");
            });

            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
            });

            builder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
                entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

            builder.Seed();
        }
    }
}
