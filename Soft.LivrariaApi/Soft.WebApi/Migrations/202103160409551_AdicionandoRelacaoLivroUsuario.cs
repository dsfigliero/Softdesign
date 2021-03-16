namespace Soft.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoRelacaoLivroUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LivroUsuario",
                c => new
                    {
                        LivroUsuarioId = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        LivroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LivroUsuarioId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Livro", t => t.LivroId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.LivroId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LivroUsuario", new[] { "LivroId" });
            DropIndex("dbo.LivroUsuario", new[] { "UsuarioId" });
            DropForeignKey("dbo.LivroUsuario", "LivroId", "dbo.Livro");
            DropForeignKey("dbo.LivroUsuario", "UsuarioId", "dbo.Usuario");
            DropTable("dbo.LivroUsuario");
        }
    }
}
