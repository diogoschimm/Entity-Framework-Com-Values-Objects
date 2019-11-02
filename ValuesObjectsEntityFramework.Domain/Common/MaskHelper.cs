using System;
using System.Collections.Generic;
using System.Text;

namespace ValuesObjectsEntityFramework.Domain.Common
{
   public  class MaskHelper
    {
        public static string RemoveMask(string valor)
        {
            return valor.Replace("-", "").Replace(".", "").Replace("/", "");
        }
    }
}
