namespace DeliveryService.DAL.Context
{
    using System.Data.Entity;

    using DeliveryService.Domain;

    public class DeliveryServiceContext : DbContext
    {
        // Контекст настроен для использования строки подключения "DeliveryServiceContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "DeliveryService" в экземпляре Dima. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "DeliveryServiceContext" 
        // в файле конфигурации приложения.
        public DeliveryServiceContext()
            : base("name=DeliveryServiceContext")
        {
        }

        public DbSet<Courier> Couriers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Departure> Departures { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<TypeOfCargo> TypeOfCargoes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}