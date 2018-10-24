namespace BancoTabajara.Infra.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBConta", "Titular_Id", "dbo.TBCliente");
            DropIndex("dbo.TBConta", new[] { "Titular_Id" });
            RenameColumn(table: "dbo.TBConta", name: "Titular_Id", newName: "ClienteId");
            CreateTable(
                "dbo.TBUsuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        AccessKey = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.TBConta", "ClienteId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBConta", "ClienteId");
            AddForeignKey("dbo.TBConta", "ClienteId", "dbo.TBCliente", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConta", "ClienteId", "dbo.TBCliente");
            DropIndex("dbo.TBConta", new[] { "ClienteId" });
            AlterColumn("dbo.TBConta", "ClienteId", c => c.Int());
            DropTable("dbo.TBUsuario");
            RenameColumn(table: "dbo.TBConta", name: "ClienteId", newName: "Titular_Id");
            CreateIndex("dbo.TBConta", "Titular_Id");
            AddForeignKey("dbo.TBConta", "Titular_Id", "dbo.TBCliente", "Id");
        }
    }
}
