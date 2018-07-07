namespace DeliveryService.UI
{
    using DeliveryService.BLL.Contract;

    public sealed class ManagerFactoryResolver
    {
        public ManagerFactoryResolver(IManagerFactory managerFactory)
        {
            ManagerFactory = managerFactory;
        }

        public static IManagerFactory ManagerFactory { get; private set; }
    }
}