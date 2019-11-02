
CREATE TABLE [dbo].[Cliente](
	[clienteId] [int] IDENTITY(1,1) NOT NULL,
	[nomeCliente] [varchar](100) NULL,
	[cpf] [varchar](11) NULL,
	[cep] [varchar](9) NULL,
	[endereco] [varchar](100) NULL,
	[numeroEndereco] [varchar](20) NULL,
	[bairro] [varchar](100) NULL,
	[cidade] [varchar](100) NULL,
	[uf] [varchar](2) NULL,
	[complemento] [varchar](100) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[clienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO