using System;
using System.Collections.Generic;
using TA8.API.Exeptions;
using TA8.API.Interfaces;

namespace TA8.API
{
    class Controller
    {
        private ITree tree;

        public Controller(ITree tree) => this.tree = tree;

        public bool FindElement(string element)
        {
            if (!Int32.TryParse(element, out int data))
            { throw new InvalidDataFormatExeption("int", "findElement"); }
            if (tree == null)
            { throw new NullTreeExeption(); }

            return tree.FindElement(data);
        }

        public void AddElement(string element)
        {
            if (!Int32.TryParse(element, out int data))
            { throw new InvalidDataFormatExeption("int", "addElement"); }
            if (tree == null)
            { throw new NullTreeExeption(); }

            if (tree.FindElement(data))
            { throw new TreeNodeExeption(element); }
            tree.NumList.Add(data);
            tree.AddElement(data);
        }

        public void RemoveElement(string element)
        {
            if (!Int32.TryParse(element, out int data))
            { throw new InvalidDataFormatExeption("int", "removeElement"); }
            if (tree == null)
            { throw new NullTreeExeption(); }

            if (!tree.FindElement(data))
            { throw new TreeNodeExeption(element, false); }
            tree.NumList.Remove(data);
            tree.RemoveElement(data);
        }

        public List<int> GetDirection(string element)
        {
            if (!Int32.TryParse(element, out int data) || data < 1)
            { throw new InvalidDataFormatExeption("int", "getDirections"); }

            if (!tree.NumList.Contains(data)) { tree.NumList.Add(data); }
            return tree.GetDirections(data);
        }

        public List<int> GetList() 
        {
            return tree.NumList;
        }
    }
}
