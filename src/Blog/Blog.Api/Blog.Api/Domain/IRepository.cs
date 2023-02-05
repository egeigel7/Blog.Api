//namespace Blog.Api.Domain
//{
//    public interface IRepository
//    {
//        int SaveChanges();

//        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

//        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

//        void AddOrUpdate<TEntity>(TEntity entity)
//            where TEntity : class, IEntity;

//        void AddOrUpdate<TEntity>(IEnumerable<TEntity> entities)
//            where TEntity : class, IEntity;

//        void Update<TEntity>(TEntity entity)
//            where TEntity : class, IEntity;

//        void Update<TEntity>(IEnumerable<TEntity> entities)
//            where TEntity : class, IEntity;

//        bool Exists<TEntity>(TEntity entity)
//            where TEntity : class, IEntity;

//        void Remove<TId>(IEntity<TId> entity)
//            where TId : IEquatable<TId>;

//        void Remove<TId>(IEnumerable<IEntity<TId>> entities)
//            where TId : IEquatable<TId>;

//        TEntity Load<TEntity, TId>(TId id)
//             where TEntity : class, IEntity<TId>
//             where TId : IEquatable<TId>;

//        TEntity Load<TEntity>(long id)
//            where TEntity : class, IEntity<long>;

//        TEntity LoadEnumerationType<TEntity>(short id)
//            where TEntity : class, IEntity<short>;
//    }
