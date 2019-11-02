using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using ValuesObjectsEntityFramework.Domain.Entidades;
using ValuesObjectsEntityFramework.Domain.ValuesObjects;
using ValuesObjectsEntityFramework.Infra.Data;

namespace ValuesObjectsEntityFramework.Presentation.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            var provider = GetProvider();
            var ctx = provider.GetService<ProjetoContext>();

            IniciarTela(ctx);
        }

        static IServiceProvider GetProvider()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddDbContext<ProjetoContext>(opt =>
            {
                opt.UseSqlServer("Data Source=DESKTOP-4BB99RK\\SQL2017;Initial Catalog=ProjetoTeste;Integrated Security=True;");
            });
            return collection.BuildServiceProvider();
        }

        static void IniciarTela(ProjetoContext ctx)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[ 1 ] - Cadastrar Novo Cliente");
            Console.WriteLine("[ 2 ] - Listar Clientes");
            Console.WriteLine("[ X ] - Sair");
            Console.WriteLine("");

            string opt = Console.ReadLine();

            Console.WriteLine("");

            if (opt == "1")
            {
                AbrirTelaAddCliente(ctx);
                IniciarTela(ctx);
            }
            else if (opt == "2")
            {
                AbrirTelaListarCliente(ctx);
                IniciarTela(ctx);
            }
        }

        static void AbrirTelaAddCliente(ProjetoContext ctx)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o Nome do Cliente: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _nomeCliente = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o CPF do Cliente: ");  

            Console.ForegroundColor = ConsoleColor.Green;
            string _cpf = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o CEP: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _cep = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o Endereço: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _endereco = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o Nº do Endereço: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _numero = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o Bairro do Endereço: ");
              
            Console.ForegroundColor = ConsoleColor.Green;
            string _bairro = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe a Cidade do Endereço: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _cidade = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe a UF do Endereço: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _uf = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Informe o Complemento do Endereço: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string _complemento = Console.ReadLine();
             
            var endereco = new Endereco(new CEP(_cep), _endereco, _numero, _bairro, _cidade, _uf, _complemento);
            var cliente = new Cliente(_nomeCliente, new CPF(_cpf), endereco);

            if (cliente.Valid)
            {
                ctx.Cliente.Add(cliente);
                ctx.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("");
                Console.WriteLine("****** Cliente Cadastrado com Sucesso ****** ");
                Console.WriteLine("");
            }
            else
            { 
                Console.ForegroundColor = ConsoleColor.Yellow ;

                Console.WriteLine("");
                Console.WriteLine("****** Cliente não cadastrado ****** ");
                Console.WriteLine("");

                foreach (var item in cliente.Notifications)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{item.Property} => {item.Message}");
                }
            }

            Console.WriteLine("");
            Console.ReadKey();
        }

        static void AbrirTelaListarCliente(ProjetoContext ctx)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("");
            Console.WriteLine("Lista de Clientes...: ");

            foreach (var item in ctx.Cliente)
            {
                Console.WriteLine("");
                Console.WriteLine("ID: " + item.ClienteId);
                Console.WriteLine("Nome: " + item.NomeCliente);
                Console.WriteLine("CPF: " + item.CPF.Numero);
                Console.WriteLine("CEP: " + item.Endereco.CEP.Numero);
                Console.WriteLine("Endereço: " + item.Endereco.Logradouro);
                Console.WriteLine("Local: " + item.Endereco.Bairro +" - " + item.Endereco.Cidade +" - " + item.Endereco.UF);
                Console.WriteLine("---------------------------------------------------------");
            }

            Console.WriteLine("");
            Console.ReadKey();
        }

    }
}
