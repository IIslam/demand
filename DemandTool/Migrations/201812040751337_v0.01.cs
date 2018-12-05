namespace DemandTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DemandLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssignedTeam = c.Int(nullable: false),
                        TeamStatus = c.Int(nullable: false),
                        Comments = c.String(),
                        UpdatedDate = c.DateTime(nullable: false),
                        ServiceLine = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        RAG = c.Int(nullable: false),
                        DemandStatus = c.Int(nullable: false),
                        Phase = c.Int(nullable: false),
                        DemandId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Demands", t => t.DemandId, cascadeDelete: true)
                .Index(t => t.DemandId);
            
            CreateTable(
                "dbo.Demands",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DemandNumber = c.String(nullable: false),
                        SubmissionDate = c.DateTime(),
                        DemandDesc = c.String(),
                        Priority = c.Int(nullable: false),
                        RequestType = c.Int(nullable: false),
                        RAG = c.Int(nullable: false),
                        ServiceLine = c.Int(nullable: false),
                        Customer = c.String(),
                        CustomerCompany = c.String(),
                        DemandStatus = c.Int(nullable: false),
                        CompletionDate = c.DateTime(),
                        RequesterName = c.String(),
                        Blocked = c.Boolean(nullable: false),
                        ReasonOfBlockage = c.String(),
                        Phase = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.DemandLogs", "DemandId", "dbo.Demands");
            DropIndex("dbo.DemandLogs", new[] { "DemandId" });
            DropTable("dbo.DemandModels");
            DropTable("dbo.Demands");
            DropTable("dbo.DemandLogs");
        }
    }
}
