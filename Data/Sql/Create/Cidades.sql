USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Cidades]    Script Date: 20/09/2018 20:20:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cidades](
	[CidadeId] [int] IDENTITY(1,1) NOT NULL,
	[DescCidade] [varchar](60) NOT NULL,
	[FlgEstado] [char](2) NOT NULL,
 CONSTRAINT [PK_dbo.Cidades] PRIMARY KEY CLUSTERED 
(
	[CidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cidades]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Cidades_dbo.Ufs_FlgEstado] FOREIGN KEY([FlgEstado])
REFERENCES [dbo].[Ufs] ([UfId])
GO

ALTER TABLE [dbo].[Cidades] CHECK CONSTRAINT [FK_dbo.Cidades_dbo.Ufs_FlgEstado]
GO


