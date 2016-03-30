namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrankforactivitytype : DbMigration
    {
        public override void Up()
        {
            AddColumn("ba.activityType", "Rank", c => c.Int());
            AddColumn("ba.activityType", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ba.activityType", "CreateDate");
            DropColumn("ba.activityType", "Rank");
        }
    }
}
