using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.Core.Convertor
{
    public static class AmountConvertor
    {
        public static string ToRial(this int value)
        {
            return value.ToString("#,0 تومان");
        }
    }
}
