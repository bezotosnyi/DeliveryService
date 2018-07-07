namespace DeliveryService.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Couriers",
                c => new
                    {
                        CourierId = c.Long(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        Passport = c.String(nullable: false, maxLength: 8),
                        Address = c.String(nullable: false, maxLength: 100),
                        ContactPhone = c.String(nullable: false),
                        ContactPhone2 = c.String(),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourierId);

            this.CreateTable(
                "dbo.Departures",
                c => new
                    {
                        DepartureId = c.Long(nullable: false, identity: true),
                        CourierId = c.Long(nullable: false),
                        DateOfDeparture = c.DateTime(nullable: false),
                        TransportId = c.Long(nullable: false),
                        PaymentForMileage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DepartureId)
                .ForeignKey("dbo.Couriers", t => t.CourierId, cascadeDelete: true)
                .ForeignKey("dbo.Transports", t => t.TransportId, cascadeDelete: true)
                .Index(t => t.CourierId)
                .Index(t => t.TransportId);

            this.CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        DeliveryId = c.Long(nullable: false, identity: true),
                        DepartureId = c.Long(nullable: false),
                        DateOfDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Long(nullable: false),
                        Mileage = c.Int(nullable: false),
                        CargeName = c.String(nullable: false),
                        TypeOfCargoId = c.Long(nullable: false),
                        CostOfCargo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DeliveryId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Departures", t => t.DepartureId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfCargoes", t => t.TypeOfCargoId, cascadeDelete: true)
                .Index(t => t.DepartureId)
                .Index(t => t.CustomerId)
                .Index(t => t.TypeOfCargoId);

            this.CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Long(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Patronymic = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        ContactPhone = c.String(nullable: false),
                        ContactPhone2 = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);

            this.CreateTable(
                "dbo.TypeOfCargoes",
                c => new
                    {
                        TypeOfCargoId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.TypeOfCargoId);

            this.CreateTable(
                "dbo.Transports",
                c => new
                    {
                        TransportId = c.Long(nullable: false, identity: true),
                        NumberOfCar = c.String(nullable: false, maxLength: 10),
                        CarModel = c.String(nullable: false, maxLength: 20),
                        DateOfRegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransportId);

            this.CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Departures", "TransportId", "dbo.Transports");
            this.DropForeignKey("dbo.Deliveries", "TypeOfCargoId", "dbo.TypeOfCargoes");
            this.DropForeignKey("dbo.Deliveries", "DepartureId", "dbo.Departures");
            this.DropForeignKey("dbo.Deliveries", "CustomerId", "dbo.Customers");
            this.DropForeignKey("dbo.Departures", "CourierId", "dbo.Couriers");
            this.DropIndex("dbo.Deliveries", new[] { "TypeOfCargoId" });
            this.DropIndex("dbo.Deliveries", new[] { "CustomerId" });
            this.DropIndex("dbo.Deliveries", new[] { "DepartureId" });
            this.DropIndex("dbo.Departures", new[] { "TransportId" });
            this.DropIndex("dbo.Departures", new[] { "CourierId" });
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Transports");
            this.DropTable("dbo.TypeOfCargoes");
            this.DropTable("dbo.Customers");
            this.DropTable("dbo.Deliveries");
            this.DropTable("dbo.Departures");
            this.DropTable("dbo.Couriers");
        }
    }
}
