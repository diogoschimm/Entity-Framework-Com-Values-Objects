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
