using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Mappings
{
    public class UserTokenMap : IEntityTypeConfiguration<IdentityUserToken<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder) => builder.ToTable("UserTokens");
    }
}
