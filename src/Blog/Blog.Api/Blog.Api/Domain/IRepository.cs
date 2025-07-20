namespace Blog.Api.Domain
{
    public interface IRepository<TAggregate, TId>
        where TAggregate : class
    {
        Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }

    public interface IContentRepository : IRepository<Entities.Aggregates.Content, Guid>
    {
    }
}
