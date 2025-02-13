using MediatR;
using Usuario.Domain.Abstractions;

namespace Usuario.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> , IBaseCommand
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> , IBaseCommand
{
    
}

public interface IBaseCommand
{
    
}