namespace DeliveryService.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    using DeliveryService.DAL.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<DeliveryServiceContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DeliveryServiceContext context)
        {
        }
    }
}
