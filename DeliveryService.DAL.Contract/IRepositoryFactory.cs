namespace DeliveryService.DAL.Contract
{
    using DeliveryService.Domain;

    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T : DomainObject;
    }
}