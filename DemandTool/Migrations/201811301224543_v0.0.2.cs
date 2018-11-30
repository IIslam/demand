namespace DemandTool.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DemandModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        RAG = c.Int(nullable: false),
                        DemandStatus = c.Int(nullable: false),
                        ServiceLine = c.Int(nullable: false),
                        Phase = c.Int(nullable: false),
                        AssignedTeam = c.Int(nullable: false),
                        TeamStatus = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DemandModels");
        }
    }
}
