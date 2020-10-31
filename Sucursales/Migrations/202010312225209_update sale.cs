namespace Sucursales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesale : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sales", new[] { "User_Id" });
            DropColumn("dbo.Sales", "UserId");
            RenameColumn(table: "dbo.Sales", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Sales", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sales", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sales", new[] { "UserId" });
            AlterColumn("dbo.Sales", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Sales", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Sales", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "User_Id");
        }
    }
}
