using MediatR;
using Usuario.Domain.Abstractions;

namespace Usuario.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}