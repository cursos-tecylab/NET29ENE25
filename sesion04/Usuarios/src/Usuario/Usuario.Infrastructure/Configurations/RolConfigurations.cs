using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuario.Domain.Roles;

namespace Usuario.Infrastructure.Configurations;

public class RolConfigurations : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(rol => rol.Id);

        builder.Property(rol => rol.NombreRol)
        .HasMaxLength(50);

        builder.Property(rol => rol.Descripcion)
        .HasMaxLength(150);

        builder.HasIndex(rol => rol.NombreRol).IsUnique();
        builder.Property<uint>("version").IsRowVersion();

    }
}