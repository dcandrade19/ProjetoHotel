USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Turistas]    Script Date: 20/09/2018 20:23:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Turistas](
	[TuristaId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Sexo] [bit] NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[Cpf] [nvarchar](14) NOT NULL,
	[Senha] [nchar](50) NULL,
 CONSTRAINT [PK_dbo.Turistas] PRIMARY KEY CLUSTERED 
(
	[TuristaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


