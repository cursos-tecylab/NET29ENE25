using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuario.Domain.Shared;

namespace Usuario.Infrastructure.Configurations;

public class UsuariosConfigurations : IEntityTypeConfiguration<Domain.Usuarios.Usuario>
{
    public void Configure(EntityTypeBuilder<Domain.Usuarios.Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(usuario => usuario.Id);

        builder.Property(usuario => usuario.NombrePersona)
        .HasMaxLength(50);

        builder.Property(usuario => usuario.ApellidoPaterno)
        .HasMaxLength(50);

        builder.Property(usuario => usuario.ApellidoMaterno)
        .HasMaxLength(50);

         builder.Property(usuario => usuario.Password)
        .HasMaxLength(50)
        .HasConversion(password => 
                password!.Value, value => 
                Domain.Usuarios.Password.Create(value));

        builder.Property(usuario => usuario.NombreUsuario)
        .HasMaxLength(50)
        .HasConversion(nombre => 
                nombre!.Value, value => 
                Domain.Usuarios.NombreUsuario.Create(value).Value);

        builder.Property(usuario => usuario.FechaNacimiento);

        builder.Property(usuario => usuario.CorreoElectronico)
        .HasMaxLength(100)
        .HasConversion(correo => 
                correo!.Value, value => 
                Domain.Usuarios.CorreoElectronico.Create(value).Value);

         builder.Property(usuario => usuario.CorreoElectronico)
        .HasMaxLength(100)
        .HasConversion(correo => 
                correo!.Value, value => 
                Domain.Usuarios.CorreoElectronico.Create(value).Value);

        builder.OwnsOne(usuario => usuario.Direccion);

        builder.Property(usuario => usuario.Estado)
        .HasConversion(
          estado => estado.ToString(),
          estado => Enum.Parse<Estados>(estado)
        );

        builder.Property(usuario => usuario.FechaUltimoCambio);

        builder.HasOne(usuario => usuario.Rol)
        .WithMany()
        .HasForeignKey(usuario => usuario.RolId)
        .IsRequired();

        builder.Property<uint>("version").IsRowVersion();
    }
}