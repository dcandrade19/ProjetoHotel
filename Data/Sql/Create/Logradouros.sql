USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Logradouros]    Script Date: 20/09/2018 20:21:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Logradouros](
	[NumCep] [int] NOT NULL,
	[BairroId] [int] NOT NULL,
	[DescLogradouro] [varchar](45) NOT NULL,
	[DescTipo] [varchar](10) NOT NULL,
 CONSTRAINT [PK_dbo.Logradouros] PRIMARY KEY CLUSTERED 
(
	[NumCep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Logradouros]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Logradouros_dbo.Bairros_BairroId] FOREIGN KEY([BairroId])
REFERENCES [dbo].[Bairros] ([BairroId])
GO

ALTER TABLE [dbo].[Logradouros] CHECK CONSTRAINT [FK_dbo.Logradouros_dbo.Bairros_BairroId]
GO


