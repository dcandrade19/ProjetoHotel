namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bairros",
                c => new
                    {
                        BairroId = c.Int(nullable: false, identity: true),
                        CidadeId = c.Int(nullable: false),
                        DescBairro = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.BairroId)
                .ForeignKey("dbo.Cidades", t => t.CidadeId)
                .Index(t => t.CidadeId);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        CidadeId = c.Int(nullable: false, identity: true),
                        DescCidade = c.String(nullable: false, maxLength: 60, unicode: false),
                        FlgEstado = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.CidadeId)
                .ForeignKey("dbo.Ufs", t => t.FlgEstado)
                .Index(t => t.FlgEstado);
            
            CreateTable(
                "dbo.Ufs",
                c => new
                    {
                        UfId = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        DescUf = c.String(nullable: false, maxLength: 60, unicode: false),
                    })
                .PrimaryKey(t => t.UfId);
            
            CreateTable(
                "dbo.Logradouros",
                c => new
                    {
                        NumCep = c.Int(nullable: false),
                        BairroId = c.Int(nullable: false),
                        DescLogradouro = c.String(nullable: false, maxLength: 45, unicode: false),
                        DescTipo = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.NumCep)
                .ForeignKey("dbo.Bairros", t => t.BairroId)
                .Index(t => t.BairroId);
            
            CreateTable(
                "dbo.Hoteis",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Endereco = c.String(nullable: false, maxLength: 100),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(nullable: false, maxLength: 20),
                        Cep = c.String(nullable: false, maxLength: 9),
                        Cidade = c.String(nullable: false, maxLength: 60, unicode: false),
                        Bairro = c.String(nullable: false, maxLength: 60),
                        Uf = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        Ddd = c.String(nullable: false, maxLength: 4),
                        Telefone = c.String(nullable: false, maxLength: 9),
                        Descricao = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.HotelId);
            
            CreateTable(
                "dbo.Quartos",
                c => new
                    {
                        QuartoId = c.Int(nullable: false, identity: true),
                        HotelId = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 50),
                        Descricao = c.String(maxLength: 250),
                        Quantidade = c.Int(nullable: false),
                        Disponiveis = c.Int(nullable: false),
                        MaximoOcupantes = c.Int(nullable: false),
                        ValorDiaria = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        ValorDiariaCrianca = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        DiariaPorOcupante = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuartoId)
                .ForeignKey("dbo.Hoteis", t => t.HotelId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        ReservaId = c.Int(nullable: false, identity: true),
                        DataReserva = c.DateTime(nullable: false),
                        TuristaId = c.Int(nullable: false),
                        Chegada = c.DateTime(nullable: false, storeType: "date"),
                        Partida = c.DateTime(nullable: false, storeType: "date"),
                        ValorDiaria = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        QuartoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservaId)
                .ForeignKey("dbo.Turistas", t => t.TuristaId, cascadeDelete: true)
                .ForeignKey("dbo.Quartos", t => t.QuartoId)
                .Index(t => t.TuristaId)
                .Index(t => t.QuartoId);
            
            CreateTable(
                "dbo.Turistas",
                c => new
                    {
                        TuristaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        Sexo = c.Boolean(nullable: false),
                        DataNascimento = c.DateTime(nullable: false, storeType: "date"),
                        Cpf = c.String(nullable: false, maxLength: 14),
                        Senha = c.String(maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.TuristaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quartos", "HotelId", "dbo.Hoteis");
            DropForeignKey("dbo.Reservas", "QuartoId", "dbo.Quartos");
            DropForeignKey("dbo.Reservas", "TuristaId", "dbo.Turistas");
            DropForeignKey("dbo.Logradouros", "BairroId", "dbo.Bairros");
            DropForeignKey("dbo.Cidades", "FlgEstado", "dbo.Ufs");
            DropForeignKey("dbo.Bairros", "CidadeId", "dbo.Cidades");
            DropIndex("dbo.Reservas", new[] { "QuartoId" });
            DropIndex("dbo.Reservas", new[] { "TuristaId" });
            DropIndex("dbo.Quartos", new[] { "HotelId" });
            DropIndex("dbo.Logradouros", new[] { "BairroId" });
            DropIndex("dbo.Cidades", new[] { "FlgEstado" });
            DropIndex("dbo.Bairros", new[] { "CidadeId" });
            DropTable("dbo.Turistas");
            DropTable("dbo.Reservas");
            DropTable("dbo.Quartos");
            DropTable("dbo.Hoteis");
            DropTable("dbo.Logradouros");
            DropTable("dbo.Ufs");
            DropTable("dbo.Cidades");
            DropTable("dbo.Bairros");
        }
    }
}
