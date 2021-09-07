using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder) => builder.ToTable("UserRoles");
    }
}
