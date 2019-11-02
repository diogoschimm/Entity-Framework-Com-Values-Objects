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
