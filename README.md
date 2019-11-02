# Entity-Framework-Com-Values-Objects

Salvando/Listando Entidade com Values Object com Entity Framework

Crie a seguinte tabela em Banco de Dados

```sql
    CREATE TABLE Cliente (
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
        CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ( [clienteId] ASC ) 
    )   
```

Altere a configuração da String de Conexão do Arquivo Program (ValuesObjectsEntityFramework.Presentation.ConsoleApp\Program.cs)

```c#
    static IServiceProvider GetProvider()
    {
        IServiceCollection collection = new ServiceCollection();
        collection.AddDbContext<ProjetoContext>(opt =>
        {
            opt.UseSqlServer("Data Source=DESKTOP-4BB99RK\\SQL2017;Initial Catalog=ProjetoTeste;Integrated Security=True;");
        });
        return collection.BuildServiceProvider();
    }
```

Após isso, execute o Projeto Presentation.ConsoleApp.

