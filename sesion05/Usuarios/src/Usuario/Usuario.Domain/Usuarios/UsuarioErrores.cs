using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Usuarios;

public class UsuarioErrores
{
    public static Error NombreUsuarioVacio = new (
        "Usuario.NombreUsuarioVacio",
        "El nombre de usuario no puede estar vacío"
    );

    
    public static Error CorreoElectronicoInvalido = new (
        "Usuario.CorreoElectronicoInvalido",
        "El correo electronico es invalido."
    );

     public static Error UsuarioInactivo = new (
        "Usuario.UsuarioInactivo",
        "El usuario ya se encuentra inactivo."
    );

     public static Error MetodoDobleFactorExiste = new (
        "Usuario.MetodoDobleFactorExiste",
        "El metodo doble factor ya esta registrado."
    );

       public static Error MetodoDobleFactorNoExiste = new (
        "Usuario.MetodoDobleFactorNoExiste",
        "El metodo doble factor no existe o esta inactivo."
    );


    
}