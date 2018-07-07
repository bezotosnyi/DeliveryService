namespace DeliveryService.DAL
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;

    public sealed class Repository<T> : IRepository<T> where T : DomainObject
    {
        private readonly DbContext context;

        public DbSet<T> Entity { get; }

        public Repository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.Entity = this.context.Set<T>();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }     
    }
}