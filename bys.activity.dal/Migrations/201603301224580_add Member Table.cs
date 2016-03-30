namespace bys.activity.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMemberTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ba.member",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Alias = c.String(maxLength: 100),
                        DisplayName = c.String(maxLength: 100),
                        AvantarPath = c.String(maxLength: 200),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("ba.member");
        }
    }
}
