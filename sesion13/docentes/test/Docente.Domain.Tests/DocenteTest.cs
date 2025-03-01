using FluentAssertions;

namespace Docente.Domain.Tests;

public class DocenteTest
{
    [Fact]
    public void Create_ShouldReturnSuccessResult_WhenValidParameters()
    {
       var usuarioId = Guid.NewGuid();
       var especialidadId = Guid.NewGuid();

       var result = Docentes.Domain.Docentes.Docente.Create(usuarioId, especialidadId);

       result.IsSuccess.Should().BeTrue();
       result.Value.Should().NotBeNull();
       result.Value.UsuarioId.Should().Be(usuarioId);
       result.Value.EspecialidadId.Should().Be(especialidadId);
       result.Value.Id.Should().NotBe(Guid.Empty);

    }
}