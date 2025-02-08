using Bogus;
using Dapper;
using Usuario.Application.Abstractions.Data;

namespace Usuario.Api.Extensions;

public static class SeedDataExtension
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var SqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using var connection = SqlConnectionFactory.CreateConnection();

        List<object> roles;

        const string sqlContarRoles = "SELECT COUNT(*) FROM roles";

        if (connection.ExecuteScalar<int>(sqlContarRoles) == 0)
        {
            roles = [
                new { Id = Guid.NewGuid(), NombreRol = "Docente", Descripcion = "ADMIN" },
                new { Id = Guid.NewGuid(), NombreRol = "Estudiante", Descripcion = "USER" }
            ];

            const string sqlInsertarRoles = """
                INSERT INTO public.roles 
                (id, nombre_rol, descripcion)
                values (@Id, @NombreRol, @Descripcion) 
            """;

            connection.Execute(sqlInsertarRoles, roles);

            List<object> usuarios = new();

            for (int i = 0; i < 100; i++)
            {
                var fake = new Faker();
                int indexRol = fake.Random.Int(0,roles.Count()-1);
                var maximoFechaNacimiento = fake.Date.Past(50);

                var usuario = new 
                {
                    id = Guid.NewGuid(),
                    nombre_usuario = fake.Person.UserName,
                    password = fake.Person.UserName,
                    rol_id = (Guid)((dynamic)roles[indexRol]).id,
                    nombres_persona = fake.Person.FirstName,
                    apellido_paterno = fake.Person.LastName,
                    apellido_materno = fake.Person.LastName,
                    direccion_pais =  fake.Address.Country(),
                    direccion_departamento =  fake.Address.City(),
                    direccion_provincia = fake.Address.City(),
                    direccion_distrito = fake.Address.StreetName(),
                    direccion_calle = fake.Address.StreetAddress(),
                    fecha_nacimiento = fake.Date.Between(maximoFechaNacimiento,DateTime.UtcNow),
                    correo_electronico = fake.Person.Email,
                    estado = fake.PickRandom("Activo","Inactivo"),
                    fecha_ultimo_cambio = DateTime.UtcNow
                };

                usuarios.Add(usuario);
            }

            const string sqlInsertUsuarios = """
                INSERT INTO public.usuarios
                (id,nombre_usuario,password,rol_id,nombres_persona,apellido_paterno,apellido_materno,direccion_pais,direccion_departamento,direccion_provincia,direccion_distrito,direccion_calle,fecha_nacimiento,correo_electronico,estado,fecha_ultimo_cambio)
                values (@id,@nombre_usuario,@password,@rol_id,@nombres_persona,@apellido_paterno,@apellido_materno,@direccion_pais,@direccion_departamento,@direccion_provincia,@direccion_distrito,@direccion_calle,@fecha_nacimiento,@correo_electronico,@estado,@fecha_ultimo_cambio)
            """;

            connection.Execute(sqlInsertUsuarios, usuarios);
        }

    }
}
