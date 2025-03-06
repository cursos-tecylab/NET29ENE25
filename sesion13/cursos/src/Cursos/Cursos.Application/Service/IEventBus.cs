namespace Cursos.Application.Service;

public interface IEventBus
{
    void Publish<T>(T @event) where T : class;
}