namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_like_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ba.activityLikeInfo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ActivityID = c.Guid(nullable: false),
                        MemberAlias = c.String(maxLength: 100),
                        LikeTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("ba.activityLikeInfo");
        }
    }
}
