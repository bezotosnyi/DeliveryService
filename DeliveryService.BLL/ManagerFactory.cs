namespace DeliveryService.BLL
{
    using System;

    using DeliveryService.BLL.Contract;
    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;

    public class ManagerFactory : IManagerFactory
    {
        private readonly IRepositoryFactory repositoryFactory;

        public ManagerFactory(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public IUserManager CreateUserManager()
        {
            return new UserManager(this.repositoryFactory.Create<User>());
        }

        public ICustomerManager CreateCustomerManager()
        {
            return new CustomerManager(this.repositoryFactory.Create<Customer>());
        }

        public ICourierManager CreateCourierManager()
        {
            return new CourierManager(this.repositoryFactory.Create<Courier>());
        }

        public ITypeOfCargoManager CreateTypeOfCargoManager()
        {
            return new TypeOfCargoManager(this.repositoryFactory.Create<TypeOfCargo>());
        }

        public ITransportManager CreateTransportManager()
        {
            return new TransportManager(this.repositoryFactory.Create<Transport>());
        }

        public IDeliveryManager CreateDeliveryManager()
        {
            return new DeliveryManager(
                this.repositoryFactory.Create<Delivery>(),
                this.CreateCustomerManager(),
                this.CreateCourierManager(),
                this.CreateTypeOfCargoManager(),
                this.CreateTransportManager(),
                this.CreateDepartureManager());
        }

        public IDepartureManager CreateDepartureManager()
        {
            return new DepartureManager(this.repositoryFactory.Create<Departure>());
        }

        public ISelectionManager CreateSelectionManager()
        {
            return new SelectionManager(this.CreateDeliveryManager());
        }

        public IReportManager CreateReportManager()
        {
            return new ReportManager(this.CreateDeliveryManager());
        }
    }
}