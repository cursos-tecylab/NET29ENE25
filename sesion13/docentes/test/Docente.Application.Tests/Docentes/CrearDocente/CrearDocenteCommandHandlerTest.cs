using Docentes.Application.Docentes.CrearDocente;
using Docentes.Application.Service;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;
using FluentAssertions;
using Moq;

namespace Docente.Application.Tests.Docentes.CrearDocente;

public class CrearDocenteCommandHandlerTest
{
    private readonly Mock<IDocenteRepository> _docenteRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IUsuarioService> _usuarioService;
    private readonly Mock<ICursosService> _cursosService;
    private readonly Mock<ICacheService> _cacheService;
    private readonly CrearDocenteCommandHandler _handler;

    public CrearDocenteCommandHandlerTest()
    {
        _docenteRepository = new Mock<IDocenteRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _usuarioService = new Mock<IUsuarioService>();
        _cursosService = new Mock<ICursosService>();
        _cacheService = new Mock<ICacheService>();

        _handler = new CrearDocenteCommandHandler(
            _docenteRepository.Object,
            _unitOfWork.Object,
            _usuarioService.Object,
            _cursosService.Object,
            _cacheService.Object
        );
    }

    [Fact]
    public async Task Handle_UsuarioNoExiste_DebeRetornarError()
    {
        // Arrange
        var command = new CrearDocenteCommand(Guid.NewGuid(), Guid.NewGuid());
        
        _usuarioService.Setup(x => x.UsuarioExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("UsuarioNotFound", result.Error.Code);
        Assert.Equal("El usuario no existe.", result.Error.Name);
    }

    [Fact]  
    public async Task Handle_ShouldUseCache_WhenCursoExists()
    {
        // Arrange
        var command = new CrearDocenteCommand(Guid.NewGuid(), Guid.NewGuid());

        _usuarioService.Setup(x => x.UsuarioExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _cacheService.Setup(x => x.GetCacheValueAsync<bool>(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _cacheService.Verify(x => x.GetCacheValueAsync<bool>(It.IsAny<string>()), Times.Once);
        _cursosService.Verify(x => x.CursoExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

}