using MediatR;
using Microsoft.EntityFrameworkCore;
using Usuario.Domain.Abstractions;

namespace Usuario.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{

    private readonly IPublisher _publisher;

    public ApplicationDbContext(
        DbContextOptions options,
        IPublisher publisher
        ) : base(options)
    {
        _publisher = publisher;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany( entity => {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

        foreach (var item in domainEvents)
        {
           await _publisher.Publish(item);
        }    
    }

}