using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Entities;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Mappings
{
    public class UserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.Name)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(u => u.Administrator)
                .IsRequired();

            builder.Property(u => u.Active)
                .IsRequired();

            builder.HasIndex(u => u.Name);

            builder.ToTable("Users");
        }
    }
}
