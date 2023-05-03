using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA8.API.Interfaces
{
    internal interface ITree
    {
        public List<int> NumList { get; set; }
        public bool FindElement(int element);
        public void AddElement(int element);
        public void RemoveElement(int element);
        public List<int> GetDirections(int elemment);

    }
}
