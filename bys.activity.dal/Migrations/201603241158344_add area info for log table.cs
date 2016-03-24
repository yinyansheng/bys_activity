namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addareainfoforlogtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("ba.syslog", "Area", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("ba.syslog", "Area");
        }
    }
}
