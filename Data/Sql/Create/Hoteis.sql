USE [ReservasDataBase]
GO

/****** Object:  Table [dbo].[Hoteis]    Script Date: 20/09/2018 20:21:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Hoteis](
	[HotelId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Endereco] [nvarchar](100) NOT NULL,
	[Numero] [int] NOT NULL,
	[Complemento] [nvarchar](20) NOT NULL,
	[Cep] [nvarchar](9) NOT NULL,
	[Cidade] [varchar](60) NOT NULL,
	[Bairro] [nvarchar](60) NOT NULL,
	[Uf] [char](2) NOT NULL,
	[Ddd] [nvarchar](4) NOT NULL,
	[Telefone] [nvarchar](9) NOT NULL,
	[Descricao] [nvarchar](250) NULL,
 CONSTRAINT [PK_dbo.Hoteis] PRIMARY KEY CLUSTERED 
(
	[HotelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


