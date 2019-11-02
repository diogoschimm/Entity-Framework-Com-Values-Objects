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
