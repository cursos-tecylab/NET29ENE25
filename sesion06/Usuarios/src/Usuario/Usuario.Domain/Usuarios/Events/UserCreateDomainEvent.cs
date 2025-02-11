using Usuario.Domain.Abstractions;

namespace Usuario.Domain.Usuarios.Event;

public sealed record UserCreateDomainEvent(Guid IdUsuario) : IDomainEvent;