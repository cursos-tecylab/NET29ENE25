using MediatR;
using Usuario.Domain.Abstractions;

namespace Usuario.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery,Result<TResponse>>
where TQuery : IQuery<TResponse>
{
    
}