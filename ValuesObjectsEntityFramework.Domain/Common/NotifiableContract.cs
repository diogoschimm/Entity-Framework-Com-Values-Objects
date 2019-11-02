using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValuesObjectsEntityFramework.Domain.Common
{
    public abstract class NotifiableContract : Notifiable
    {
        private Contract _contrato;

        protected Contract Requires()
        {
            if (this._contrato == null)
                this._contrato = new Contract();

            return this._contrato;
        }

        protected void AddContractNotifications()
        {
            if (this._contrato != null && this._contrato.Invalid)
                this.AddNotifications(this._contrato.Notifications);
        }
    }
}
