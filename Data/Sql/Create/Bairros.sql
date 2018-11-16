USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Bairros]    Script Date: 20/09/2018 20:18:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bairros](
	[BairroId] [int] IDENTITY(1,1) NOT NULL,
	[CidadeId] [int] NOT NULL,
	[DescBairro] [varchar](45) NOT NULL,
 CONSTRAINT [PK_dbo.Bairros] PRIMARY KEY CLUSTERED 
(
	[BairroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Bairros]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Bairros_dbo.Cidades_CidadeId] FOREIGN KEY([CidadeId])
REFERENCES [dbo].[Cidades] ([CidadeId])
GO

ALTER TABLE [dbo].[Bairros] CHECK CONSTRAINT [FK_dbo.Bairros_dbo.Cidades_CidadeId]
GO


