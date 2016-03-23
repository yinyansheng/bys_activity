namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ba.activity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        ActivityTypeID = c.Guid(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 500),
                        OriginatorAlias = c.String(maxLength: 100),
                        Detail = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ba.activityJoinInfo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActivityID = c.Guid(nullable: false),
                        MemberAlias = c.String(maxLength: 100),
                        JoinTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ba.activityType",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        PosterImagePath1 = c.String(maxLength: 500),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("ba.activityType");
            DropTable("ba.activityJoinInfo");
            DropTable("ba.activity");
        }
    }
}
