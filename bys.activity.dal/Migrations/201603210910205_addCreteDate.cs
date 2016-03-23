namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreteDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("ba.activity", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ba.activity", "CreateDate");
        }
    }
}
