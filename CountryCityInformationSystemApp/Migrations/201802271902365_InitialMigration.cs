namespace CountryCityInformationSystemApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        About = c.String(),
                        NoOfDwellers = c.Int(nullable: false),
                        Location = c.String(),
                        Weather = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        About = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
