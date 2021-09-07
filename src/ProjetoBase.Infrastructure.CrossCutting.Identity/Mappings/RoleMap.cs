using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Entities;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(r => r.Description)
                .HasColumnType("varchar(900)")
                .IsRequired();

            builder.Property(r => r.Active)
                .IsRequired();

            builder.ToTable("Roles");
        }
    }
}
