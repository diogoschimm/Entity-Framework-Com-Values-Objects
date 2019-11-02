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
