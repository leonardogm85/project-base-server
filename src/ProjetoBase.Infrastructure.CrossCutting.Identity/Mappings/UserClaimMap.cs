using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Mappings
{
    public class UserClaimMap : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder) => builder.ToTable("UserClaims");
    }
}
