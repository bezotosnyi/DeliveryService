namespace DeliveryService.BLL.Contract
{
    public interface IManagerFactory
    {
        IUserManager CreateUserManager();

        ICustomerManager CreateCustomerManager();

        ICourierManager CreateCourierManager();

        ITypeOfCargoManager CreateTypeOfCargoManager();

        ITransportManager CreateTransportManager();

        IDeliveryManager CreateDeliveryManager();

        IDepartureManager CreateDepartureManager();

        ISelectionManager CreateSelectionManager();

        IReportManager CreateReportManager();
    }
}