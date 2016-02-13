namespace Cartefact.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Brand = c.String(),
                        Model = c.String(),
                        Color = c.String(),
                        Description = c.String(),
                        BuyingDate = c.DateTime(nullable: false),
                        Kilometers = c.Int(nullable: false),
                        Location_Id = c.String(maxLength: 128),
                        Person_Id = c.String(maxLength: 128),
                        Status_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Nickname = c.String(),
                        DrivingHabits = c.String(),
                        DriverExperience = c.String(),
                        PasswordSalt = c.String(),
                        PasswordHash = c.String(),
                        Role_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EstimatedKilometers = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Car_Id = c.String(maxLength: 128),
                        Person_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Car_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.People", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Rentals", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Rentals", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.Cars", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Cars", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Rentals", new[] { "Person_Id" });
            DropIndex("dbo.Rentals", new[] { "Car_Id" });
            DropIndex("dbo.People", new[] { "Role_Id" });
            DropIndex("dbo.Cars", new[] { "Status_Id" });
            DropIndex("dbo.Cars", new[] { "Person_Id" });
            DropIndex("dbo.Cars", new[] { "Location_Id" });
            DropTable("dbo.Status");
            DropTable("dbo.Roles");
            DropTable("dbo.Rentals");
            DropTable("dbo.People");
            DropTable("dbo.Locations");
            DropTable("dbo.Cars");
        }
    }
}
