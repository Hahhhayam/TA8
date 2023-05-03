using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA8.API.Exeptions
{
    class NullTreeExeption : ApplicationException
    {
        public override string Message => $"You trying to use tree before its init";
    }
}
