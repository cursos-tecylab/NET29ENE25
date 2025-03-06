using FluentValidation;

namespace Usuario.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
       RuleFor(u => u.CorreoElectronico).NotEmpty().WithMessage("El correo electronico no puede ser vacio."); 
       RuleFor(u => u.NombrePersona).NotEmpty().WithMessage("El nombre de la persona no puede ser vacio.");
       RuleFor(u => u.ApellidoPaterno).NotEmpty();
       RuleFor(u => u.FechaNacimiento).LessThan(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser la actual");
    }
}