namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlogtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ba.syslog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(maxLength: 100),
                        UserName = c.String(maxLength: 200),
                        ControllerName = c.String(maxLength: 200),
                        ActionName = c.String(maxLength: 200),
                        MethodName = c.String(maxLength: 200),
                        ExecuteTime = c.String(maxLength: 200),
                        Msg = c.String(maxLength: 500),
                        LogTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("ba.syslog");
        }
    }
}
