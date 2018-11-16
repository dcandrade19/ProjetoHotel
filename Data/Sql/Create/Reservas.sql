USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Reservas]    Script Date: 20/09/2018 20:22:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reservas](
	[ReservaId] [int] IDENTITY(1,1) NOT NULL,
	[DataReserva] [datetime] NOT NULL,
	[TuristaId] [int] NOT NULL,
	[Chegada] [date] NOT NULL,
	[Partida] [date] NOT NULL,
	[ValorDiaria] [numeric](18, 2) NULL,
	[QuartoId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Reservas] PRIMARY KEY CLUSTERED 
(
	[ReservaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reservas_dbo.Quartos_QuartoId] FOREIGN KEY([QuartoId])
REFERENCES [dbo].[Quartos] ([QuartoId])
GO

ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_dbo.Reservas_dbo.Quartos_QuartoId]
GO

ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reservas_dbo.Turistas_TuristaId] FOREIGN KEY([TuristaId])
REFERENCES [dbo].[Turistas] ([TuristaId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_dbo.Reservas_dbo.Turistas_TuristaId]
GO


