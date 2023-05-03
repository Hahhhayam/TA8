using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA8.API.Exeptions
{
    class TreeNodeExeption : ApplicationException
    {
        private readonly string _data;
        private readonly bool _exist;
        public TreeNodeExeption(string data, bool exist = true) 
        {
            _data = data;
            _exist = exist;
        }
        public override string Message 
        {
            get 
            {
                if (_exist) 
                {
                    return $"{_data} already exist";
                } 
                else 
                { 
                    return $"{_data} not exist";
                }
            }
        }
    }
}
