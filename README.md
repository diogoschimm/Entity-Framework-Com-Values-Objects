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

## O Projeto

Foi criado um projeto chamado ValuesObjectEntityFramework.Domain, dentro deste projeto foi criado a seguinte classe:

```c#
    using System;
    using ValuesObjectsEntityFramework.Domain.Common;
    using ValuesObjectsEntityFramework.Domain.ValuesObjects;

    namespace ValuesObjectsEntityFramework.Domain.Entidades
    {
        public class Cliente : NotifiableContract
        {
            public int ClienteId { get; private set; }
            public string NomeCliente { get; private set; }
            public CPF CPF { get; private set; }
            public Endereco Endereco { get; private set; }

            public Cliente(string nome, CPF cpf, Endereco endereco)
            {
                this.ClienteId = 0;
                this.NomeCliente = nome;
                this.CPF = cpf;
                this.Endereco = endereco;

                this.Validate();
            }
            protected Cliente() { }

            private void Validate()
            {
                this.Requires()
                    .IsNotNullOrEmpty(this.NomeCliente, "NomeCliente", "Nome do Cliente não pode ser em branco");

                if (this.CPF.Invalid)
                    this.AddNotifications(this.CPF.Notifications);

                if (this.Endereco.Invalid)
                    this.AddNotifications(this.Endereco.Notifications);

                this.AddContractNotifications();
            }
        }
    }
```

Essa entidade possui os seguintes objetos de valor (CPF e Endereco), abaixo os objetos de valor.

### CPF

```c#
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ValuesObjectsEntityFramework.Domain.Common;

    namespace ValuesObjectsEntityFramework.Domain.ValuesObjects
    {
        public class CPF : NotifiableContract
        {
            public string Numero { get; private set; }

            public CPF(string num)
            {
                this.Numero = this.Prepare(num);
                this.Validate();
            }
            protected CPF() { }

            private string Prepare(string num)
            {
                return MaskHelper.RemoveMask(num);
            }

            private void Validate()
            {
                this.Requires()
                    .IsNotNullOrEmpty(this.Numero, "CPF", "CPF não pode ser em branco")
                    .HasLen(this.Numero, 11, "CPF", "CPF deve possuir 11 Dígitos");

                this.AddContractNotifications();
            }
        }
    }

```

### Endereco

```c#
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ValuesObjectsEntityFramework.Domain.Common;

    namespace ValuesObjectsEntityFramework.Domain.ValuesObjects
    {
        public class Endereco : NotifiableContract
        {
            public CEP CEP { get; private set; }
            public string Logradouro { get; private set; }
            public string Numero { get; private set; }
            public string Bairro { get; private set; }
            public string Cidade { get; private set; }
            public string UF { get; private set; }
            public string Complemento { get; private set; }

            public Endereco(CEP cep, string logradouro, string numero, string bairro, string cidade, string uf, string complemento)
            {
                this.CEP = cep;
                this.Logradouro = logradouro;
                this.Numero = numero;
                this.Bairro = bairro;
                this.Cidade = cidade;
                this.UF = uf;
                this.Complemento = complemento;
                this.Validate();
            }
            protected Endereco() { }

            private void Validate()
            {
                if (this.CEP.Invalid)
                    this.AddNotifications(this.CEP.Notifications);

                this.Requires()
                    .IsNotNullOrEmpty(this.Logradouro, "Logradouro", "Logradouro não pode ser em Branco")
                    .IsNotNullOrEmpty(this.Numero, "Numero", "Número do Endereço não pode ser em Branco")
                    .IsNotNullOrEmpty(this.Bairro, "Bairro", "Bairro não pode ser em Branco")
                    .IsNotNullOrEmpty(this.Cidade, "Cidade", "Cidade não pode ser em Branco")
                    .IsNotNullOrEmpty(this.UF, "UF", "UF não pode ser em Branco");

                this.AddContractNotifications();
            }
        }
    }

```

### CEP

```c#
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ValuesObjectsEntityFramework.Domain.Common;

    namespace ValuesObjectsEntityFramework.Domain.ValuesObjects
    {
        public class CEP :NotifiableContract
        {
            public string Numero { get; private set; }

            public CEP(string num)
            {
                this.Numero = num;
                this.Validate();
            }
            protected CEP() { }

            private void Validate()
            {
                this.Requires().HasLen(this.Numero, 9, "CEP", "CEP deve possuir 9 Caracteres (XXXXX-XXX)");

                this.AddContractNotifications();
            }
        }
    } 
```

## Configurando o Mapping do Entity Framework

Para configurar o Entity Framework foi criado o projeto ValuesObjectsEntityFramework.Infra.Data que possui o Context e a Configuração do Cliente, abaixo a configuração do Entity para a Entidade Cliente e seus Values Objects.

```c#
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ValuesObjectsEntityFramework.Domain.Entidades;

    namespace ValuesObjectsEntityFramework.Infra.Data.Mappings
    {
        public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
        {
            public void Configure(EntityTypeBuilder<Cliente> builder)
            {
                builder.OwnsOne(c => c.CPF, c =>
                {
                    c.Property(e => e.Numero).HasColumnName("CPF");
                });
                builder.OwnsOne(c => c.Endereco, vo =>
                {
                    vo.OwnsOne(e => e.CEP, e =>
                    {
                        e.Property(c => c.Numero).HasColumnName("CEP");
                    });

                    vo.Property(e => e.Logradouro).HasColumnName("endereco");
                    vo.Property(e => e.Numero).HasColumnName("numeroEndereco");
                    vo.Property(e => e.Bairro).HasColumnName("bairro");
                    vo.Property(e => e.Cidade).HasColumnName("cidade");
                    vo.Property(e => e.UF).HasColumnName("UF");
                    vo.Property(e => e.Complemento).HasColumnName("complemento");
                });
            }
        }
    }

```