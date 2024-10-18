using AuthApplication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApplication.Database.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);

            builder.Property(p => p.Email).IsRequired().HasMaxLength(150);

            builder.HasIndex(p => p.Email).IsUnique();

            builder.Property(p => p.Password).IsRequired();
        }
    }
}
