using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Publi4.Domain;
using Publi4.Domain.Entities;
using Publi4.Domain.Seed;
using System;

namespace Publi4.Domain
{
    public class Publi4DbContext
        : IdentityDbContext<Publi4User, Publi4Role, Guid, IdentityUserClaim<Guid>,
    UserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
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

            //Identity core table names
            builder.Entity<Publi4User>(entity =>
            {
                entity.ToTable(name: "Users")
                      .Property(e => e.Id)
                      .HasColumnName("UserId")
                      .HasDefaultValueSql("newsequentialid()");

                entity.HasMany(u => u.Roles)
                     .WithOne(ur => ur.User)
                     .HasForeignKey(ur => ur.UserId)
                     .IsRequired();
            });

            builder.Entity<Publi4Role>(role =>
            {
                role.ToTable("Roles")
                    .Property(e => e.Id)
                    .HasColumnName("RoleId")
                    .HasDefaultValueSql("newsequentialid()");

                role.HasKey(r => r.Id);
                role.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                role.Property(u => u.Name).HasMaxLength(256);
                role.Property(u => u.NormalizedName).HasMaxLength(256);

                role.HasMany<UserRole>()
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                role.HasMany<IdentityRoleClaim<Guid>>()
                    .WithOne()
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
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
                entity.HasKey(rc => rc.Id);
            });

            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
                entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

            builder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(r => new { r.UserId, r.RoleId });
            });

            builder.Seed();
        }
    }
}
