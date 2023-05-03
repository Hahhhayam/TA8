using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TA8.API.Exeptions
{
    class InvalidDataFormatExeption: ApplicationException
    {
        private readonly string _type;
        private readonly string _method;
        public InvalidDataFormatExeption(string variable, string method)
        {
            _method = method;
            _type = variable;
        }
        public override string Message => $"Data must be {_type}, when sended to {_method}";
    }
}
