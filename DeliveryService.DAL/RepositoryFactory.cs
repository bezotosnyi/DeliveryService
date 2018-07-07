namespace DeliveryService.DAL
{
    using DeliveryService.DAL.Context;
    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;

    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> Create<T>() where T : DomainObject
        {
            return new Repository<T>(new DeliveryServiceContext());
        }
    }
}