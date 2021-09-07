using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Mappings
{
    public class UserLoginMap : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder) => builder.ToTable("UserLogins");
    }
}
