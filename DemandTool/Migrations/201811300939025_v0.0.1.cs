namespace DemandTool.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Demands",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DemandNumber = c.String(),
                        SubmissionDate = c.DateTime(),
                        DemandDesc = c.String(),
                        Priority = c.Int(nullable: false),
                        RequestType = c.Int(nullable: false),
                        RAG = c.Int(nullable: false),
                        ServiceLine = c.Int(nullable: false),
                        Customer = c.String(),
                        CustomerCompany = c.String(),
                        demandStatus = c.Int(nullable: false),
                        CompletionDate = c.DateTime(),
                        RequesterName = c.String(),
                        Blocked = c.Boolean(nullable: false),
                        ReasonOfBlockage = c.String(),
                        phase = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DemandLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssignedTeam = c.Int(nullable: false),
                        TeamStatus = c.Int(nullable: false),
                        Comments = c.String(),
                        DemandId = c.Long(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        ServiceLine = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        RAG = c.Int(nullable: false),
                        demandStatus = c.Int(nullable: false),
                        phase = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Demands", t => t.DemandId, cascadeDelete: true)
                .Index(t => t.DemandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemandLogs", "DemandId", "dbo.Demands");
            DropIndex("dbo.DemandLogs", new[] { "DemandId" });
            DropTable("dbo.DemandLogs");
            DropTable("dbo.Demands");
        }
    }
}
