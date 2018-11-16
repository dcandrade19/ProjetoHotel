USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Quartos]    Script Date: 20/09/2018 20:22:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Quartos](
	[QuartoId] [int] IDENTITY(1,1) NOT NULL,
	[HotelId] [int] NOT NULL,
	[Titulo] [nvarchar](50) NOT NULL,
	[Descricao] [nvarchar](250) NULL,
	[Quantidade] [int] NOT NULL,
	[Disponiveis] [int] NOT NULL,
	[MaximoOcupantes] [int] NOT NULL,
	[ValorDiaria] [numeric](18, 2) NOT NULL,
	[ValorDiariaCrianca] [numeric](18, 2) NOT NULL,
	[DiariaPorOcupante] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Quartos] PRIMARY KEY CLUSTERED 
(
	[QuartoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Quartos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Quartos_dbo.Hoteis_HotelId] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hoteis] ([HotelId])
GO

ALTER TABLE [dbo].[Quartos] CHECK CONSTRAINT [FK_dbo.Quartos_dbo.Hoteis_HotelId]
GO


