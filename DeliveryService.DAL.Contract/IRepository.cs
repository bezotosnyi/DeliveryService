namespace DeliveryService.DAL.Contract
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DeliveryService.Domain;

    public interface IRepository<T> : IDisposable where T : DomainObject
    {
        DbSet<T> Entity { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}